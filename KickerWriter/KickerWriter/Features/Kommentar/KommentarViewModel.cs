using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using KickerWriter.Data;

namespace KickerWriter.Features.Kommentar
{
    public class KommentarViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        public KommentarViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }


        public ObservableCollection<Model.Kommentar> Kommentare => _dataService.GetKommentare();
    }
    
}
