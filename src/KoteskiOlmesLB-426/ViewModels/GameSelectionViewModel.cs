using KoteskiOlmesLB_426.Models;
using KoteskiOlmesLB_426.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;




namespace KoteskiOlmesLB_426.ViewModels
{

    public class GameSelectionViewModel : INotifyPropertyChanged, KoteskiOlmesLB_426.Services.IObserver<SessionData>
    {
        private readonly Session _session;
        private Player _currentPlayer;

        // Properties
        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        // Commands
        public ICommand StartBlackJackCommand { get; }
        public ICommand StartSlotMachineCommand { get; }
        public ICommand ReturnToMainMenuCommand { get; }

        // Event für Navigation
        public event EventHandler<NavigationEventArgs> NavigationRequested;

        public GameSelectionViewModel()
        {
            _session = Session.Instance;
            _session.AddObserver(this);

            // Initialisiere den CurrentPlayer aus der Session
            CurrentPlayer = _session.CurrentPlayer;

            // Initialisiere Commands
            StartBlackJackCommand = new RelayCommand(ExecuteStartBlackJack);
            StartSlotMachineCommand = new RelayCommand(ExecuteStartSlotMachine);
            ReturnToMainMenuCommand = new RelayCommand(ExecuteReturnToMainMenu);
        }

        // IObserver Implementation
        public void Update(SessionData data)
        {
            // Aktualisiere den CurrentPlayer, wenn er sich in der Session ändert
            CurrentPlayer = data.CurrentPlayer;
        }

        private void ExecuteStartBlackJack(object parameter)
        {
            _session.CurrentGameType = GameType.BlackJack;
            OnNavigationRequested("BlackJack");
        }

        private void ExecuteStartSlotMachine(object parameter)
        {
            _session.CurrentGameType = GameType.SlotMachine;
            OnNavigationRequested("SlotMachine");
        }

        private void ExecuteReturnToMainMenu(object parameter)
        {
            OnNavigationRequested("MainMenu");
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
}
