using System.IO;
using KickerWriter.Utilities;

namespace KickerWriter.Model
{
    public class Kommentar

    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set {_name = value; PicturePath = GetKommenatorBildPfad(value);}
        }

        public string Text { get; set; }
      
        public string PicturePath { get; set; }
        

        private static string GetKommenatorBildPfad(string kommentatorName)
        {
            FilePathesBuilder filePathesBuilder = new FilePathesBuilder();
            string kommentatorPictureFolder = filePathesBuilder.GetKommentatorenPictureFolder();
            string kommentatorPicturePath = Path.Combine(kommentatorPictureFolder, kommentatorName.Replace(" ", string.Empty) + ".jpg");
            if (!File.Exists(kommentatorPicturePath))
            {
                kommentatorPicturePath = kommentatorPicturePath.Replace(".jpg", ".png");
            }
            return kommentatorPicturePath;
        }
    }
}
