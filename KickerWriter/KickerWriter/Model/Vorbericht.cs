using System.Collections.ObjectModel;

namespace KickerWriter.Model
{
    public class Vorbericht
    {
        public Vorbericht()
        {
            Mitspieler = new ObservableCollection<VorberichtMitspieler>();
            Kommentare = new ObservableCollection<Kommentar> {new Kommentar()};
        }

        public string Header { get; set; }
        public ObservableCollection<VorberichtMitspieler> Mitspieler { get; set; }
        public ObservableCollection<Kommentar> Kommentare { get; set; }
    }
}
