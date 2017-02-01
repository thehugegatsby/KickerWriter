namespace KickerWriter.Data.Models.Soccer
{
    using KickerWriter.Utilities;
    using System.Data;

    public class Table
    {
        private DataTable dataTable;
        private FilePathesBuilder _filePathesBuilder;

        public Table()
        {
            this._filePathesBuilder = new FilePathesBuilder();
        }
    }
}