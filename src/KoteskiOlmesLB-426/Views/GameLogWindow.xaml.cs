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
                // Log the exception or handle it gracefully
                MessageBox.Show($"Failed to load the window icon: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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