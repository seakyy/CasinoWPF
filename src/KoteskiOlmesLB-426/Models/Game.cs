using System;
using KoteskiOlmesLB_426.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoteskiOlmesLB_426.Models
{
    public interface IGame
    {
        string Name { get; }
        GameType GameType { get; }
        string Description { get; }
        int MinimumBet { get; }
        int MaximumBet { get; }
        bool IsGameRunning { get; }

        /// <summary>
        /// Startet eine neue Spielrunde mit dem angegebenen Einsatz (Einzelspieler-Modus).
        /// </summary>
        bool StartGame(Player player, int betAmount);

        /// <summary>
        /// Startet eine neue Spielrunde mit Einsatz und optionaler Spieleranzahl (z. B. Multiplayer bei BlackJack).
        /// </summary>
        bool StartGame(Player player, int betAmount, int numberOfPlayers);


        GameResult EndGame();
        bool ExecuteAction(string actionName, params object[] parameters);

        event EventHandler<GameStateChangedEventArgs> GameStateChanged;
    }


    public class GameStateChangedEventArgs : EventArgs
    {
        public string Message { get; set; }
        public bool IsGameOver { get; set; }
        public GameResult Result { get; set; }

        public GameStateChangedEventArgs(string message, bool isGameOver = false, GameResult result = null)
        {
            Message = message;
            IsGameOver = isGameOver;
            Result = result;
        }
    }

}
