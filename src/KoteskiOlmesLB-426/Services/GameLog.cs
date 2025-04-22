using KoteskiOlmesLB_426.Models;
using System;
using System.Collections.ObjectModel;

namespace KoteskiOlmesLB_426.Services
{
    public class GameLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string GameMode { get; set; }
        public string Message { get; set; }
        public int Bet { get; set; }
        public int Win { get; set; }
    }

    public class GameLogService
    {
        private static readonly Lazy<GameLogService> _instance = new(() => new GameLogService());
        public static GameLogService Instance => _instance.Value;

        public ObservableCollection<GameLogEntry> GameLogEntries { get; } = new();

        private GameLogService() { }

        public void AddEntry(GameType game, string message, int bet, int win)
        {
            GameLogEntries.Add(new GameLogEntry
            {
                Timestamp = DateTime.Now,
                GameMode = game.ToString(),
                Message = message,
                Bet = bet,
                Win = win
            });
        }
    }
}