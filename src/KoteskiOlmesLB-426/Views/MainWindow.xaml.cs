using KoteskiOlmesLB_426.Views;
using KoteskiOlmesLB_426.Services;
using KoteskiOlmesLB_426.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KoteskiOlmesLB_426.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private readonly Session _session;

        public MainWindow()
        {
            InitializeComponent();

            // Initialisiere das ViewModel und setze es als DataContext
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            // Hole die Session-Instanz
            _session = Session.Instance;

            // Konfiguriere die Fenstereigenschaften
            Title = $"Casino Royale - {DateTime.UtcNow:yyyy-MM-dd} - Benutzer: seakyy";

            // Event-Handler für Navigationsereignisse registrieren
            _viewModel.NavigationRequested += OnNavigationRequested;
        }

        /// <summary>
        /// Event-Handler für Navigationsereignisse
        /// </summary>
        private void OnNavigationRequested(object sender, NavigationEventArgs e)
        {
            switch (e.TargetView)
            {
                case "GameSelection":
                    NavigateToGameSelection();
                    break;
                    // Weitere Navigationsziele hier hinzufügen
            }
        }

        /// <summary>
        /// Navigiert zur Spielauswahl-Ansicht
        /// </summary>
        private void NavigateToGameSelection()
        {
            try
            {
                // In diesem einfachen Beispiel setzen wir den Content direkt
                // In einer realen Anwendung würdest du hier einen Frame verwenden
                var gameSelectionView = new GameSelectionView();

                // In einer realen Anwendung würdest du hier das ViewModel erstellen
                // und konfigurieren

                // Setze den Content des Fensters
                Content = gameSelectionView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler bei der Navigation: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Behandelt das Schließen des Fensters
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Bereinige alle Ressourcen und beende die Session
            _session.EndSession();

            // Entregistriere Event-Handler
            _viewModel.NavigationRequested -= OnNavigationRequested;
        }
    }
}