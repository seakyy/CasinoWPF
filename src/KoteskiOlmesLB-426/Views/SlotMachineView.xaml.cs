using KoteskiOlmesLB_426.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using KoteskiOlmesLB_426.ViewModels;

namespace KoteskiOlmesLB_426.Views
{
    public partial class SlotMachineView : Page
    {
        public SlotMachineView()
        {

            InitializeComponent();

            var viewModel = new BlackJackViewModel();
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
            // Add logic for spinning the slot machine
        }
    }
}
