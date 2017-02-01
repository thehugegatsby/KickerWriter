namespace KickerWriter.Data
{
    using KickerWriter.Data.Interfaces;
    using KickerWriter.Data.Models;
    using System;
    using System.IO.Abstractions;
    using System.Text;

    public class DataWriter
    {
        private readonly DataReader _dataReader;

        public DataWriter()
            : this(new FileSystem())
        {
        }

        public DataWriter(IFileSystem fileSystem)
        {
            this.FileSystem = fileSystem;
            this._dataReader = new DataReader(fileSystem);
        }

        public IFileSystem FileSystem { get; set; }

        public void WriteFromStringListsToLatexFile(
            IGeneralFile templateFile,
            IGeneralFile writeFile,
            StringTupleLists stringTupleLists)
        {
            this.FileSystem.File.WriteAllLines(
                path: writeFile.Path,
                contents: this.GetReplacedFileContent(templateFile, stringTupleLists),
                encoding: Encoding.GetEncoding(1252));
        }

        public void WriteNewFileContentToLatexFile(string[] newFileContent, string filePath)
        {
            this.FileSystem.File.WriteAllLines(
                path: filePath,
                contents: newFileContent,
                encoding: Encoding.GetEncoding(1252));
        }

        private string[] GetReplacedFileContent(IGeneralFile file, StringTupleLists stringTupleLists)
        {
            string[] fileContent = this._dataReader.ReadSingleFileIntoStringArray(file);
            for (int i = 0; i < fileContent.Length; i++)
            {
                foreach (Tuple<string, string> stringTuple in stringTupleLists.List)
                {
                    fileContent[i] = fileContent[i].Replace(stringTuple.Item1, stringTuple.Item2);
                }
            }

            return fileContent;
        }
    }
}