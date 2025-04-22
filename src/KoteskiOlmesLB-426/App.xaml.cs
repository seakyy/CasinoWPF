using KoteskiOlmesLB_426.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace KoteskiOlmesLB_426
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
