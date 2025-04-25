using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using KoteskiOlmesLB_426.Services;

namespace KoteskiOlmesLB_426.Views
{
    public partial class GameLogWindow : Window
    {
        public GameLogWindow()
        {
            InitializeComponent();

            try
            {
                this.Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/game_log_icon.ico"));
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show($"Fehler beim Setzen des Icons: {ex.Message}", "Icon-Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
        System.Diagnostics.Trace.WriteLine("Icon konnte nicht geladen werden: " + ex.Message);
#endif
            }
        }


        private void SaveGameLog_Click(object sender, RoutedEventArgs e)
        {
            var log = GameLogService.Instance.GameLogEntries;

            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON-Datei (*.json)|*.json",
                FileName = "GameLog.json"
            };

            if (dialog.ShowDialog() == true)
            {
                var json = System.Text.Json.JsonSerializer.Serialize(log, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(dialog.FileName, json);
                MessageBox.Show("Spielverlauf gespeichert!", "Export erfolgreich", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}