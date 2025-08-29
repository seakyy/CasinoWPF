using CasinoWPF.Games;
using CasinoWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWPF.Services
{
    // implemented observer pattern to react to session changes
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

        // IObserver implementation
        public void Update(SessionData data)
        {
            // for example, if the game type changes, we might want to create a new game instance
            if (data.CurrentGameType.HasValue && data.CurrentGame == null)
            {
                CreateGameInstance(data.CurrentGameType.Value);
            }
        }

        // factory pattern to create game instances
        private void CreateGameInstance(GameType gameType)
        {
            var game = GameFactory.Instance.CreateGame(gameType);
            _session.CurrentGame = game;
        }

        public bool StartGame(GameType gameType, int betAmount)
        {
            if (_session.CurrentPlayer == null || betAmount <= 0)
                return false;

            if (_session.CurrentPlayer.Balance < betAmount)
                return false;

            if (_session.CurrentGameType != gameType || _session.CurrentGame == null)
            {
                _session.CurrentGameType = gameType;
                CreateGameInstance(gameType);
            }

            // start game
            var started = _session.CurrentGame.StartGame(_session.CurrentPlayer, betAmount);
            if (started)
            {
                _session.LastBet = betAmount;
            }

            return started;
        }

        public GameResult EndCurrentGame()
        {
            if (_session.CurrentGame == null || !_session.CurrentGame.IsGameRunning)
                return null;

            var result = _session.CurrentGame.EndGame();
            _session.LastResult = result;

            if (result.IsWin)
            {
                _session.CurrentPlayer.AddWinnings(result.WinAmount);
            }

            return result;
        }

        public bool ExecuteGameAction(string actionName, params object[] parameters)
        {
            if (_session.CurrentGame == null || !_session.CurrentGame.IsGameRunning)
                return false;

            return _session.CurrentGame.ExecuteAction(actionName, parameters);
        }
    }
}
