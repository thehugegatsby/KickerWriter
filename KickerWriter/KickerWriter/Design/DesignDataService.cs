using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KickerWriter.Model;

namespace KickerWriter.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }


        public IEnumerable<Season> GetAllSeasons()
        {
            ObservableCollection<Season> allSeasons = new ObservableCollection<Season>();
            for (int seasonNumber = 1; seasonNumber <= 5; seasonNumber++)
            {
                Season season = new Season {Number = seasonNumber};
                allSeasons.Add(season);
            }
            return allSeasons;
        }
    }
}