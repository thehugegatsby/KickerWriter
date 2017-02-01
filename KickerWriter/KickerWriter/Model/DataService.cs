using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using KickerWriter.Utilities;


namespace KickerWriter.Model
{
    public class DataService : IDataService
    {
        private FilePathesBuilder _filePathesBuilder;
        private FilePathesBuilder _filePathesBuilderDesktop;
        private SeasonCalculations _seasonCalculations;
        private KickerConstants _kickerConstants;
        public void GetData(Action<DataItem, Exception> callback)
        {
            this._filePathesBuilder = new FilePathesBuilder();
            this._filePathesBuilderDesktop = new FilePathesBuilder(new KickerLatexPathDesktop());
            this._seasonCalculations = new SeasonCalculations();
            this._kickerConstants = new KickerConstants();

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }

        public IEnumerable<Season> GetAllSeasons()
        {
            ObservableCollection<Season> allSeasons = new ObservableCollection<Season>();
            for (int seasonNumber = 1; seasonNumber <= this._filePathesBuilder.GetLastSeason(); seasonNumber++)
            {
                allSeasons.Add(GetSingleSeason(seasonNumber));
            }
            return allSeasons;
        }

        private Season GetSingleSeason(int seasonYearOrNumber)
        {
            int actualSeasonNumber = this._seasonCalculations.GetNumberFromYearOrNumber(seasonYearOrNumber);
            int actualSeasonYear = this._seasonCalculations.GetYearFromYearOrNumber(seasonYearOrNumber);
            Season actualSeason = new Season
            {
                Number = actualSeasonNumber,
                Year = actualSeasonYear
            };
            actualSeason = AddVorberichtToSeason(actualSeason);


            return actualSeason;
        }

        private Season AddVorberichtToSeason(Season actualSeason)
        {
            string vorberichtFilePath = this._filePathesBuilder.GetStatistikTextFilePath(actualSeason.Number,
                "Vorbericht");
            string[] vorberichtFileContent = File.ReadAllLines(vorberichtFilePath, Encoding.Default);
            Vorbericht actualVorbericht = new Vorbericht();
            VorberichtMitspieler actualVorberichtMitspieler = new VorberichtMitspieler();
            bool isMitspieler = false;
            bool isKommentar = false;
            for (int i = 0; i < vorberichtFileContent.Length; i++)
            {
                string line = vorberichtFileContent[i].Trim();
                if (line.Equals("-Header:"))
                {
                    actualVorbericht.Header = LineOperations.GetValue(vorberichtFileContent[i + 1]);
                }
                if (isMitspieler)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        isMitspieler = false;
                        actualVorbericht.Mitspieler.Add(actualVorberichtMitspieler);
                        actualVorberichtMitspieler = new VorberichtMitspieler();
                        continue;
                    }

                    string keyWord = LineOperations.GetKeyWord(line).Replace(" ", string.Empty);
                    var property = typeof(VorberichtMitspieler).GetProperty(keyWord);
                    property.SetValue(actualVorberichtMitspieler, LineOperations.GetValue(line), null);
                }
                else if (isKommentar)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        isKommentar = false;
                        continue;
                    }
                    string keyWord = LineOperations.GetKeyWord(line).Replace(" ", string.Empty);
                    var property = typeof(Kommentar).GetProperty(keyWord);
                    property.SetValue(actualVorbericht.Kommentar, LineOperations.GetValue(line), null);
                }
                if (!line.StartsWith("-"))
                {
                    continue;
                }

                if (this._kickerConstants.AllMitspielerNames.Any(mitspielerName => line.Contains(mitspielerName)))
                {
                    isMitspieler = true;
                    string name = LineOperations.GetKeyWord(line).Replace("-", string.Empty);
                    actualVorberichtMitspieler.Name = name;
                }
                if (line.Contains("Kommentar"))
                {
                    isKommentar = true;
                }
            }
            actualSeason.Vorbericht = actualVorbericht;
            actualSeason.Vorbericht.Kommentar.Text =
                SetLineBreaksForKommentarText(actualSeason.Vorbericht.Kommentar.Text);
            return actualSeason;
        }

        public void SaveSeason(Season season)
        {
            SaveVorbericht(season);
        }

        private void SaveVorbericht(Season season)
        {
            string filePathVorbericht = this._filePathesBuilderDesktop.GetStatistikTextFilePath(season.Number,
                "Vorbericht");
            List<string> newFileContent = new List<string>();
            foreach (VorberichtMitspieler mitspieler in season.Vorbericht.Mitspieler)
            {
                newFileContent.Add("-" + mitspieler.Name + ":");
                newFileContent.Add("Verein: " + mitspieler.Verein);
                newFileContent.Add("Stadion: " + mitspieler.Stadion);
                newFileContent.Add("Liga: " + mitspieler.Liga);
                newFileContent.Add("Stärke: " + mitspieler.Stärke);
                newFileContent.Add("Saisonziel Vorstand: " + mitspieler.SaisonzielVorstand);
                newFileContent.Add("Saisonziel Manager: " + mitspieler.SaisonzielManager);
                newFileContent.Add(string.Empty);
            }
            newFileContent.Add("-Header:");
            newFileContent.Add(season.Vorbericht.Header);
            newFileContent.Add(string.Empty);

            newFileContent.Add("-Kommentar:");
            newFileContent.Add("Name: " + season.Vorbericht.Kommentar.Name);
            newFileContent.Add("Text: " + season.Vorbericht.Kommentar.Text.Replace(Environment.NewLine, " "));
            File.WriteAllLines(filePathVorbericht, newFileContent, Encoding.Default);
        }

        private static string SetLineBreaksForKommentarText(string kommentarText)
        {
            int charCounter = 0;
            int rowCounter = 1;
            List<string> newKommentarTextList = new List<string>();
            foreach (string word in kommentarText.Split(' ').ToList())
            {
                charCounter += word.Length + 1;
                if (charCounter > 70 * rowCounter)
                {
                    newKommentarTextList[newKommentarTextList.Count - 1] += Environment.NewLine + word;
                    rowCounter++;
                }
                else
                {
                    newKommentarTextList.Add(word);
                }
            }
            return string.Join(" ", newKommentarTextList.ToArray());
        }
    }
}