using KoteskiOlmesLB_426.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoteskiOlmesLB_426.Games
{

    public class Card
    {
        public CardSuit Suit { get; }
        public CardValue Value { get; }

        public Card(CardSuit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            string valueStr;
            switch (Value)
            {
                case CardValue.Ace:
                    valueStr = "A";
                    break;
                case CardValue.Jack:
                    valueStr = "J";
                    break;
                case CardValue.Queen:
                    valueStr = "Q";
                    break;
                case CardValue.King:
                    valueStr = "K";
                    break;
                default:
                    valueStr = ((int)Value).ToString();
                    break;
            }

            return $"{valueStr} of {Suit}";
        }
    }

    public class BlackJack : IGame
    {
        private readonly Random _random = new Random();
        private List<Card> _deck;
        private List<Card> _dealerHand;
        private List<Card> _playerHand;
        private Player _currentPlayer;
        private int _currentBet;
        private bool _gameRunning;

        public string Name => "BlackJack";
        public GameType GameType => GameType.BlackJack;
        public string Description => "Erreiche mit deinen Karten eine Summe möglichst nahe an 21, ohne darüber hinauszugehen.";
        public int MinimumBet => 10;
        public int MaximumBet => 500;
        public bool IsGameRunning => _gameRunning;

        public List<Card> DealerHand => _dealerHand?.ToList();
        public List<Card> PlayerHand => _playerHand?.ToList();

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

            // Mische das Deck
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
            {
                CreateDeck();
            }

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
                // Behandle Kartenwerte speziell
                if (card.Value == CardValue.Ace)
                {
                    aceCount++;
                    value += 11; // Ass initial als 11 zählen
                }
                else if (card.Value == CardValue.Jack || card.Value == CardValue.Queen || card.Value == CardValue.King)
                {
                    value += 10; // Bildkarten zählen als 10
                }
                else
                {
                    value += (int)card.Value; // Zahlenkarten zählen als ihr Wert
                }
            }

            // Passe den Wert von Assen an, wenn nötig
            while (aceCount > 0 && value > 21)
            {
                value -= 10; // Reduziere ein Ass von 11 auf 1
                aceCount--;
            }

            return value;
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

            // Erstelle und mische das Deck
            CreateDeck();

            // Teile Karten aus
            _playerHand.Add(DrawCard());
            _dealerHand.Add(DrawCard());
            _playerHand.Add(DrawCard());
            _dealerHand.Add(DrawCard());

            // Prüfe auf BlackJack
            if (CalculateHandValue(_playerHand) == 21)
            {
                // Spieler hat BlackJack
                return ExecuteAction("Stand");
            }

            // Benachrichtige Beobachter über Spielstart
            OnGameStateChanged("Spiel gestartet. Deine Karten: " + string.Join(", ", _playerHand));

            return true;
        }

        public GameResult EndGame()
        {
            if (!_gameRunning)
                return null;

            _gameRunning = false;

            int playerValue = CalculateHandValue(_playerHand);
            int dealerValue = CalculateHandValue(_dealerHand);

            bool playerBust = playerValue > 21;
            bool dealerBust = dealerValue > 21;
            bool playerBlackjack = playerValue == 21 && _playerHand.Count == 2;
            bool dealerBlackjack = dealerValue == 21 && _dealerHand.Count == 2;

            bool isWin = false;
            int winAmount = 0;
            string resultDescription = "";
            BlackJackResult blackJackResult;

            if (playerBust)
            {
                // Spieler hat sich überkauft
                isWin = false;
                resultDescription = "Bust! Deine Hand überschreitet 21.";
                blackJackResult = BlackJackResult.Bust;
            }
            else if (dealerBust)
            {
                // Dealer hat sich überkauft
                isWin = true;
                winAmount = _currentBet * 2;
                resultDescription = "Gewonnen! Der Dealer hat sich überkauft.";
                blackJackResult = BlackJackResult.Win;
            }
            else if (playerBlackjack && !dealerBlackjack)
            {
                // Spieler hat BlackJack, Dealer nicht
                isWin = true;
                winAmount = (int)(_currentBet * 2.5); // BlackJack zahlt 3:2
                resultDescription = "BlackJack! Du hast 3:2 gewonnen.";
                blackJackResult = BlackJackResult.BlackJack;
            }
            else if (!playerBlackjack && dealerBlackjack)
            {
                // Dealer hat BlackJack, Spieler nicht
                isWin = false;
                resultDescription = "Verloren! Der Dealer hat BlackJack.";
                blackJackResult = BlackJackResult.Lose;
            }
            else if (playerBlackjack && dealerBlackjack)
            {
                // Beide haben BlackJack
                isWin = true;
                winAmount = _currentBet; // Einsatz zurück
                resultDescription = "Push! Beide haben BlackJack.";
                blackJackResult = BlackJackResult.Push;
            }
            else if (playerValue > dealerValue)
            {
                // Spieler hat höheren Wert
                isWin = true;
                winAmount = _currentBet * 2;
                resultDescription = $"Gewonnen! Deine Hand ({playerValue}) schlägt den Dealer ({dealerValue}).";
                blackJackResult = BlackJackResult.Win;
            }
            else if (playerValue < dealerValue)
            {
                // Dealer hat höheren Wert
                isWin = false;
                resultDescription = $"Verloren! Der Dealer ({dealerValue}) hat eine bessere Hand als du ({playerValue}).";
                blackJackResult = BlackJackResult.Lose;
            }
            else
            {
                // Gleichstand
                isWin = true;
                winAmount = _currentBet; // Einsatz zurück
                resultDescription = $"Push! Gleichstand mit {playerValue} Punkten.";
                blackJackResult = BlackJackResult.Push;
            }

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
                    { "DealerHand", _dealerHand.ToList() }
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
                case "hit":
                    // Spieler nimmt eine weitere Karte
                    _playerHand.Add(DrawCard());

                    int playerValue = CalculateHandValue(_playerHand);
                    OnGameStateChanged($"Du ziehst eine Karte. Neuer Wert: {playerValue}");

                    if (playerValue > 21)
                    {
                        // Spieler hat sich überkauft
                        return EndGame() != null;
                    }

                    return true;

                case "stand":
                    // Dealer zieht Karten, bis er 17 oder mehr erreicht
                    while (CalculateHandValue(_dealerHand) < 17)
                    {
                        _dealerHand.Add(DrawCard());
                        OnGameStateChanged($"Dealer zieht eine Karte. Dealer Wert: {CalculateHandValue(_dealerHand)}");
                    }

                    // Beende das Spiel und bestimme das Ergebnis
                    return EndGame() != null;

                case "doubledown":
                    if (_playerHand.Count != 2 || !_currentPlayer.PlaceBet(_currentBet))
                    {
                        return false;
                    }

                    // Verdopple den Einsatz
                    _currentBet *= 2;

                    // Nimm eine Karte und dann Stand
                    _playerHand.Add(DrawCard());

                    playerValue = CalculateHandValue(_playerHand);
                    OnGameStateChanged($"Du verdoppelst deinen Einsatz und ziehst eine Karte. Neuer Wert: {playerValue}");

                    // Spiel automatisch beenden
                    return ExecuteAction("Stand");

                default:
                    return false;
            }
        }

        protected virtual void OnGameStateChanged(string message, bool isGameOver = false, GameResult result = null)
        {
            GameStateChanged?.Invoke(this, new GameStateChangedEventArgs(message, isGameOver, result));
        }
    }
}
