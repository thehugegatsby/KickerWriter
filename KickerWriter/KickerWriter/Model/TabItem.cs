using GalaSoft.MvvmLight;

namespace KickerWriter.Model
{
    public sealed class TabItem
    {
        public string Header { get; set; }
        public ViewModelBase ViewModel { get; set; }
    }
}
