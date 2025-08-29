using CasinoWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CasinoWPF.Views
{
    public partial class SlotMachineView : Page
    {
        private SlotMachineViewModel ViewModel => DataContext as SlotMachineViewModel;

        public SlotMachineView()
        {
            InitializeComponent();

            var viewModel = new SlotMachineViewModel();
            viewModel.NavigationRequested += ViewModel_NavigationRequested;
            DataContext = viewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.Reel1ImageControl = Reel1ImageControl;
                ViewModel.Reel2ImageControl = Reel2ImageControl;
                ViewModel.Reel3ImageControl = Reel3ImageControl;
            }
        }

        private void ViewModel_NavigationRequested(object sender, NavigationEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow == null || mainWindow.MainFrame == null)
                return;

            switch (e.TargetView)
            {
                case "GameSelection":
                    mainWindow.MainFrame.Navigate(new GameSelectionView());
                    break;
                case "MainMenu":
                    mainWindow.MainFrame.Navigate(new StartPage());
                    break;
            }
        }

        private void SpinButton_Click(object sender, RoutedEventArgs e)
        {
            
            TwoLeftIndicator.Visibility = Visibility.Collapsed;
            TwoRightIndicator.Visibility = Visibility.Collapsed;
            TwoOuterIndicator.Visibility = Visibility.Collapsed;
            ThreeIndicator.Visibility = Visibility.Collapsed;

            AnimateReel(Reel1ImageControl);
            AnimateReel(Reel2ImageControl);
            AnimateReel(Reel3ImageControl);
        }

        private void AnimateReel(UIElement element)
        {
            var storyboard = (Storyboard)FindResource("ShakeReel");
            Storyboard.SetTarget(storyboard, element);
            storyboard.Begin();
        }

        private void AutoSpinInfo_Click(object sender, RoutedEventArgs e)
        {
            string message = @"SLOT MACHINE GEWINNKOMBINATIONEN:

3 GLEICHE SYMBOLE (JACKPOT):
Kirschen: 2x Einsatz
Zitronen: 2x Einsatz  
Orangen: 3x Einsatz
Pflaumen: 3x Einsatz
Glocken: 4x Einsatz
Bars: 5x Einsatz
Siebener: 10x Einsatz
Diamanten: 15x Einsatz

2 GLEICHE SYMBOLE:
Kirschen: 0.5x Einsatz
Zitronen: 0.5x Einsatz
Orangen: 1x Einsatz
Pflaumen: 1x Einsatz
Glocken: 1.5x Einsatz
Bars: 2x Einsatz
Siebener: 3x Einsatz
Diamanten: 5x Einsatz

AutoSpin:
• 1x drücken = AutoSpin startet
• erneut drücken = AutoSpin stoppt";

            MessageBox.Show(message, "Slot Machine Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OpenFeedback_Click(object sender, RoutedEventArgs e)
        {
            var feedbackWindow = new FeedbackWindow();
            feedbackWindow.ShowDialog();
        }
    }
}
