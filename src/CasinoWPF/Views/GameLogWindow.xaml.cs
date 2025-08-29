using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CasinoWPF.Services;
using LiveCharts;
using LiveCharts.Wpf;

namespace CasinoWPF.Views
{
    public partial class GameLogWindow : Window
    {
        public GameLogWindow()
        {
            InitializeComponent();

            this.DataContext = GameLogService.Instance;


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

            GameLogService.Instance.PropertyChanged += (s, e) =>
            {
                // update chart if relevant properties change
                if (e.PropertyName == nameof(GameLogService.TotalWon) ||
                    e.PropertyName == nameof(GameLogService.TotalSpent) ||
                    e.PropertyName == nameof(GameLogService.GameLogEntries))
                {
                    UpdateChart();
                }
            };

            UpdateChart();
        }

        private void UpdateChart()
        {
            var seriesCollection = new SeriesCollection();

            var total = GameLogService.Instance.TotalWon + GameLogService.Instance.TotalSpent;

            if (total > 0)
            {
                seriesCollection.Add(new PieSeries
                {
                    Title = $"Gewonnen: {GameLogService.Instance.TotalWon:C}",
                    Values = new ChartValues<int> { GameLogService.Instance.TotalWon },
                    DataLabels = true,
                    LabelPoint = point => $"{point.Y:C} ({(point.Y / total):P1})",
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#27AE60")
                });

                seriesCollection.Add(new PieSeries
                {
                    Title = $"Verloren: {GameLogService.Instance.TotalSpent:C}",
                    Values = new ChartValues<int> { GameLogService.Instance.TotalSpent },
                    DataLabels = true,
                    LabelPoint = point => $"{point.Y:C} ({(point.Y / total):P1})",
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#E74C3C")
                });
            }
            else
            {
                // Fallback, if no data is available
                seriesCollection.Add(new PieSeries
                {
                    Title = "Keine Daten",
                    Values = new ChartValues<decimal> { 100 },
                    DataLabels = false,
                    Fill = Brushes.LightGray
                });
            }

            WinLossChart.Series = seriesCollection;
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