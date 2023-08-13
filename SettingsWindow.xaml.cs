using System.IO;
using System.Windows;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace PlainLauncher
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public bool saved { get; private set; } = false;
        Settings settings;

        public SettingsWindow()
        {
            InitializeComponent();

            settings = new Settings();

            app.Text = settings.AppPath;
            argument.Text = settings.Argument;
            files.Text = settings.FilesPath;
        }

        private void app_browse(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dial = new OpenFileDialog();
            dial.Filter = "Windows Executables|*.exe|All files|*";
            dial.DefaultExt = ".exe";
            dial.Title = "Browse File";
            dial.ShowDialog();

            app.Text = dial.FileName;
        }

        private void files_browse(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dial = new CommonOpenFileDialog();
            dial.IsFolderPicker = true;
            dial.Title = "Browse Folder";
            dial.ShowDialog();

            files.Text = dial.FileName;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            saved = true;
            settings.SaveSettings(app.Text, argument.Text, files.Text);
            this.Close();
        }
    }
}
