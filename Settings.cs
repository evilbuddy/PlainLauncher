using System.IO;
using IniParser;
using IniParser.Model;

/*
[PlainLauncher]
app = F:\jeux pc\DOOM\GZDoom\gzdoom.exe
argument = 
files = F:\jeux pc\DOOM\mods
*/

namespace PlainLauncher
{
    class Settings
    {
        public string AppPath { get; private set; }
        public string Argument { get; private set; }
        public string FilesPath { get; private set; }

        private void NewFile()
        {
            IniData data = new IniData();
            data["PlainLauncher"]["app"] = "C:\\path\\to\\app.exe";
            data["PlainLauncher"]["argument"] = "-arg";
            data["PlainLauncher"]["files"] = "C:\\path\\to\\files";

            File.WriteAllText("pl.ini", data.ToString());
        }

        public Settings()
        {
            if (!File.Exists("pl.ini"))
                NewFile();

            ReloadSettings();
        }

        public void ReloadSettings()
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData data = parser.ReadFile("pl.ini");

            AppPath = data["PlainLauncher"]["app"];
            Argument = data["PlainLauncher"]["argument"];
            FilesPath = data["PlainLauncher"]["files"];
        }

        public void SaveSettings(string app, string argument, string files)
        {
            IniData data = new IniData();
            data["PlainLauncher"]["app"] = app;
            data["PlainLauncher"]["argument"] = argument;
            data["PlainLauncher"]["files"] = files;

            File.WriteAllText("pl.ini", data.ToString());

            ReloadSettings();
        }
    }
}
