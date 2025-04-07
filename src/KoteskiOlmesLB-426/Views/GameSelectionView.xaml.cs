using KoteskiOlmesLB_426.ViewModels;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KoteskiOlmesLB_426.Views
{
    public partial class GameSelectionView : Page
    {
        public GameSelectionView()
        {
            InitializeComponent();

            var viewModel = new GameSelectionViewModel();
            viewModel.NavigationRequested += ViewModel_NavigationRequested;

            DataContext = viewModel;
        }

        private void ViewModel_NavigationRequested(object sender, NavigationEventArgs e)
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;

            if (mainWindow != null && mainWindow.MainFrame != null)
            {
                switch (e.TargetView)
                {
                    case "BlackJack":
                        mainWindow.MainFrame.Navigate(new BlackJackView());
                        break;
                    case "SlotMachine":
                        mainWindow.MainFrame.Navigate(new SlotMachineView()); // Wenn du die View schon hast
                        break;
                    case "MainMenu":
                        mainWindow.MainFrame.Navigate(new StartPage());
                        break;
                }
            }
        }
    }
}
