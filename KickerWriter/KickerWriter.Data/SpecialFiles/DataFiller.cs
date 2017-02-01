namespace KickerWriter.Data
{
    using KickerWriter.Data.Interfaces;
    using KickerWriter.Data.Models;
    using KickerWriter.Utilities;
    using System.Collections.Generic;
    using System.IO.Abstractions;

    public class DataFiller
    {
        private readonly IFileSystem _fileSystem;

        public DataFiller()
            : this(new FileSystem())
        {
        }

        public DataFiller(IFileSystem fileSystem)
        {
            this._fileSystem = fileSystem;
        }

        public StringTupleLists GetTotalStringListsForSeasonLatexFile(SeasonLatexFile seasonLatexFile, GlobalData globalData)
        {
            StringTupleLists stringTupleListsTotal = new StringTupleLists();
            FillSeasonDataIntoStringTupleLists(stringTupleListsTotal, seasonLatexFile.SeasonYearOrNumber);
            foreach (var textFile in seasonLatexFile.CorrespondingStatistikTextFiles)
            {
                var statistikTextFile = (StatistikTextFile)textFile;
                stringTupleListsTotal.AddList(this.GetBasicStringListsForSingleTextFile(statistikTextFile));
            }

            FillSpecialFileData(seasonLatexFile, stringTupleListsTotal, globalData);
            return stringTupleListsTotal;
        }

        public StringTupleLists GetBasicStringListsForSingleTextFile(IGeneralFile file)
        {
            IEnumerable<string> fileContent = this.GetFileContent(file);
            FileContentParser fileContentParser = new FileContentParser();
            StringTupleLists stringTupleCollectionsList = new StringTupleLists
            {
                List = fileContentParser.GetStringTupleListFromFileContent(
                    fileContent,
                    file.Name)
            };
            return stringTupleCollectionsList;
        }

        private static void FillSeasonDataIntoStringTupleLists(StringTupleLists stringTupleLists, int seasonYearOrNumber)
        {
            SeasonCalculations seasonCalculations = new SeasonCalculations();
            stringTupleLists.AddStringPair(
                "Season_Number",
                seasonCalculations.GetNumberFromYearOrNumber(seasonYearOrNumber).ToString());
            stringTupleLists.AddStringPair(
                "Season_Year",
                seasonCalculations.GetYearFromYearOrNumber(seasonYearOrNumber).ToString());
        }

        private IEnumerable<string> GetFileContent(IGeneralFile file)
        {
            DataReader dataReader = new DataReader(this._fileSystem);
            string[] fileContent = dataReader.ReadSingleFileIntoStringArray(file);
            return fileContent;
        }

        private static void FillSpecialFileData(
            SeasonLatexFile seasonLatexFile,
            StringTupleLists stringTupleLists,
            GlobalData globalData)
        {
            seasonLatexFile.SpecialFile?.FillSpecialData(seasonLatexFile, stringTupleLists, globalData);
        }
    }
}