using System;
using System.Data;
using System.IO;
using System.Text;
using KickerWriter.Utilities;

namespace KickerWriter.Model
{
    public class League
    {
        public League(string name)
        {
            this.Name = name;
            this.IsSelected = false;
        }

        public League LeagueUp
        {
            get
            {
                switch (this.Name)
                {
                    case "Premier League":
                        return null;
                    case "Championship":
                        return new League("Premier League");
                    case "League One":
                        return new League("Championship");
                    case "League Two":
                        return new League("League One");
                    default:
                        return null;
                }
            }
        }

        public League LeagueDown
        {
            get
            {
                switch (this.Name)
                {
                    case "Premier League":
                        return new League("Championship");
                    case "Championship":
                        return new League("League One");
                    case "League One":
                        return new League("League Two");
                    case "League Two":
                        return null;
                    default:
                        return null;
                }
            }
        }

        public int Level { get; set; }

        public string Name { get; }

        public bool IsBonusLand { get; set; }

        public bool IsSelected { get; set; }

        public DataTable GetDataTable(int seasonYearOrNumber)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Place", typeof(int));
            dt.Columns.Add("Team", typeof(string));
            dt.Columns.Add("GoalsScored", typeof(int));
            dt.Columns.Add("GoalsReceived", typeof(int));
            dt.Columns.Add("Points", typeof(int));

            FilePathesBuilder fb = new FilePathesBuilder();
            string tableFilePath = fb.GetTableFilePath(seasonYearOrNumber, this.Name);
            var tableContent = File.ReadAllLines(tableFilePath, Encoding.Default);
            for (int i = 0; i < tableContent.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }

            int rowCounter = 0;
            foreach (string untrimmedLine in tableContent)
            {
                string line = untrimmedLine.Trim();

                int positionOfLastDoublePoint = line.LastIndexOf(":", StringComparison.Ordinal);
                int positionOfLastWhitespace = line.LastIndexOf(' ');
                int positionOfSecondLastWhitespace = line.LastIndexOf(' ', positionOfLastWhitespace - 1);
                int positionOfFirstDot = line.IndexOf('.');
                if (positionOfFirstDot == -1)
                {
                    positionOfFirstDot = line.IndexOf(' ');
                }

                int place = int.Parse(line.Substring(0, positionOfFirstDot));
                int points = int.Parse(line.Substring(positionOfLastWhitespace));

                int goalsReceived =
                    int.Parse(line.Substring(
                        positionOfLastDoublePoint + 1,
                        positionOfLastWhitespace - positionOfLastDoublePoint - 1));
                var a = line.Substring(
                    positionOfSecondLastWhitespace + 1,
                    positionOfLastDoublePoint - positionOfSecondLastWhitespace - 1);
                int goalsScored =
                    int.Parse(line.Substring(
                        positionOfSecondLastWhitespace + 1,
                        positionOfLastDoublePoint - positionOfSecondLastWhitespace - 1));
                string teamName = line.Substring(
                    positionOfFirstDot + 2,
                    positionOfSecondLastWhitespace - positionOfFirstDot - 2);

                dt.Rows[rowCounter]["Place"] = place;
                dt.Rows[rowCounter]["Team"] = teamName;
                dt.Rows[rowCounter]["GoalsScored"] = goalsScored;
                dt.Rows[rowCounter]["GoalsReceived"] = goalsReceived;
                dt.Rows[rowCounter]["Points"] = points;
                rowCounter++;
            }

            return dt;
        }
    }
}