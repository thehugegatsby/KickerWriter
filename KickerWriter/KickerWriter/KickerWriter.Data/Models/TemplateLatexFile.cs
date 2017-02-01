namespace KickerWriter.Data.Models
{
    using KickerWriter.Data.Interfaces;

    public class TemplateLatexFile : IGeneralFile
    {
        public string Path { get; set; }

        public string Name { get; set; }
    }
}