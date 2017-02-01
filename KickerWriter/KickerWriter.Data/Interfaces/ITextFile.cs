namespace KickerWriter.Data.Interfaces
{
    public interface ITextFile
    {
        string GetFilePath();

        string GetFileName();

        string GetCorrespondingLatexTemplateFilePath();
    }
}