using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlainLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Settings settings;
        string[] files;

        public MainWindow()
        {
            InitializeComponent();

            settings = new Settings();

            UpdateFiles();
        }

        void UpdateFiles()
        {
            if (!Directory.Exists(settings.FilesPath))
                return;

            files = Directory.GetFileSystemEntries(settings.FilesPath, "*", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                string name = "";

                if (File.GetAttributes(file).HasFlag(FileAttributes.Directory))
                {
                    name = new DirectoryInfo(file).Name;
                }
                else
                {
                    name = new FileInfo(file).Name;
                }

                ListBoxItem item = new ListBoxItem();
                item.Content = name;

                Files.Items.Add(item);
            }
        }

        void OpenSettings(object sender, RoutedEventArgs e)
        {
            SettingsWindow win = new SettingsWindow();
            win.ShowDialog();

            if(win.saved)
                settings.ReloadSettings();

            Files.Items.Clear();
            UpdateFiles();
        }

        void Launch(object sender, MouseButtonEventArgs e)
        {
            string app = settings.AppPath;
            string arg = settings.Argument;
            string file = files[Files.SelectedIndex];

            string cmd = "";

            if (arg == "")
                cmd = file;
            else if (arg.Contains("%F") || arg.Contains("%f"))
                cmd = arg.Replace("%F", file).Replace("%f", file);
            else
                cmd = arg + " " + file;

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = app;
            info.UseShellExecute = true;
            info.Arguments = cmd;

            Process.Start(info);

            this.Close();
        }
    }
}
