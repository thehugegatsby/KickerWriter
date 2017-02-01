using System;
using System.IO;

namespace KickerWriter.Utilities
{
    public class KickerLatexPathDropbox : IKickerLatexPath
    {
        public string GetKickerLatexFolderPath()
        {
            string dropboxPath = this.GetDropboxFolderPath();
            string kickerLatexPath = Path.Combine(dropboxPath, "Anstoss_3", "KickerLatex");
            return kickerLatexPath;
        }

        public string GetDropboxFolderPath()
        {
            const string infoPath = @"Dropbox\info.json";

            var jsonPath = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), infoPath);

            if (!File.Exists(jsonPath))
            {
                jsonPath = Path.Combine(Environment.GetEnvironmentVariable("AppData"), infoPath);
            }

            if (!File.Exists(jsonPath))
            {
                throw new Exception("Dropbox could not be found!");
            }

            var dropboxPath = File.ReadAllText(jsonPath).Split('\"')[5].Replace(@"\\", @"\");
            return dropboxPath;
        }
    }
}