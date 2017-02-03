using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KickerWriter.Data;
using KickerWriter.Model;

namespace KickerWriter.Design
{
    public class DesignDataService : IDataService
    {
        public IEnumerable<Season> GetAllSeasons()
        {
            ObservableCollection<Season> allSeasons = new ObservableCollection<Season>();
            for (int seasonNumber = 1; seasonNumber <= 5; seasonNumber++)
            {
                allSeasons.Add(GetSingleSeason(seasonNumber));
            }
            return allSeasons;
        }

        private Season GetSingleSeason(int seasonYearOrNumber)
        {
            int actualSeasonNumber = seasonYearOrNumber;
            int actualSeasonYear = seasonYearOrNumber + 3000;
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
            Vorbericht actualVorbericht = new Vorbericht();


            for (int i = 1; i <= 4; i++)
            {
                VorberichtMitspieler mitspieler = new VorberichtMitspieler
                {
                    Name = "Spieler" + i,
                    Liga = "Liga" + 1 + i,
                    SaisonzielManager = "Ziel" + i * 100,
                    SaisonzielVorstand = "Ziel" + i * 1000,
                    Stadion = (i * 10000).ToString(),
                    Stärke = i.ToString(),
                    Verein = "Team" + i
                };
                actualVorbericht.Mitspieler.Add(mitspieler);
            }
            actualVorbericht.Header = "Das ist Saison " + actualSeason.Number;
            actualVorbericht.Kommentare = GetKommentare();
            actualSeason.Vorbericht = actualVorbericht;


            return actualSeason;
        }

        public ObservableCollection<Kommentar> GetKommentare()
        {
            Kommentar mockKommentar = new Kommentar
            {
                Name = "Giovane Elber",
                Text =
                    "David Jakobeit hat mal wieder sein Gespür für extravagante Transfers gezeigt und mit Alfredo de Lemos von den Corinthians ein echtes Schnäppchen gemacht. Viele denken jetzt, dass dies eine von Jakobeits Harakiri-Aktionen ist, doch meiner Meinung nach hat der Deutsche hier wieder einmal seine gute Kenntnis des internationalen Marktes gezeigt. Die Jugendarbeit bei Corinthians ist erstklassig und nicht umsonst zählt der Verein zur besten Kaderschmiede Brasiliens. De Lemos hat eine hervorragende Ausbildung genossen und ist nicht umsonst Jugendnationalspieler der Selecao. Ich denke, dass er den Zuschauern in Fleetwood noch viel Freude bereiten kann, sollte er sich gut in England einleben und sich an den dortigen Fußball gewöhnen.",
                PicturePath = @"C:\Users\David\Dropbox\Anstoss_3\KickerLatex\Bilder\SonstigeMenschen\GiovaneElber.jpg"
            };
            mockKommentar.Text = SetLineBreaksForKommentarText(mockKommentar.Text);
            ObservableCollection<Kommentar> kommentare = new ObservableCollection<Kommentar> {mockKommentar, mockKommentar};
            return kommentare;
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