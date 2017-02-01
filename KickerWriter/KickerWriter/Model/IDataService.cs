using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KickerWriter.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
        IEnumerable<Season> GetAllSeasons();
    }
}
