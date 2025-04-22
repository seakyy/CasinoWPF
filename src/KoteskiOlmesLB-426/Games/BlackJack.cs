using System;
using System.Collections.Generic;
using System.Linq;
using KoteskiOlmesLB_426.Models;

namespace KoteskiOlmesLB_426.Games
{
    public class BlackJack : IGame
    {
        private readonly Random _random = new Random();
        private List<Card> _deck;
        private List<Card> _dealerHand;
        private List<Card> _playerHand;
        private List<ComputerPlayer> _computerPlayers;
        private Player _currentPlayer;
        private int _currentBet;
        private bool _gameRunning;
        private int _numberOfPlayers;

        public int GetPlayerHandValue() => CalculateHandValue(_playerHand);
        public int GetDealerHandValue() => CalculateHandValue(_dealerHand);


        private const float BASE_WINNING_MULTIPLIER = 2.0f;
        private const float ADDITIONAL_PLAYER_MULTIPLIER = 0.5f;

        public string Name => "BlackJack";
        public GameType GameType => GameType.BlackJack;
        public string Description => "Erreiche mit deinen Karten eine Summe möglichst nahe an 21, ohne darüber hinauszugehen.";
        public int MinimumBet => 10;
        public int MaximumBet => 500;
        public bool IsGameRunning => _gameRunning;

        public List<Card> DealerHand => _dealerHand?.ToList();
        public List<Card> PlayerHand => _playerHand?.ToList();
        public List<ComputerPlayer> ComputerPlayers => _computerPlayers;

        public int NumberOfPlayers
        {
            get => _numberOfPlayers;
            set
            {
                if (value >= 1 && value <= 5)
                    _numberOfPlayers = value;
            }
        }

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        public BlackJack()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _deck = new List<Card>();
            _dealerHand = new List<Card>();
            _playerHand = new List<Card>();
            _computerPlayers = new List<ComputerPlayer>();
            _currentPlayer = null;
            _currentBet = 0;
            _gameRunning = false;
        }

        private void CreateDeck()
        {
            _deck.Clear();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    _deck.Add(new Card(suit, value));
                }
            }

            ShuffleDeck();
        }

        private void ShuffleDeck()
        {
            int n = _deck.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                Card temp = _deck[k];
                _deck[k] = _deck[n];
                _deck[n] = temp;
            }
        }

        private Card DrawCard()
        {
            if (_deck.Count == 0)
                CreateDeck();

            Card card = _deck[0];
            _deck.RemoveAt(0);
            return card;
        }

        private int CalculateHandValue(List<Card> hand)
        {
            int value = 0;
            int aceCount = 0;

            foreach (var card in hand)
            {
                if (card.Value == CardValue.Ace)
                {
                    aceCount++;
                    value += 11;
                }
                else if (card.Value == CardValue.Jack || card.Value == CardValue.Queen || card.Value == CardValue.King)
                {
                    value += 10;
                }
                else
                {
                    value += (int)card.Value;
                }
            }

            while (aceCount > 0 && value > 21)
            {
                value -= 10;
                aceCount--;
            }

            return value;
        }

        private float CalculateWinningMultiplier()
        {
            return BASE_WINNING_MULTIPLIER + (_numberOfPlayers * ADDITIONAL_PLAYER_MULTIPLIER);
        }

        public bool StartGame(Player player, int betAmount)
        {
            // Standardmäßig mit einem Computergegner
            return StartGame(player, betAmount, 1);
        }

        public bool StartGame(Player player, int betAmount, int numberOfPlayers)
        {
            if (player == null || betAmount < MinimumBet || betAmount > MaximumBet)
                return false;

            if (!player.PlaceBet(betAmount))
                return false;

            if (numberOfPlayers == 1)
            {
                _dealerHand.Add(DrawCard());
                _dealerHand.Add(DrawCard());
            }


            InitializeGame();
            _currentPlayer = player;
            _currentBet = betAmount;

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                var comp = new ComputerPlayer($"Computer {i + 1}", 1000);
                comp.SetBet(betAmount);
                _computerPlayers.Add(comp);
            }

            NumberOfPlayers = numberOfPlayers;


            _gameRunning = true;
            CreateDeck();

            _playerHand.Add(DrawCard());
            _dealerHand.Add(DrawCard());
            _playerHand.Add(DrawCard());
            _dealerHand.Add(DrawCard());

            foreach (var comp in _computerPlayers)
            {
                comp.AddCard(DrawCard());
                comp.AddCard(DrawCard());
            }

            if (CalculateHandValue(_playerHand) == 21)
                return ExecuteAction("stand");

            OnGameStateChanged("Spiel gestartet. Deine Karten: " + string.Join(", ", _playerHand));
            return true;

        }

        public bool ExecuteAction(string actionName, params object[] parameters)
        {
            if (!_gameRunning)
                return false;

            switch (actionName.ToLower())
            {
                case "hit":
                    _playerHand.Add(DrawCard());
                    int value = CalculateHandValue(_playerHand);
                    OnGameStateChanged($"Du ziehst eine Karte. Neuer Wert: {value}");
                    if (value > 21)
                        return EndGame() != null;
                    return true;

                case "stand":
                    foreach (var comp in _computerPlayers)
                    {
                        while (CalculateHandValue(comp.Hand) < 17)
                        {
                            comp.AddCard(DrawCard());
                            OnGameStateChanged($"{comp.Name} zieht eine Karte. Wert: {CalculateHandValue(comp.Hand)}");
                        }
                        comp.Stand();
                    }

                    while (CalculateHandValue(_dealerHand) < 17)
                    {
                        _dealerHand.Add(DrawCard());
                        OnGameStateChanged($"Dealer zieht eine Karte. Dealer Wert: {CalculateHandValue(_dealerHand)}");
                    }

                    return EndGame() != null;

                case "doubledown":
                    if (_playerHand.Count != 2 || !_currentPlayer.PlaceBet(_currentBet))
                        return false;

                    _currentBet *= 2;
                    _playerHand.Add(DrawCard());
                    OnGameStateChanged($"Du verdoppelst deinen Einsatz und ziehst eine Karte. Neuer Wert: {CalculateHandValue(_playerHand)}");
                    return ExecuteAction("stand");

                default:
                    return false;
            }
        }

        public GameResult EndGame()
        {
            if (!_gameRunning)
                return null;

            _gameRunning = false;

            int playerValue = CalculateHandValue(_playerHand);
            int dealerValue = CalculateHandValue(_dealerHand);
            float winningMultiplier = CalculateWinningMultiplier();

            bool playerBust = playerValue > 21;
            bool dealerBust = dealerValue > 21;
            bool playerBlackjack = playerValue == 21 && _playerHand.Count == 2;
            bool dealerBlackjack = dealerValue == 21 && _dealerHand.Count == 2;

            bool isWin;
            int winAmount = 0;
            string resultDescription;
            BlackJackResult blackJackResult;

            if (playerBust)
            {
                isWin = false;
                resultDescription = "Bust! Deine Hand überschreitet 21.";
                blackJackResult = BlackJackResult.Bust;
            }
            else if (dealerBust)
            {
                isWin = true;
                resultDescription = "Gewonnen! Der Dealer hat sich überkauft.";
                blackJackResult = BlackJackResult.Win;
            }
            else if (playerBlackjack && !dealerBlackjack)
            {
                isWin = true;
                blackJackResult = BlackJackResult.BlackJack;
                resultDescription = "BlackJack! Du hast 3:2 gewonnen.";
            }
            else if (!playerBlackjack && dealerBlackjack)
            {
                isWin = false;
                blackJackResult = BlackJackResult.Lose;
                resultDescription = "Verloren! Der Dealer hat BlackJack.";
            }
            else if (playerValue > dealerValue)
            {
                isWin = true;
                blackJackResult = BlackJackResult.Win;
                resultDescription = $"Gewonnen! Deine Hand ({playerValue}) schlägt den Dealer ({dealerValue}).";
            }
            else if (playerValue < dealerValue)
            {
                isWin = false;
                blackJackResult = BlackJackResult.Lose;
                resultDescription = $"Verloren! Der Dealer ({dealerValue}) schlägt deine Hand ({playerValue}).";
            }
            else
            {
                isWin = true;
                blackJackResult = BlackJackResult.Push;
                resultDescription = $"Push! Gleichstand mit {playerValue} Punkten.";
            }

            if (isWin)
            {
                winAmount = playerBlackjack
                    ? (int)(_currentBet * (1.5 + (_numberOfPlayers * 0.5)))
                    : (int)(_currentBet * winningMultiplier);
                _currentPlayer.AddBalance(winAmount);
            }

            var computerResults = _computerPlayers.Select(cp => new Dictionary<string, object>
            {
                { "PlayerName", cp.Name },
                { "HandValue", CalculateHandValue(cp.Hand) },
                { "Bust", CalculateHandValue(cp.Hand) > 21 },
                { "BlackJack", CalculateHandValue(cp.Hand) == 21 && cp.Hand.Count == 2 },
                { "Hand", cp.Hand.ToList() }
            }).ToList();

            var result = new GameResult
            {
                IsWin = isWin,
                WinAmount = winAmount,
                BetAmount = _currentBet,
                GameType = GameType.BlackJack,
                ResultDescription = resultDescription,
                GameSpecificResults = new Dictionary<string, object>
                {
                    { "BlackJackResult", blackJackResult },
                    { "PlayerValue", playerValue },
                    { "DealerValue", dealerValue },
                    { "PlayerHand", _playerHand.ToList() },
                    { "DealerHand", _dealerHand.ToList() },
                    { "ComputerPlayers", computerResults },
                    { "WinningMultiplier", winningMultiplier }
                }
            };

            OnGameStateChanged(resultDescription, true, result);
            return result;
        }

        protected virtual void OnGameStateChanged(string message, bool isGameOver = false, GameResult result = null)
        {
            GameStateChanged?.Invoke(this, new GameStateChangedEventArgs(message, isGameOver, result));
        }
    }
}