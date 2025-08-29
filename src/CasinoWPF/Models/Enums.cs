using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWPF.Models
{
    public enum GameType
    {
        SlotMachine,
        BlackJack
    }

    public enum BlackJackResult
    {
        Win,
        Lose,
        Push,
        BlackJack,
        Bust
    }

    public enum CardSuit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum CardValue
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,    // these face cards (jack,queen,king,ace) are treated as 10 in game logic
        Queen = 12,   
        King = 13,
        Ace = 14
    }

    public enum SlotSymbol
    {
        Cherry,
        Lemon,
        Orange,
        Plum,
        Bell,
        Bar,
        Seven,
        Diamond
    }

    public enum BlackJackAction
    {
        Hit,
        Stand,
        DoubleDown,
        Split
    }
}