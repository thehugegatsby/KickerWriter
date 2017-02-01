namespace KickerWriter.Data.Models
{
    using KickerWriter.Data.Interfaces;
    using System.Collections.Generic;

    public class StatistikTextFileList
    {
        public StatistikTextFileList(int seasonYearOrNumber)
        {
            this.FillStatistikFileNameList(seasonYearOrNumber);
            this._seasonYearOrNumber = seasonYearOrNumber;
        }

        public List<ITextFile> FileList { get; set; }

        private int _seasonYearOrNumber;

        private void FillStatistikFileNameList(int seasonYearOrNumber)
        {
            List<string> statistikFileNameList = new List<string>()
            {
                "Direkte_Duelle",
                "Europa_Cups",
                "Pokalsieger",
                "Saisonverlauf",
                "Sonstige_Titel",
                "Sonstiges",
                "Torjaeger",
                "Transfers",
                "Vorbericht"
            };

            this.FileList = new List<ITextFile>();
            foreach (string statistikFileName in statistikFileNameList)
            {
                StatistikTextFile statistikTextFile = new StatistikTextFile(statistikFileName, seasonYearOrNumber);
                this.FileList.Add(statistikTextFile);
            }
        }
    }
}