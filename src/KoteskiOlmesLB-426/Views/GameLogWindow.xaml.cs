using System.IO;
using System.Windows;
using KoteskiOlmesLB_426.Services;

namespace KoteskiOlmesLB_426.Views
{
    public partial class GameLogWindow : Window
    {
        public GameLogWindow()
        {
            InitializeComponent();
            DataContext = GameLogService.Instance;
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

