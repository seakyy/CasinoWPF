using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoteskiOlmesLB_426.Models
{
    public interface IGame
    {
        /// <summary>
        /// Name des Spiels
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Typ des Spiels (aus Enum)
        /// </summary>
        GameType GameType { get; }

        /// <summary>
        /// Beschreibung des Spiels und der Regeln
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Minimaler Einsatz für das Spiel
        /// </summary>
        int MinimumBet { get; }

        /// <summary>
        /// Maximaler Einsatz für das Spiel
        /// </summary>
        int MaximumBet { get; }

        /// <summary>
        /// Gibt an, ob das Spiel gerade läuft
        /// </summary>
        bool IsGameRunning { get; }

        /// <summary>
        /// Startet eine neue Spielrunde mit dem angegebenen Einsatz
        /// </summary>
        /// <param name="player">Der Spieler, der die Runde spielt</param>
        /// <param name="betAmount">Der Einsatz für das Spiel</param>
        /// <returns>True, wenn das Spiel erfolgreich gestartet wurde</returns>
        bool StartGame(Player player, int betAmount);

        /// <summary>
        /// Beendet das aktuelle Spiel und gibt das Ergebnis zurück
        /// </summary>
        /// <returns>Das Ergebnis des Spiels</returns>
        GameResult EndGame();

        /// <summary>
        /// Führt eine spielspezifische Aktion aus
        /// </summary>
        /// <param name="actionName">Name der Aktion (z.B. "Hit", "Stand" bei BlackJack)</param>
        /// <param name="parameters">Optionale Parameter für die Aktion</param>
        /// <returns>True, wenn die Aktion erfolgreich ausgeführt wurde</returns>
        bool ExecuteAction(string actionName, params object[] parameters);

        /// <summary>
        /// Event, das ausgelöst wird, wenn sich der Status des Spiels ändert
        /// </summary>
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
