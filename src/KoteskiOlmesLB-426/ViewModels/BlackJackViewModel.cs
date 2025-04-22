using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using KoteskiOlmesLB_426.Games;
using KoteskiOlmesLB_426.Models;
using KoteskiOlmesLB_426.Services;
using System.Diagnostics;


namespace KoteskiOlmesLB_426.ViewModels
{
    public class BlackJackViewModel : ViewModelBase
    {
        private readonly BlackJack _game;
        private Player _currentPlayer;
        private ObservableCollection<Card> _playerCards;
        private ObservableCollection<Card> _dealerCards;
        private ObservableCollection<ComputerPlayerViewModel> _computerPlayers;
        private string _resultMessage;
        private bool _gameInProgress;
        private int _selectedBet;
        private int _selectedPlayerCount;
        private int _playerWins;
        private int _dealerWins;
        private int _balance;
        public int PlayerScore => _game.GetPlayerHandValue();
        public int DealerScore => _game.GetDealerHandValue();




        public int Balance
        {
            get => _balance;
            set
            {
                if (_balance == value) return; // Avoid unnecessary updates
                _balance = value;
                OnPropertyChanged(); // ❗️ Damit UI aktualisiert wird
            }
        }


        /*
        {
            get => _playerWins;
            set => SetProperty(ref _playerWins, value);
        }
        */
        public int DealerWins
        {
            get => _dealerWins;
            set => SetProperty(ref _dealerWins, value);
        }


        public void RefreshBalance()
        {
            OnPropertyChanged(nameof(Balance));
        }


        public ObservableCollection<Card> PlayerCards
        {
            get => _playerCards;
            set => SetProperty(ref _playerCards, value);
        }

        public ObservableCollection<Card> DealerCards
        {
            get => _dealerCards;
            set => SetProperty(ref _dealerCards, value);
        }

        public ObservableCollection<ComputerPlayerViewModel> ComputerPlayers
        {
            get => _computerPlayers;
            set => SetProperty(ref _computerPlayers, value);
        }

        public ObservableCollection<int> BetOptions { get; } = new() { 10, 20, 50, 100, 200, 500 };
        public ObservableCollection<int> PlayerCountOptions { get; } = new() { 1, 2, 3, 4, 5 };

        public int SelectedBet
        {
            get => _selectedBet;
            set => SetProperty(ref _selectedBet, value);
        }

        public int SelectedPlayerCount
        {
            get => _selectedPlayerCount;
            set => SetProperty(ref _selectedPlayerCount, value);
        }

        public string ResultMessage
        {
            get => _resultMessage;
            set => SetProperty(ref _resultMessage, value);
        }


        public bool GameInProgress
        {
            get => _gameInProgress;
            set
            {
                if (_gameInProgress != value)
                {
                    _gameInProgress = value;
                    Console.WriteLine($"📢 GameInProgress gesetzt auf: {value}");
                    OnPropertyChanged();
                }
            }
        }



        public ICommand NewGameCommand { get; }
        public ICommand HitCommand { get; }
        public ICommand StandCommand { get; }
        public ICommand DoubleDownCommand { get; }
        public ICommand ReturnToSelectionCommand { get; }
        public ICommand ReturnToMainMenuCommand { get; }
        private int _currentBet;

        public int CurrentBet => _currentBet;
        public int NumberOfEnemies => _computerPlayers.Count;


        public event EventHandler<NavigationEventArgs> NavigationRequested;

        public BlackJackViewModel(Player player)
        {
            GameInProgress = false;
            _currentPlayer = player;
            _game = new BlackJack();
            _game.GameStateChanged += OnGameStateChanged;

            PlayerCards = new ObservableCollection<Card>();
            DealerCards = new ObservableCollection<Card>();
            ComputerPlayers = new ObservableCollection<ComputerPlayerViewModel>();

            SelectedBet = BetOptions.First();
            SelectedPlayerCount = PlayerCountOptions.First();

            NewGameCommand = new RelayCommand(_ => StartNewGame());
            HitCommand = new RelayCommand(_ => _game.ExecuteAction("hit"));
            StandCommand = new RelayCommand(_ => _game.ExecuteAction("stand"));
            DoubleDownCommand = new RelayCommand(_ => _game.ExecuteAction("doubledown"));

            ReturnToSelectionCommand = new RelayCommand(_ => OnNavigationRequested("GameSelection"));
            ReturnToMainMenuCommand = new RelayCommand(_ => OnNavigationRequested("MainMenu"));
        }

        private void StartNewGame()
        {
            Debug.WriteLine("🎮 StartNewGame wurde aufgerufen");

            if (_game.StartGame(_currentPlayer, SelectedBet, SelectedPlayerCount))
            {
                GameInProgress = false;
                Debug.WriteLine($"📢 GameInProgress gesetzt auf: {GameInProgress}");

                UpdateGameState();
                ResultMessage = string.Empty;
            }
            else
            {
                Debug.WriteLine("⚠️ StartGame returned false!");
            }
        }


        private void UpdateGameState()
        {
            PlayerCards = new ObservableCollection<Card>(_game.PlayerHand);
            DealerCards = new ObservableCollection<Card>(_game.DealerHand);
            Balance = _currentPlayer.Balance;

            ComputerPlayers.Clear();
            foreach (var comp in _game.ComputerPlayers)
                ComputerPlayers.Add(new ComputerPlayerViewModel(comp));

            OnPropertyChanged(nameof(PlayerScore));
            OnPropertyChanged(nameof(DealerScore));

            DealerCards = SelectedPlayerCount == 1
                ? new ObservableCollection<Card>(_game.DealerHand)
                : new ObservableCollection<Card>();


        }

        private void OnGameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            if (e.IsGameOver)
            {
                GameInProgress = false;
                ResultMessage = e.Message;
            }
            else
            {
                ResultMessage = e.Message;
            }

            UpdateGameState();

            if (e.IsGameOver && e.Result is GameResult result)
            {
                GameLogService.Instance.AddEntry(
                    GameType.SlotMachine,
                    result.ResultDescription,
                    result.BetAmount,
                    result.WinAmount
                );
            }
        }

        private void OnNavigationRequested(string view)
        {
            NavigationRequested?.Invoke(this, new NavigationEventArgs(view));
        }
    }
}