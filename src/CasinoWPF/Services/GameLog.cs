using CasinoWPF.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace CasinoWPF.Services
{
    public class GameLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string GameMode { get; set; }
        public string Message { get; set; }
        public int Bet { get; set; }
        public int Win { get; set; }
    }

    public class GameLogService : INotifyPropertyChanged
    {
        private static readonly Lazy<GameLogService> _instance = new(() => new GameLogService());
        public static GameLogService Instance => _instance.Value;

        public ObservableCollection<GameLogEntry> GameLogEntries { get; } = new();

        private int _startingBalance = 0;
        private int _currentBalance = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        private GameLogService()
        {
            GameLogEntries.CollectionChanged += (s, e) => UpdateStatistics();
        }

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

            // update current balance
            _currentBalance = _currentBalance - bet + win;
            UpdateStatistics();
        }

        public void SetStartingBalance(int balance)
        {
            _startingBalance = balance;
            _currentBalance = balance;
            UpdateStatistics();
        }

        public int StartingBalance => _startingBalance;

        public int TotalSpent => GameLogEntries.Sum(e => e.Bet);

        public int TotalWon => GameLogEntries.Sum(e => e.Win);

        public int TotalLost => TotalSpent - TotalNetWon;

        public int TotalNetWon => GameLogEntries.Sum(e => e.Win - e.Bet);

        public int EndingBalance => _currentBalance;

        public int NetResult => EndingBalance - StartingBalance;

        public Brush NetResultColor => NetResult >= 0 ? Brushes.Green : Brushes.Red;

        public int TotalGames => GameLogEntries.Count;

        public int WonGames => GameLogEntries.Count(e => e.Win > e.Bet);

        public int LostGames => GameLogEntries.Count(e => e.Win <= e.Bet);

        public decimal WinAmountForChart => TotalWon;

        public decimal LossAmountForChart => TotalSpent;

        public decimal WinPercentage => (TotalWon + TotalSpent) > 0 ?
            (decimal)TotalWon / (TotalWon + TotalSpent) * 100 : 0;

        public decimal LossPercentage => (TotalWon + TotalSpent) > 0 ?
            (decimal)TotalSpent / (TotalWon + TotalSpent) * 100 : 0;

        private void UpdateStatistics()
        {
            OnPropertyChanged(nameof(StartingBalance));
            OnPropertyChanged(nameof(TotalSpent));
            OnPropertyChanged(nameof(TotalWon));
            OnPropertyChanged(nameof(TotalLost));
            OnPropertyChanged(nameof(TotalNetWon));
            OnPropertyChanged(nameof(EndingBalance));
            OnPropertyChanged(nameof(NetResult));
            OnPropertyChanged(nameof(NetResultColor));
            OnPropertyChanged(nameof(TotalGames));
            OnPropertyChanged(nameof(WonGames));
            OnPropertyChanged(nameof(LostGames));
            OnPropertyChanged(nameof(WinAmountForChart));
            OnPropertyChanged(nameof(LossAmountForChart));
            OnPropertyChanged(nameof(WinPercentage));
            OnPropertyChanged(nameof(LossPercentage));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}