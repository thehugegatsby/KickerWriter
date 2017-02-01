namespace KickerWriter.Utilities
{
    public class SeasonCalculations
    {
        private const int StartYear = 2014;

        public int GetYearFromNumber(int seasonNumber)
        {
            int seasonYear = (seasonNumber - 1) + StartYear;
            return seasonYear;
        }

        public int GetNumberFromYear(int seasonYear)
        {
            int seasonNumber = (seasonYear + 1) - StartYear;
            return seasonNumber;
        }

        public int GetNumberFromYearOrNumber(int seasonYearOrNumber)
        {
            int seasonNumber = seasonYearOrNumber.ToString().Length == 4
                ? this.GetNumberFromYear(seasonYearOrNumber)
                : seasonYearOrNumber;

            return seasonNumber;
        }

        public int GetYearFromYearOrNumber(int seasonYearOrNumber)
        {
            int seasonYear = seasonYearOrNumber.ToString().Length == 4
                ? seasonYearOrNumber
                : this.GetYearFromNumber(seasonYearOrNumber);

            return seasonYear;
        }
    }
}