using CasinoWPF.Views;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CasinoWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Hauptfenster erzeugen und anzeigen
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Spielverlauf-Fenster erzeugen und anzeigen
            var logWindow = new GameLogWindow();
            logWindow.Show();
        }
    }
}
