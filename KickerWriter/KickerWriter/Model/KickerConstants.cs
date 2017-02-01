using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using KickerWriter.Utilities;

namespace KickerWriter.Model
{
    public class KickerConstants
    {
        public KickerConstants()
        {
            this.ReadInAllTeams();
            this.ReadInAllLeagues();
            this.ReadInAllVorstandZiele();
            this.ReadInAllManagerZiele();
            this.ReadInAllKommentatoren();
        }

        public List<Team> AllTeams { get; set; }
        public List<League> AllLeagues { get; set; }

        public List<string> AllVorstandZiele { get; set; }

        public List<string> AllManagerZiele { get; set; }

        public ObservableCollection<Kommentar> AllKommentatoren { get; set; }

        public List<string> AllMitspielerNames = new List<string>()

        {
            "David",
            "Basti",
            "Christian",
            "Jens",
        };

        private void ReadInAllLeagues()
        {
            this.AllLeagues = new List<League>();
            List<Tuple<string, int, bool>> allLeagueTriples =
                new List<Tuple<string, int, bool>>()
                {
                    new Tuple<string, int, bool>("Premier Liga", 1, false),
                    new Tuple<string, int, bool>("Championship", 2, false),
                    new Tuple<string, int, bool>("Liga One", 3, false),
                    new Tuple<string, int, bool>("Liga Two", 4, false),
                    new Tuple<string, int, bool>("Deutschland", 1, true),
                    new Tuple<string, int, bool>("Spanien", 1, true),
                    new Tuple<string, int, bool>("Schottland", 1, true),
                };
            foreach (var tuple in allLeagueTriples)
            {
                League league = new League(tuple.Item1)
                {
                    Level = tuple.Item2,
                    IsBonusLand = tuple.Item3,
                };
                this.AllLeagues.Add(league);
            }
        }

        private void ReadInAllTeams()
        {
            FilePathesBuilder filePathesBuilder = new FilePathesBuilder();
            string teamListFilePath = filePathesBuilder.GetTeamListTextFilePath();
            var teamListContent = File.ReadAllLines(teamListFilePath, Encoding.Default);
            this.AllTeams = new List<Team>();
            foreach (string line in teamListContent)
            {
                if (line.StartsWith("Verein:"))
                {
                    Team team =
                        new Team(line.Substring(line.IndexOf("Verein:", StringComparison.Ordinal) + "Verein:".Length).Trim());
                    this.AllTeams.Add(team);
                }
                else if (line.StartsWith("Wappen:"))
                {
                    string emblemName =
                        line.Substring(line.IndexOf("Wappen:", StringComparison.Ordinal) + "Wappen:".Length).Trim();

                    this.AllTeams[this.AllTeams.Count - 1].EmblemPath = filePathesBuilder.GetEmblemPath(emblemName);
                }
                else if (line.StartsWith("Stadt:"))
                {
                    this.AllTeams[this.AllTeams.Count - 1].City =
                        line.Substring(line.IndexOf("Stadt:", StringComparison.Ordinal) + "Stadt:".Length).Trim();
                }
            }
        }

        private void ReadInAllVorstandZiele()
        {
            this.AllVorstandZiele = new List<string>()
            {
                "Klassenerhalt",
                "Guter Fußball",
                "Mittelfeldplatz",
                "Einst. Tabellenplatz",
                "intern. Wettb.",
                "Oben mitspielen",
                "Erste Drei",
                "Aufstieg",
                "Meisterschaft",
            };
        }

        private void ReadInAllManagerZiele()
        {
            this.AllManagerZiele = new List<string>();
            this.AllManagerZiele = this.AllVorstandZiele;
            this.AllManagerZiele.Add("Verein wechseln");
            this.AllManagerZiele.Add("Vor Maiberg landen");
        }

        private void ReadInAllKommentatoren()
        {
            this.AllKommentatoren = new ObservableCollection<Kommentar>();
            FilePathesBuilder fb = new FilePathesBuilder();
            for (int i = 1; i < fb.GetLastSeason(); i++)
            {
                string vorberichtStatistikFile = fb.GetStatistikTextFilePath(i, "Vorbericht");
                FindKommenatorsInTextFile(vorberichtStatistikFile);

                string transferStatistikFile = fb.GetStatistikTextFilePath(i, "Transfers");
                FindKommenatorsInTextFile(transferStatistikFile);
            }
        }

        private void FindKommenatorsInTextFile(string filePath)
        {
            string[] fileContent = File.ReadAllLines(filePath, Encoding.Default);
            bool isKommentar = false;
            foreach (string line in fileContent)
            {
                if (line.StartsWith("-Kommentar"))
                {
                    isKommentar = true;
                }
                if (!line.StartsWith("Name:") || !isKommentar)
                {
                    continue;
                }
                Kommentar kommentator = new Kommentar {Name = line.Split(new char[] {':'}, 2)[1]};
                if (!this.AllKommentatoren.Any(k => kommentator.Name.Equals(k.Name)))
                {
                    this.AllKommentatoren.Add(kommentator);
                    isKommentar = false;
                }
            }
        }

        private void SetKommenatorBildPfad(Kommentar kommentator)
        {
            FilePathesBuilder fb = new FilePathesBuilder();
            string kommentatorPictureFolder = fb.GetKommentatorenPictureFolder();
            string kommentatorPicturePath = Path.Combine(kommentatorPictureFolder,
                kommentator.Name.Replace(" ", string.Empty) + ".jpg");
            if (!File.Exists(kommentatorPicturePath))
            {
                kommentatorPicturePath = kommentatorPicturePath.Replace(".jpg", ".png");
            }
            kommentator.PicturePath = kommentatorPicturePath;
        }
    }
}