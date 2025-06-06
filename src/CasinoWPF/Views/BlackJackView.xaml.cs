﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CasinoWPF.Models;
using CasinoWPF.ViewModels;
using CasinoWPF.Services;


namespace CasinoWPF.Views
{
    public partial class BlackJackView : Page
    {
        public BlackJackView()
        {
            InitializeComponent();

            var player = Session.Instance.CurrentPlayer;
            var viewModel = new BlackJackViewModel(player); // Pass the player instance to the ViewModel
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

        private void OpenFeedback_Click(object sender, RoutedEventArgs e)
        {
            var feedbackWindow = new FeedbackWindow();
            feedbackWindow.ShowDialog();
        }
    }
}