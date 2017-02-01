namespace KickerWriter.Data
{
    using KickerWriter.Data.Interfaces;
    using System.IO.Abstractions;
    using System.Text;

    public class DataReader
    {
        private readonly IFileSystem _fileSystem;

        public DataReader()
            : this(new FileSystem())
        {
        }

        public DataReader(IFileSystem fileSystem)
        {
            this._fileSystem = fileSystem;
        }

        public string[] ReadSingleFileIntoStringArray(IGeneralFile file)
        {
            string[] fileContentStringArray = this._fileSystem.File.ReadAllLines(
                path: file.Path,
                encoding: Encoding.GetEncoding(1252));
            return fileContentStringArray;
        }
    }
}