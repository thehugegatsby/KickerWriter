namespace KickerWriter.Data.Interfaces
{
    using KickerWriter.Data.Models;

    public interface ISpecialFile
    {
        void FillSpecialData(SeasonLatexFile seasonLatexFile, StringTupleLists stringTupleLists, GlobalData globalData);
    }
}