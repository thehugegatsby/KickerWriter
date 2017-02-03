using System.Collections.Generic;
using System.Collections.ObjectModel;
using KickerWriter.Model;

namespace KickerWriter.Data
{
    public interface IDataService
    {
        IEnumerable<Season> GetAllSeasons();
        ObservableCollection<Kommentar> GetKommentare();

    }
}
