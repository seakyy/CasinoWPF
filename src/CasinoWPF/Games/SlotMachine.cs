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
        public string Description => "Bringe drei gleiche Symbole in einer Reihe, um zu gewinnen.";
        public int MinimumBet => 5;
        public int MaximumBet => 100;
        public bool IsGameRunning => _gameRunning;

        public SlotSymbol[] CurrentReels => _reels?.Clone() as SlotSymbol[];

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        // Symbol-Gewinnfaktoren
        private readonly Dictionary<SlotSymbol, int> _payoutMultipliers = new Dictionary<SlotSymbol, int>
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


        // Diese Methode wird nur zur Interface-Erfüllung benötigt
        public bool StartGame(Player player, int betAmount, int numberOfPlayers)
        {
            // SlotMachine benötigt keine Gegneranzahl, leite einfach weiter
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

            // Benachrichtige Beobachter über Spielstart
            OnGameStateChanged("Slot Machine gestartet. Drücke Spin, um die Walzen zu drehen.");

            return true;
        }

        public GameResult EndGame()
        {
            if (!_gameRunning)
                return null;

            _gameRunning = false;

            // Berechne das Ergebnis
            bool isWin = AreAllReelsEqual();
            int winAmount = 0;
            string resultDescription = "";

            if (isWin)
            {
                // Bestimme den Gewinnfaktor basierend auf dem Symbol
                int multiplier = _payoutMultipliers[_reels[0]];
                winAmount = _currentBet * multiplier;

                resultDescription = $"Gewonnen! Drei {_reels[0]} in einer Reihe! {multiplier}x Gewinn: {winAmount} Jetons.";
            }
            else
            {
                resultDescription = "Kein Gewinn. Versuche es erneut!";
            }

            var result = new GameResult
            {
                IsWin = isWin,
                WinAmount = winAmount,
                BetAmount = _currentBet,
                GameType = GameType.SlotMachine,
                ResultDescription = resultDescription,
                GameSpecificResults = new Dictionary<string, object>
                {
                    { "Reels", _reels.Clone() }
                }
            };

            // Benachrichtige Beobachter über Spielende
            OnGameStateChanged(resultDescription, true, result);

            return result;
        }

        public bool ExecuteAction(string actionName, params object[] parameters)
        {
            if (!_gameRunning)
                return false;

            switch (actionName.ToLower())
            {
                case "spin":
                    // Drehe die Walzen
                    SpinReels();

                    // Zeige das Ergebnis der Walzen
                    string reelsDescription = $"Walzen: {_reels[0]}, {_reels[1]}, {_reels[2]}";
                    OnGameStateChanged(reelsDescription);

                    // Beende das Spiel und bestimme das Ergebnis
                    return EndGame() != null;

                default:
                    return false;
            }
        }

        private void SpinReels()
        {
            // Drehe jede Walze
            for (int i = 0; i < _reels.Length; i++)
            {
                _reels[i] = GetRandomSymbol();
            }
        }

        private SlotSymbol GetRandomSymbol()
        {
            // Wähle ein zufälliges Symbol aus
            Array values = Enum.GetValues(typeof(SlotSymbol));
            return (SlotSymbol)values.GetValue(_random.Next(values.Length));
        }

        private bool AreAllReelsEqual()
        {
            // Prüfe, ob alle Walzen das gleiche Symbol zeigen
            return _reels[0] == _reels[1] && _reels[1] == _reels[2];
        }

        protected virtual void OnGameStateChanged(string message, bool isGameOver = false, GameResult result = null)
        {
            GameStateChanged?.Invoke(this, new GameStateChangedEventArgs(message, isGameOver, result));
        }
    }
}