using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWPF.Models
{
    public class GameResult
    {
        public bool IsWin { get; set; }
        public int WinAmount { get; set; }
        public int BetAmount { get; set; }
        public GameType GameType { get; set; }
        public string ResultDescription { get; set; }
        public DateTime Timestamp { get; set; }

        // Für spezifische Spielergebnisse (z.B. Kartenwerte bei BlackJack)
        public Dictionary<string, object> GameSpecificResults { get; set; }

        public GameResult()
        {
            Timestamp = DateTime.Now;
            GameSpecificResults = new Dictionary<string, object>();
        }

        public int NetProfit => IsWin ? WinAmount - BetAmount : -BetAmount;

        public override string ToString()
        {
            string result = IsWin ? "Gewonnen" : "Verloren";
            return $"[{GameType}] {result}: {(IsWin ? "+" : "-")}{Math.Abs(NetProfit)} Jetons. {ResultDescription}";
        }
    }
}