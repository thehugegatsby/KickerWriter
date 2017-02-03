using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using KickerWriter.Data;
using KickerWriter.Model;

namespace KickerWriter.Features.Vorbericht
{
    public sealed class VorberichtViewModel : ViewModelBase
    {
        private Season _selectedSeason;

        public Season SelectedSeason
        {
            get { return this._selectedSeason; }

            set
            {
                Set(ref _selectedSeason, value);
                RaisePropertyChanged(string.Empty);
            }
        }



        public string Header
        {
            get { return this.SelectedSeason.Vorbericht.Header; }

            set
            {
                if (this.SelectedSeason.Vorbericht.Header == value) return;
                this.SelectedSeason.Vorbericht.Header = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<VorberichtMitspieler> Mitspieler
        {
            get { return this.SelectedSeason.Vorbericht.Mitspieler; }

            set
            {
                if (this.SelectedSeason.Vorbericht.Mitspieler == value) return;
                this.SelectedSeason.Vorbericht.Mitspieler = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Model.Kommentar> Kommentare
        {
            get { return this.SelectedSeason.Vorbericht.Kommentare; }

            set
            {
                if (this.SelectedSeason.Vorbericht.Kommentare == value) return;
                this.SelectedSeason.Vorbericht.Kommentare = value;
                RaisePropertyChanged();
            }
        }
        public VorberichtViewModel(IDataService dataService)
        {
            Messenger.Default.Register<Season>
                (this, (season) => SelectedSeason = season);

            SelectedSeason = dataService.GetAllSeasons().ToList()[0];
        }
    }
}