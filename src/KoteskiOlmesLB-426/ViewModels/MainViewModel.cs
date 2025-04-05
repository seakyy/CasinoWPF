using KoteskiOlmesLB_426.Services;
using KoteskiOlmesLB_426.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace KoteskiOlmesLB_426.ViewModels
{
    public class MainViewModel : KoteskiOlmesLB_426.Services.IObserver<KoteskiOlmesLB_426.Services.SessionData>
    {
        private readonly Session _session;
        private string _playerName;
        private string _initialBalanceInput;
        private string _errorMessage;
        private bool _hasError;

        public string PlayerName
        {
            get => _playerName;
            set
            {
                if (_playerName != value)
                {
                    _playerName = value;
                    OnPropertyChanged();
                    ValidateInputs();
                }
            }
        }

        public string InitialBalanceInput
        {
            get => _initialBalanceInput;
            set
            {
                if (_initialBalanceInput != value)
                {
                    _initialBalanceInput = value;
                    OnPropertyChanged();
                    ValidateInputs();
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                    HasError = !string.IsNullOrEmpty(value);
                }
            }
        }

        public bool HasError
        {
            get => _hasError;
            set
            {
                if (_hasError != value)
                {
                    _hasError = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand StartGameCommand { get; }

        public event EventHandler<NavigationEventArgs> NavigationRequested;

        public MainViewModel()
        {
            _session = Session.Instance;
            _session.AddObserver(this);

            PlayerName = "Spieler";
            InitialBalanceInput = "1000";

            StartGameCommand = new RelayCommand(ExecuteStartGame, CanStartGame);
        }

        public void Update(SessionData data)
        {
            // optional implementieren, falls benötigt
        }

        private bool CanStartGame(object parameter)
        {
            return !HasError && !string.IsNullOrWhiteSpace(PlayerName) && int.TryParse(InitialBalanceInput, out int value) && value >= 100;
        }

        private void ExecuteStartGame(object parameter)
        {
            try
            {
                if (!int.TryParse(InitialBalanceInput, out int balance))
                {
                    ErrorMessage = "Ungültiger Betrag.";
                    return;
                }

                _session.StartNewSession(PlayerName, balance);

                OnNavigationRequested("GameSelection");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Fehler beim Starten des Spiels: " + ex.Message;
            }
        }

        private void ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                ErrorMessage = "Bitte gib einen Spielernamen ein.";
            }
            else if (!int.TryParse(InitialBalanceInput, out int value) || value < 100)
            {
                ErrorMessage = "Das Startguthaben muss mindestens 100 Jetons betragen.";
            }
            else
            {
                ErrorMessage = null;
            }
        }

        protected virtual void OnNavigationRequested(string targetView)
        {
            NavigationRequested?.Invoke(this, new NavigationEventArgs(targetView));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class NavigationEventArgs : EventArgs
    {
        public string TargetView { get; }
        public NavigationEventArgs(string targetView) => TargetView = targetView;
    }
}