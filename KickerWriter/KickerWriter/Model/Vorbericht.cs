using System.Collections.ObjectModel;

namespace KickerWriter.Model
{
    public class Vorbericht
    {
        public Vorbericht()
        {
            Mitspieler = new ObservableCollection<VorberichtMitspieler>();
            Kommentar = new Kommentar();
        }

        public string Header { get; set; }
        public ObservableCollection<VorberichtMitspieler> Mitspieler { get; set; }
        public Kommentar Kommentar { get; set; }
    }
}
