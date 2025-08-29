using CasinoWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWPF.Games
{
    public class SlotMachine : IGame
    {
        private readonly Random _random = new Random();
        private Player _currentPlayer;
        private int _currentBet;
        private bool _gameRunning;
        private SlotSymbol[] _reels;

        public string Name => "Slot Machine";
        public GameType GameType => GameType.SlotMachine;
        public string Description => "Bringe drei gleiche Symbole in einer Reihe oder zwei gleiche nebeneinander, um zu gewinnen.";
        public int MinimumBet => 5;
        public int MaximumBet => 100;
        public bool IsGameRunning => _gameRunning;

        public SlotSymbol[] CurrentReels => _reels?.Clone() as SlotSymbol[];

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        // Win factors for 3 matching symbols
        private readonly Dictionary<SlotSymbol, int> _threeSymbolPayouts = new Dictionary<SlotSymbol, int>
        {
            { SlotSymbol.Cherry, 2 },
            { SlotSymbol.Lemon, 2 },
            { SlotSymbol.Orange, 3 },
            { SlotSymbol.Plum, 3 },
            { SlotSymbol.Bell, 4 },
            { SlotSymbol.Bar, 5 },
            { SlotSymbol.Seven, 10 },
            { SlotSymbol.Diamond, 15 }
        };

        // Win factors for 2 matching symbols
        private readonly Dictionary<SlotSymbol, double> _twoSymbolPayouts = new Dictionary<SlotSymbol, double>
        {
            { SlotSymbol.Cherry, 0.5 },
            { SlotSymbol.Lemon, 0.5 },
            { SlotSymbol.Orange, 1.0 },
            { SlotSymbol.Plum, 1.0 },
            { SlotSymbol.Bell, 1.5 },
            { SlotSymbol.Bar, 2.0 },
            { SlotSymbol.Seven, 3.0 },
            { SlotSymbol.Diamond, 5.0 }
        };

        public SlotMachine()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _reels = new SlotSymbol[3];
            _currentPlayer = null;
            _currentBet = 0;
            _gameRunning = false;
        }

        public bool StartGame(Player player, int betAmount, int numberOfPlayers)
        {
            return StartGame(player, betAmount);
        }

        public bool StartGame(Player player, int betAmount)
        {
            if (player == null || betAmount < MinimumBet || betAmount > MaximumBet)
                return false;

            if (!player.PlaceBet(betAmount))
                return false;

            InitializeGame();
            _currentPlayer = player;
            _currentBet = betAmount;
            _gameRunning = true;

            OnGameStateChanged("Slot Machine gestartet. Drücke Spin, um die Walzen zu drehen.");
            return true;
        }

        public GameResult EndGame()
        {
            if (!_gameRunning)
                return null;

            _gameRunning = false;

            // test win conditions 
            var winResult = CalculateWin();
            int winAmount = winResult.winAmount;
            string resultDescription = winResult.resultDescription;

            var result = new GameResult
            {
                IsWin = winAmount > 0,
                WinAmount = winAmount,
                BetAmount = _currentBet,
                GameType = GameType.SlotMachine,
                ResultDescription = resultDescription,
                GameSpecificResults = new Dictionary<string, object>
                {
                    { "Reels", _reels.Clone() },
                    { "WinType", winResult.winType }
                }
            };

            OnGameStateChanged(resultDescription, true, result);
            return result;
        }

        private (int winAmount, string resultDescription, string winType) CalculateWin()
        {
            // test if 3 symbols are the same
            if (AreAllReelsEqual())
            {
                int multiplier = _threeSymbolPayouts[_reels[0]];
                int winAmount = (int)(_currentBet * multiplier);
                string description = $"JACKPOT! Drei {_reels[0]} in einer Reihe! {multiplier}x Gewinn: {winAmount} Jetons.";
                return (winAmount, description, "ThreeOfAKind");
            }

            // test if 2 symbols are the same
            if (HasTwoMatchingSymbols(out SlotSymbol matchingSymbol, out string position))
            {
                double multiplier = _twoSymbolPayouts[matchingSymbol];
                int winAmount = (int)(_currentBet * multiplier);
                string symbolName = GetSymbolName(matchingSymbol);
                string description = $"Gewonnen! Zwei {symbolName} {position}. {multiplier}x Gewinn: {winAmount} Jetons.";
                return (winAmount, description, "TwoOfAKind");
            }

            // loss
            return (0, "Kein Gewinn. Versuche es erneut!", "NoWin");
        }

        private bool HasTwoMatchingSymbols(out SlotSymbol matchingSymbol, out string position)
        {
            matchingSymbol = SlotSymbol.Cherry;
            position = "";

            // test left and middle
            if (_reels[0] == _reels[1] && _reels[0] != _reels[2])
            {
                matchingSymbol = _reels[0];
                position = "links";
                return true;
            }

            // test middle and right
            if (_reels[1] == _reels[2] && _reels[1] != _reels[0])
            {
                matchingSymbol = _reels[1];
                position = "rechts";
                return true;
            }

            // test left and right
            if (_reels[0] == _reels[2] && _reels[0] != _reels[1])
            {
                matchingSymbol = _reels[0];
                position = "aussen";
                return true;
            }

            return false;
        }

        private string GetSymbolName(SlotSymbol symbol)
        {
            return symbol switch
            {
                SlotSymbol.Cherry => "Kirschen",
                SlotSymbol.Lemon => "Zitronen",
                SlotSymbol.Orange => "Orangen",
                SlotSymbol.Plum => "Pflaumen",
                SlotSymbol.Bell => "Glocken",
                SlotSymbol.Bar => "Bars",
                SlotSymbol.Seven => "Siebener",
                SlotSymbol.Diamond => "Diamanten",
                _ => symbol.ToString()
            };
        }

        public bool ExecuteAction(string actionName, params object[] parameters)
        {
            if (!_gameRunning)
                return false;

            switch (actionName.ToLower())
            {
                case "spin":
                    SpinReels();
                    string reelsDescription = $"Walzen: {_reels[0]}, {_reels[1]}, {_reels[2]}";
                    OnGameStateChanged(reelsDescription);
                    return EndGame() != null;

                default:
                    return false;
            }
        }

        private void SpinReels()
        {
            for (int i = 0; i < _reels.Length; i++)
            {
                _reels[i] = GetRandomSymbol();
            }
        }

        private SlotSymbol GetRandomSymbol()
        {
            Array values = Enum.GetValues(typeof(SlotSymbol));
            return (SlotSymbol)values.GetValue(_random.Next(values.Length));
        }

        private bool AreAllReelsEqual()
        {
            return _reels[0] == _reels[1] && _reels[1] == _reels[2];
        }

        protected virtual void OnGameStateChanged(string message, bool isGameOver = false, GameResult result = null)
        {
            GameStateChanged?.Invoke(this, new GameStateChangedEventArgs(message, isGameOver, result));
        }
    }
}