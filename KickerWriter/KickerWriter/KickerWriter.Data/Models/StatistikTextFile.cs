namespace KickerWriter.Data.Models
{
    using KickerWriter.Data.Interfaces;
    using KickerWriter.Utilities;
    using System.Collections.Generic;
    using System.Linq;

    public class StatistikTextFile : ITextFile, IGeneralFile
    {
        private readonly FilePathesBuilder _filePathesBuilder;

        private readonly int _seasonYearOrNumber;

        public StatistikTextFile(string name, int seasonYearOrNumber)
        {
            this._filePathesBuilder = new FilePathesBuilder(new KickerLatexPathDropbox());
            this.Name = name;
            this._seasonYearOrNumber = seasonYearOrNumber;
            this.SetPath();
        }

        public StatistikTextFile(string name, int seasonYearOrNumber, FilePathesBuilder filePathesBuilder)
        {
            this._filePathesBuilder = filePathesBuilder;
            this.Name = name;
            this._seasonYearOrNumber = seasonYearOrNumber;
            this.SetPath();
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public List<string> StatistikFileNameList { get; set; }

        public string GetFileName()
        {
            return this.Name;
        }

        public string GetFilePath()
        {
            return this._filePathesBuilder.GetStatistikTextFilePath(this._seasonYearOrNumber, this.Name);
        }

        public string GetCorrespondingLatexTemplateFilePath()
        {
            string[] statistikInternationalCorrespondingTextFiles = new[]
            {
                "Europa_Cups",
                "Torjaeger",
                "Sonstige_Titel"
            };

            if (statistikInternationalCorrespondingTextFiles.Contains(this.Name))
            {
                return this._filePathesBuilder.GetTemplateFilePath("Statistik_International");
            }
            else
            {
                return this._filePathesBuilder.GetTemplateFilePath(this.Name);
            }
        }

        private void SetPath()
        {
            this.Path = this._filePathesBuilder.GetStatistikTextFilePath(this._seasonYearOrNumber, this.Name);
        }
    }
}