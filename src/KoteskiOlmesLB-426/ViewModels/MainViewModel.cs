using KoteskiOlmesLB_426.Services;
using KoteskiOlmesLB_426.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace KoteskiOlmesLB_426.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IObserver<SessionData>
    {
        private readonly Session _session;
        private string _playerName;
        private int _initialBalance;
        private string _errorMessage;
        private bool _hasError;

        // Properties
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

        public int InitialBalance
        {
            get => _initialBalance;
            set
            {
                if (_initialBalance != value)
                {
                    _initialBalance = value;
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

        // Commands
        public ICommand StartGameCommand { get; }

        // Event für Navigation
        public event EventHandler<NavigationEventArgs> NavigationRequested;

        public MainViewModel()
        {
            _session = Session.Instance;
            _session.AddObserver(this);

            PlayerName = "Spieler";
            InitialBalance = 1000;

            StartGameCommand = new RelayCommand(ExecuteStartGame, CanStartGame);
        }

        // IObserver Implementation
        public void Update(SessionData data)
        {
            // Reagiere auf Änderungen in der Session
            // In diesem Fall könnten wir z.B. auf Änderungen am Spielerguthaben reagieren
        }

        private bool CanStartGame(object parameter)
        {
            return !HasError && !string.IsNullOrWhiteSpace(PlayerName) && InitialBalance > 0;
        }

        private void ExecuteStartGame(object parameter)
        {
            try
            {
                // Erstelle eine neue Spieler-Session
                _session.StartNewSession(PlayerName, InitialBalance);

                // Löse das Navigationsereignis aus
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
            else if (InitialBalance < 100)
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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    // Einfache RelayCommand-Implementierung
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    // Klasse für Navigation Events
    public class NavigationEventArgs : EventArgs
    {
        public string TargetView { get; }

        public NavigationEventArgs(string targetView)
        {
            TargetView = targetView;
        }
    }
}
