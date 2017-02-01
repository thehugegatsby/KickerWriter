namespace KickerWriter.Data.Models
{
    using KickerWriter.Data.Interfaces;
    using KickerWriter.Data.SpecialFiles;
    using KickerWriter.Utilities;
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;

    public class SeasonLatexFile : IGeneralFile
    {
        private readonly List<Tuple<string, List<string>>> _correspondingTextFilesNameListglobal =
            new List<Tuple<string, List<string>>>()
            {
                Tuple.Create(
                    "Statistik_International",
                    new List<string>() {"Torjaeger", "Europa_Cups", "Sonstige_Titel"})
            };

        private readonly FilePathesBuilder _filePathesBuilder;

        private readonly IFileSystem _fileSystem;

        public SeasonLatexFile(string seasonLatexFileName, int seasonYearOrNumber, FilePathesBuilder filePathesBuilder)
        {
            this.Name = seasonLatexFileName;
            this.SeasonYearOrNumber = seasonYearOrNumber;
            this._filePathesBuilder = filePathesBuilder;
            this.GetPath();

            this._fileSystem = new FileSystem();
            this.Initialize();
        }

        public SeasonLatexFile(string seasonLatexFileName, int seasonYearOrNumber, FilePathesBuilder filePathesBuilder,
            IFileSystem fileSystem)
        {
            this.Name = seasonLatexFileName;
            this.SeasonYearOrNumber = seasonYearOrNumber;
            this._filePathesBuilder = filePathesBuilder;
            this.GetPath();

            this._fileSystem = fileSystem;
            this.Initialize();
        }

        public SeasonLatexFile(string seasonLatexFileName, int seasonYearOrNumber)
        {
            this.Name = seasonLatexFileName;
            this.SeasonYearOrNumber = seasonYearOrNumber;
            this._filePathesBuilder = new FilePathesBuilder();
            this.GetPath();

            this._fileSystem = new FileSystem();
            this.Initialize();
        }

        public ISpecialFile SpecialFile { get; set; }

        public int SeasonYearOrNumber { get; set; }

        public List<ITextFile> CorrespondingStatistikTextFiles { get; set; }

        public TemplateLatexFile CorrespondingLatexTemplateFile { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public void WriteFile(GlobalData globalData)
        {
            DataFiller dataFiller = new DataFiller(this._fileSystem);
            StringTupleLists stringTupleLists = dataFiller.GetTotalStringListsForSeasonLatexFile(this, globalData);

            DataWriter dataWriter = new DataWriter(this._fileSystem);
            dataWriter.WriteFromStringListsToLatexFile(this.CorrespondingLatexTemplateFile, this, stringTupleLists);
        }

        private void Initialize()
        {
            this.FillCorrespondingStatistikTextFiles(this.SeasonYearOrNumber);
            this.FillCorrespondingLatexTemplateFile();
            this.SetSpecialFile();
        }

        private void SetSpecialFile()
        {
            if (this.Name == "Direkte_Duelle")
            {
                this.SpecialFile = new DirekteDuelle();
            }
            else
            {
                this.SpecialFile = null;
            }
        }

        private void GetPath()
        {
            this.Path = this._filePathesBuilder.GetSeasonLatexFilePath(this.SeasonYearOrNumber, this.Name);
        }

        private void FillCorrespondingStatistikTextFiles(int seasonYearOrNumber)
        {
            this.CorrespondingStatistikTextFiles = new List<ITextFile>();
            if (this._correspondingTextFilesNameListglobal.Any(t => t.Item1 == this.Name))
            {
                int index = this._correspondingTextFilesNameListglobal.FindIndex(t => t.Item1 == this.Name);
                List<string> textFileNames = this._correspondingTextFilesNameListglobal[index].Item2;
                foreach (string textfileName in textFileNames)
                {
                    this.CorrespondingStatistikTextFiles.Add(new StatistikTextFile(
                        name: textfileName,
                        seasonYearOrNumber: seasonYearOrNumber,
                        filePathesBuilder: this._filePathesBuilder));
                }
            }
            else
            {
                this.CorrespondingStatistikTextFiles.Add(new StatistikTextFile(
                    name: this.Name,
                    seasonYearOrNumber: seasonYearOrNumber,
                    filePathesBuilder: this._filePathesBuilder));
            }
        }

        private void FillCorrespondingLatexTemplateFile()
        {
            TemplateLatexFile templateLatexFile = new TemplateLatexFile
            {
                Path = this._filePathesBuilder.GetTemplateFilePath(this.Name)
            };
            this.CorrespondingLatexTemplateFile = templateLatexFile;
        }
    }
}