using CasinoWPF.Games;
using CasinoWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWPF.Services
{
    // GameManager - Verwaltet Spielstarts, Einsätze, etc.
    // Implementiert auch das Observer-Pattern, um über Änderungen in der Session informiert zu werden
    public class GameManager : IObserver<SessionData>
    {
        private static readonly Lazy<GameManager> _instance = new Lazy<GameManager>(() => new GameManager());

        public static GameManager Instance => _instance.Value;

        private readonly Session _session;

        private GameManager()
        {
            _session = Session.Instance;
            _session.AddObserver(this);
        }

        // IObserver Implementation
        public void Update(SessionData data)
        {
            // Reagiere auf Änderungen in der Session
            // z.B. wenn ein neues Spiel ausgewählt wurde
            if (data.CurrentGameType.HasValue && data.CurrentGame == null)
            {
                CreateGameInstance(data.CurrentGameType.Value);
            }
        }

        // Erstellt eine neue Spielinstanz mit dem Factory-Pattern
        private void CreateGameInstance(GameType gameType)
        {
            var game = GameFactory.Instance.CreateGame(gameType);
            _session.CurrentGame = game;
        }

        // Startet ein bestimmtes Spiel
        public bool StartGame(GameType gameType, int betAmount)
        {
            if (_session.CurrentPlayer == null || betAmount <= 0)
                return false;

            // Prüfe, ob der Spieler genug Guthaben hat
            if (_session.CurrentPlayer.Balance < betAmount)
                return false;

            // Erstelle das Spiel, wenn nötig
            if (_session.CurrentGameType != gameType || _session.CurrentGame == null)
            {
                _session.CurrentGameType = gameType;
                CreateGameInstance(gameType);
            }

            // Starte das Spiel
            var started = _session.CurrentGame.StartGame(_session.CurrentPlayer, betAmount);
            if (started)
            {
                _session.LastBet = betAmount;
            }

            return started;
        }

        // Beendet das aktuelle Spiel
        public GameResult EndCurrentGame()
        {
            if (_session.CurrentGame == null || !_session.CurrentGame.IsGameRunning)
                return null;

            var result = _session.CurrentGame.EndGame();
            _session.LastResult = result;

            // Füge eventuellen Gewinn zum Guthaben des Spielers hinzu
            if (result.IsWin)
            {
                _session.CurrentPlayer.AddWinnings(result.WinAmount);
            }

            return result;
        }

        // Führt eine Aktion im aktuellen Spiel aus
        public bool ExecuteGameAction(string actionName, params object[] parameters)
        {
            if (_session.CurrentGame == null || !_session.CurrentGame.IsGameRunning)
                return false;

            return _session.CurrentGame.ExecuteAction(actionName, parameters);
        }
    }
}
