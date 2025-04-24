using KoteskiOlmesLB_426.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using KoteskiOlmesLB_426.ViewModels;
using System.Windows.Media.Animation;

namespace KoteskiOlmesLB_426.Views
{
    public partial class SlotMachineView : Page
    {
        public SlotMachineView()
        {

            InitializeComponent();

            var viewModel = new SlotMachineViewModel();
            viewModel.NavigationRequested += ViewModel_NavigationRequested;

            DataContext = viewModel;
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
            MessageBox.Show(
                "🎰 AutoSpin Anleitung:\n" +
                "• 1x drücken = AutoSpin startet\n" +
                "• erneut drücken = AutoSpin stoppt\n\n" +
                "💎 Gewinnquoten der Symbole:\n" +
                "• Cherry 🍒 → 2x\n" +
                "• Lemon 🍋 → 2x\n" +
                "• Orange 🍊 → 3x\n" +
                "• Plum 🍑 → 3x\n" +
                "• Bell 🔔 → 4x\n" +
                "• Bar 🟦 → 5x\n" +
                "• Seven 7️ → 10x\n" +
                "• Diamond 💎 → 15x",
                "AutoSpin & Gewinnquoten",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }


    }
}
