using CasinoWPF.Games;
using CasinoWPF.Models;
using CasinoWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace CasinoWPF.ViewModels
{
    public class SlotMachineViewModel : ViewModelBase
    {
        private readonly SlotMachine _game;
        private ObservableCollection<SlotSymbol> _reels;
        private string _resultMessage;
        private int _selectedBet;
        private int _balance;
        private bool _isAutoSpinning = false;
        private string _winType;

        public ImageSource Reel1DisplayImage { get; set; }
        public ImageSource Reel2DisplayImage { get; set; }
        public ImageSource Reel3DisplayImage { get; set; }

        public bool HasResult => !string.IsNullOrWhiteSpace(ResultMessage);

        public Brush ResultBackground
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_resultMessage))
                    return Brushes.Transparent;

                if (_resultMessage.ToLower().Contains("jackpot"))
                    return Brushes.Gold;
                if (_resultMessage.ToLower().Contains("gewonnen"))
                    return Brushes.Green;
                return Brushes.IndianRed;
            }
        }

        public string WinType
        {
            get => _winType;
            set => SetProperty(ref _winType, value);
        }

        public ObservableCollection<SlotSymbol> Reels
        {
            get => _reels;
            set
            {
                SetProperty(ref _reels, value);
                OnPropertyChanged(nameof(Reel1Image));
                OnPropertyChanged(nameof(Reel2Image));
                OnPropertyChanged(nameof(Reel3Image));
            }
        }

        public ObservableCollection<int> BetOptions { get; } = new() { 5, 10, 20, 50, 100 };

        public int SelectedBet
        {
            get => _selectedBet;
            set => SetProperty(ref _selectedBet, value);
        }

        public string ResultMessage
        {
            get => _resultMessage;
            set
            {
                SetProperty(ref _resultMessage, value);
                OnPropertyChanged(nameof(HasResult));
                OnPropertyChanged(nameof(ResultBackground));
            }
        }

        public int Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }

        public ICommand SpinCommand { get; }
        public ICommand MaxBetCommand { get; }
        public ICommand AutoSpinCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand ReturnToSelectionCommand { get; }
        public ICommand ReturnToMainMenuCommand { get; }

        public event EventHandler<NavigationEventArgs> NavigationRequested;

        public SlotMachineViewModel()
        {
            _game = GameManager.Instance.StartGame(GameType.SlotMachine, SelectedBet)
                ? Session.Instance.CurrentGame as SlotMachine
                : new SlotMachine();

            if (_game != null)
            {
                _game.GameStateChanged += OnGameStateChanged;
            }

            Reels = new ObservableCollection<SlotSymbol> { SlotSymbol.Cherry, SlotSymbol.Cherry, SlotSymbol.Cherry };
            SelectedBet = BetOptions.First();

            SpinCommand = new RelayCommand(_ => Spin());
            MaxBetCommand = new RelayCommand(_ => SelectedBet = BetOptions.Max());
            AutoSpinCommand = new RelayCommand(_ => ToggleAutoSpin());
            NewGameCommand = new RelayCommand(_ => StartNewGame());
            ReturnToSelectionCommand = new RelayCommand(_ => OnNavigationRequested("GameSelection"));
            ReturnToMainMenuCommand = new RelayCommand(_ => OnNavigationRequested("MainMenu"));

            Balance = Session.Instance.CurrentPlayer?.Balance ?? 0;
        }

        private void StartNewGame()
        {
            var player = Session.Instance.CurrentPlayer;
            if (_game.StartGame(player, SelectedBet))
            {
                ResultMessage = string.Empty;
                WinType = "";
                Balance = player.Balance;
            }
        }

        private async void Spin()
        {
            var player = Session.Instance.CurrentPlayer;

            if (!_game.IsGameRunning)
            {
                _game.StartGame(player, SelectedBet);
            }

            // Fake animation so it looks like the reels are spinning
            for (int i = 0; i < 10; i++)
            {
                Reel1DisplayImage = GetRandomImage();
                Reel2DisplayImage = GetRandomImage();
                Reel3DisplayImage = GetRandomImage();
                OnPropertyChanged(nameof(Reel1DisplayImage));
                OnPropertyChanged(nameof(Reel2DisplayImage));
                OnPropertyChanged(nameof(Reel3DisplayImage));
                await Task.Delay(50);
            }

            // show real result
            _game.ExecuteAction("spin");

            // wait till the game state has updated
            await Task.Delay(50);

            Reel1DisplayImage = Reel1Image;
            Reel2DisplayImage = Reel2Image;
            Reel3DisplayImage = Reel3Image;
            OnPropertyChanged(nameof(Reel1DisplayImage));
            OnPropertyChanged(nameof(Reel2DisplayImage));
            OnPropertyChanged(nameof(Reel3DisplayImage));

            // special animation for win
            await PlayWinAnimation();
        }

        private async Task PlayWinAnimation()
        {
            if (WinType == "ThreeOfAKind")
            {
                // Jackpot animation for three same symbolss
                await Task.WhenAll(
                    PulseReel(Reel1ImageControl),
                    PulseReel(Reel2ImageControl),
                    PulseReel(Reel3ImageControl)
                );
            }
            else if (WinType == "TwoOfAKind")
            {
                // same animation but only for the two matching reels
                if (Reels[0] == Reels[1])
                {
                    await Task.WhenAll(PulseReel(Reel1ImageControl), PulseReel(Reel2ImageControl));
                }
                else if (Reels[1] == Reels[2])
                {
                    await Task.WhenAll(PulseReel(Reel2ImageControl), PulseReel(Reel3ImageControl));
                }
                else if (Reels[0] == Reels[2])
                {
                    await Task.WhenAll(PulseReel(Reel1ImageControl), PulseReel(Reel3ImageControl));
                }
            }
        }

        private async Task PulseReel(Image reelImage)
        {
            if (reelImage == null) return;

            for (int i = 0; i < 3; i++)
            {
                reelImage.Opacity = 0.7;
                await Task.Delay(100);
                reelImage.Opacity = 1.0;
                await Task.Delay(100);
            }
        }

        private ImageSource GetRandomImage()
        {
            var values = Enum.GetValues(typeof(SlotSymbol)).Cast<SlotSymbol>().ToList();
            var random = new Random();
            var symbol = values[random.Next(values.Count)];
            return GetImageFromSymbol(symbol);
        }

        private ImageSource GetImageFromSymbol(SlotSymbol symbol)
        {
            if (_symbolImagePaths.TryGetValue(symbol, out var path))
            {
                try
                {
                    var uri = new Uri($"pack://application:,,,/{path}", UriKind.Absolute);
                    return new BitmapImage(uri);
                }
                catch { }
            }
            return null;
        }

        private async void ToggleAutoSpin()
        {
            _isAutoSpinning = !_isAutoSpinning;

            while (_isAutoSpinning)
            {
                if (Session.Instance.CurrentPlayer?.Balance < SelectedBet)
                {
                    ResultMessage = "Nicht genug Jetons!";
                    _isAutoSpinning = false;
                    return;
                }

                Spin();
                await Task.Delay(2000); // longer delay to see results better / longer
            }
        }

        private void OnGameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            ResultMessage = e.Message;
            Reels = new ObservableCollection<SlotSymbol>(_game.CurrentReels);
            Balance = Session.Instance.CurrentPlayer.Balance;

            if (e.IsGameOver && e.Result is GameResult result)
            {
                if (result.GameSpecificResults != null &&
                    result.GameSpecificResults.ContainsKey("WinType"))
                {
                    WinType = result.GameSpecificResults["WinType"] as string;
                }

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

        public ImageSource Reel1Image => GetImage(0);
        public ImageSource Reel2Image => GetImage(1);
        public ImageSource Reel3Image => GetImage(2);

        public Image Reel1ImageControl { get; set; }
        public Image Reel2ImageControl { get; set; }
        public Image Reel3ImageControl { get; set; }

        private ImageSource GetImage(int index)
        {
            if (Reels.Count > index && _symbolImagePaths.TryGetValue(Reels[index], out var path))
            {
                try
                {
                    var uri = new Uri($"pack://application:,,,/{path}", UriKind.Absolute);
                    return new BitmapImage(uri);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌❌ Fehler beim Laden von Bild {path}: {ex.Message}");
                }
            }
            return null;
        }

        private readonly Dictionary<SlotSymbol, string> _symbolImagePaths = new()
        {
            { SlotSymbol.Cherry, "Resources/Cherry.jpg" },
            { SlotSymbol.Lemon,  "Resources/Lemon.jpg" },
            { SlotSymbol.Orange, "Resources/Orange.jpg" },
            { SlotSymbol.Plum,   "Resources/Plum.jpg" },
            { SlotSymbol.Bell,   "Resources/Bell.jpg" },
            { SlotSymbol.Bar,    "Resources/Bar.jpg" },
            { SlotSymbol.Seven,  "Resources/Seven.jpg" },
            { SlotSymbol.Diamond,"Resources/Diamond.jpg" }
        };
    }
}