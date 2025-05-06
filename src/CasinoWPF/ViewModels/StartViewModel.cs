using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CasinoWPF.Services;
using CasinoWPF.Views;
using System.Windows.Controls;

namespace CasinoWPF.ViewModels
{
    public class StartViewModel : INotifyPropertyChanged
    {
        private string _playerName;
        private string _initialBalanceInput;
        private string _errorMessage;
        private bool _hasError;

        public string PlayerName
        {
            get => _playerName;
            set { _playerName = value; OnPropertyChanged(); }
        }

        public string InitialBalanceInput
        {
            get => _initialBalanceInput;
            set { _initialBalanceInput = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; HasError = !string.IsNullOrWhiteSpace(value); OnPropertyChanged(); }
        }

        public bool HasError
        {
            get => _hasError;
            set { _hasError = value; OnPropertyChanged(); }
        }

        public ICommand StartGameCommand { get; }

        public StartViewModel()
        {
            StartGameCommand = new RelayCommand(ExecuteStartGame);
        }

        private void ExecuteStartGame(object obj)
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                ErrorMessage = "Bitte gib einen Spielernamen ein.";
                return;
            }

            if (!int.TryParse(InitialBalanceInput, out int balance) || balance < 100)
            {
                ErrorMessage = "Bitte gib ein gültiges Guthaben ab 100 Jetons ein.";
                return;
            }

            // Alles OK → Spielsession starten und navigieren
            Session.Instance.StartNewSession(PlayerName, balance);

            // Navigate using MainWindow's Frame
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.MainFrame != null)
            {
                mainWindow.MainFrame.Navigate(new GameSelectionView());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}