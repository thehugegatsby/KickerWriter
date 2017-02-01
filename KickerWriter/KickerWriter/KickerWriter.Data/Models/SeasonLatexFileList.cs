namespace KickerWriter.Data.Models
{
    using KickerWriter.Utilities;
    using System.Collections.Generic;
    using System.IO.Abstractions;

    public class SeasonLatexFileList
    {
        private readonly FilePathesBuilder _filePathesBuilder;

        private readonly IFileSystem _fileSystem;

        private readonly int _seasonYearOrNumber;

        public SeasonLatexFileList(int seasonYearOrNumber)
        {
            this._seasonYearOrNumber = seasonYearOrNumber;
            this._filePathesBuilder = new FilePathesBuilder(new KickerLatexPathDropbox());
            this._fileSystem = new FileSystem();

            this.FillSeasonLatexList();
        }

        public SeasonLatexFileList(int seasonYearOrNumber, FilePathesBuilder filePathesBuilder, IFileSystem fileSystem)
        {
            this._seasonYearOrNumber = seasonYearOrNumber;
            this._filePathesBuilder = filePathesBuilder;
            this._fileSystem = fileSystem;

            this.FillSeasonLatexList();
        }

        public List<SeasonLatexFile> SeasonLatexList { get; set; }

        public void WriteAllFiles(GlobalData globalData)
        {
            foreach (SeasonLatexFile seasonLatexFile in this.SeasonLatexList)
            {
                seasonLatexFile.WriteFile(globalData);
            }
        }

        private void FillSeasonLatexList()
        {
            this.SeasonLatexList = new List<SeasonLatexFile>();
            foreach (string seasonFileName in GlobalData.SeasonFileNameList)
            {
                this.SeasonLatexList.Add(new SeasonLatexFile(
                    seasonFileName,
                    this._seasonYearOrNumber,
                    this._filePathesBuilder,
                    this._fileSystem));
            }
        }
    }
}