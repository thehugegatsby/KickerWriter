using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using KickerWriter.Model;

namespace KickerWriter.Features.Main
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        private ObservableCollection<Season> _allSeasons;

        public ObservableCollection<Season> AllSeasons
        {
            get { return this._allSeasons; }

            set
            {
                Set(ref _allSeasons, value);
            }
        }

        private Season _selectedSeason;

        public Season SelectedSeason
        {
            get { return this._selectedSeason; }

            set
            {
                Set(ref _selectedSeason, value);
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
            this.AllSeasons = new ObservableCollection<Season>(dataService.GetAllSeasons());
            this.SelectedSeason = IsInDesignModeStatic ? this.AllSeasons[1] : this.AllSeasons[0];
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}