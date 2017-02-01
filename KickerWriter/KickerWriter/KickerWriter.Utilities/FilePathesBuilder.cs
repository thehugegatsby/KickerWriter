namespace KickerWriter.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FilePathesBuilder
    {
        public FilePathesBuilder(IKickerLatexPath iKickerLatexPath)
        {
            this.KickerLatexPath = iKickerLatexPath;
            this.SeasonCalculations = new SeasonCalculations();
        }

        public FilePathesBuilder()
        {
            this.KickerLatexPath = new KickerLatexPathDropbox();
            this.SeasonCalculations = new SeasonCalculations();
        }

        public IKickerLatexPath KickerLatexPath { get; set; }

        private SeasonCalculations SeasonCalculations { get; set; }

        public string GetDropboxFolderPath()
        {
            const string infoPath = @"Dropbox\info.json";

            var jsonPath = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), infoPath);

            if (!File.Exists(jsonPath))
            {
                jsonPath = Path.Combine(Environment.GetEnvironmentVariable("AppData"), infoPath);
            }

            if (!File.Exists(jsonPath))
            {
                throw new Exception("Dropbox could not be found!");
            }

            var dropboxPath = File.ReadAllText(jsonPath).Split('\"')[5].Replace(@"\\", @"\");
            return dropboxPath;
        }

        public int GetLastSeason()
        {
            int lastSeason = 0;
            var directories = Directory.GetDirectories(this.KickerLatexPath.GetKickerLatexFolderPath());
            foreach (var directory in directories)
            {
                int actualSeason;
                if (!directory.Contains("_"))
                {
                    continue;
                }

                string[] words = directory.Split('_');
                bool parseResult = int.TryParse(words[words.Length - 1], out actualSeason);
                if (parseResult && actualSeason > lastSeason)
                {
                    lastSeason = actualSeason;
                }
            }


            return lastSeason;
        }

        public string GetStatistikFolderPath(int seasonYearOrNumber)
        {
            string seasonPath = this.GetSeasonFolderPath(seasonYearOrNumber);
            string statistikPath = Path.Combine(seasonPath, "Statistik");
            return statistikPath;
        }

        public string GetSeasonFolderPath(int seasonYearOrNumber)
        {
            int seasonYear;
            int seasonNumber;
            if (seasonYearOrNumber.ToString().Length == 4)
            {
                seasonYear = seasonYearOrNumber;
                seasonNumber = this.SeasonCalculations.GetNumberFromYear(seasonYear);
            }
            else
            {
                seasonNumber = seasonYearOrNumber;
                seasonYear = this.SeasonCalculations.GetYearFromNumber(seasonNumber);
            }

            string kickerLatexPath = this.KickerLatexPath.GetKickerLatexFolderPath();
            string seasonPath = Path.Combine(kickerLatexPath, seasonYear + "_Saison_" + seasonNumber);
            return seasonPath;
        }

        public string GetTemplateFolderPath()
        {
            string kickerLatexPath = this.KickerLatexPath.GetKickerLatexFolderPath();
            string templateFolderPath = Path.Combine(kickerLatexPath, "Templates");
            return templateFolderPath;
        }

        public string GetTemplateFilePath(string templateFileName)
        {
            string templateFolderPath = this.GetTemplateFolderPath();
            string templateFilePath = Path.Combine(templateFolderPath, templateFileName + ".tex");
            return templateFilePath;
        }

        public string GetStatistikTextFilePath(int seasonYearOrNumber, string statistikTextFileName)
        {
            string statistikFolderPath = this.GetStatistikFolderPath(seasonYearOrNumber);
            int seasonNumber = this.SeasonCalculations.GetNumberFromYearOrNumber(seasonYearOrNumber);
            string statistikFilePath = Path.Combine(statistikFolderPath,
                statistikTextFileName + "_Saison" + seasonNumber + ".txt");
            return statistikFilePath;
        }

        public string GetTableFilePath(int seasonYearOrNumber, string leagueName)
        {
            leagueName = leagueName.Replace(" ", string.Empty);
            if (leagueName.Equals("PremierLeague"))
            {
                leagueName = "Premier";
            }

            string statistikFolderPath = this.GetStatistikFolderPath(seasonYearOrNumber);
            int seasonNumber = this.SeasonCalculations.GetNumberFromYearOrNumber(seasonYearOrNumber);
            string tableFilePath = Path.Combine(statistikFolderPath, "Tabellen",
                "Tabelle_" + leagueName + "_Saison" + seasonNumber + ".txt");
            return tableFilePath;
        }

        public string GetSeasonLatexFilePath(int seasonYearOrNumber, string latexFileName)
        {
            string seasonFolderPath = this.GetSeasonFolderPath(seasonYearOrNumber);
            int seasonNumber = this.SeasonCalculations.GetNumberFromYearOrNumber(seasonYearOrNumber);
            int seasonYear = this.SeasonCalculations.GetYearFromYearOrNumber(seasonYearOrNumber);
            string latexFilePath = Path.Combine(
                seasonFolderPath,
                latexFileName + "_" + seasonYear + "_Saison" + seasonNumber + ".tex");
            return latexFilePath;
        }

        public string GetEmblemPath(string emblemName)
        {
            string pictureFolderPath = this.GetPictureFolder();
            string emblemFolderPath = Path.Combine(pictureFolderPath, "Wappen");
            string emblemPathWithoutExtension = Path.Combine(emblemFolderPath, pictureFolderPath);
            string emblemPath = emblemPathWithoutExtension + ".png";
            if (!File.Exists(emblemPath))
            {
                emblemPath = emblemPathWithoutExtension + ".jpg";
            }

            return emblemPath;
        }

        public string GetPictureFolder()
        {
            string kickerLatexFolderPath = this.KickerLatexPath.GetKickerLatexFolderPath();
            string pictureFolderPath = Path.Combine(kickerLatexFolderPath, "Bilder");
            return pictureFolderPath;
        }

        public string GetKommentatorenPictureFolder()
        {
            string pictureFolderPath = this.GetPictureFolder();
            string kommentatorenPicturePath = Path.Combine(pictureFolderPath, "Kommentatoren");
            return kommentatorenPicturePath;
        }
        public string GetSonderseitenFilePath(string sonderseitenFileName)
        {
            string kickerLatexFolderPath = this.KickerLatexPath.GetKickerLatexFolderPath();
            string sonderseitenFolderPath = Path.Combine(kickerLatexFolderPath, "Sonderseiten");
            string sonderseitenFilePath = Path.Combine(sonderseitenFolderPath, sonderseitenFileName + ".tex");
            return sonderseitenFilePath;
        }

        public List<string> GetAllTextFilesInStatistikFolder(int seasonYearOrNumber)
        {
            string statistikFolderPath = this.GetStatistikFolderPath(seasonYearOrNumber);
            DirectoryInfo directoryInfo = new DirectoryInfo(statistikFolderPath);
            FileInfo[] textFilesInStatistikFolder = directoryInfo.GetFiles("*.txt");
            return textFilesInStatistikFolder.Select(file => file.Name).ToList();
        }

        public string GetTeamListTextFilePath()
        {
            string templateFolderPath = this.GetTemplateFolderPath();
            string teamListTextFilePath = Path.Combine(templateFolderPath, "Vereinsliste.txt");
            return teamListTextFilePath;
        }

        public string GetFilePathFromNameAndSeasonNumber(int seasonYearOrNumber, string fileName)
        {
            return string.Empty;
        }

        public string GetKommenatorBildPfad(string kommentatorName)
        {
            string kommentatorPictureFolder = this.GetKommentatorenPictureFolder();
            string kommentatorPicturePath = Path.Combine(kommentatorPictureFolder, kommentatorName.Replace(" ", string.Empty) + ".jpg");
            if (!File.Exists(kommentatorPicturePath))
            {
                kommentatorPicturePath = kommentatorPicturePath.Replace(".jpg", ".png");
            }
            return kommentatorPicturePath;
        }
    }
}