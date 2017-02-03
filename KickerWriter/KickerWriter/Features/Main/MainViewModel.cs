using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using KickerWriter.Data;
using KickerWriter.Features.Vorbericht;
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

        private ObservableCollection<Season> _allSeasons;

        public ObservableCollection<Season> AllSeasons
        {
            get { return this._allSeasons; }

            set { Set(ref _allSeasons, value); }
        }

        private Season _selectedSeason;

        public Season SelectedSeason
        {
            get { return this._selectedSeason; }

            set
            {
                Set(ref _selectedSeason, value);
                Messenger.Default.Send<Season>(_selectedSeason);
            }
        }

        public ObservableCollection<TabItem> Tabs { get; set; }
        private TabItem _selectedTab;

        public TabItem SelectedTab
        {
            get { return this._selectedTab; }

            set
            {
                if (this._selectedTab == value) return;
                this._selectedTab = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            this.AllSeasons = new ObservableCollection<Season>(dataService.GetAllSeasons());
            this.SelectedSeason = IsInDesignModeStatic ? this.AllSeasons[1] : this.AllSeasons[0];

            Tabs = new ObservableCollection<TabItem>
            {
                new TabItem {Header = "Vorbericht", ViewModel = SimpleIoc.Default.GetInstance<VorberichtViewModel>()}
            };
            SelectedTab = Tabs[0];
        }
    }
}
        
