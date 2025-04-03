using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoteskiOlmesLB_426.Models
{
    /// <summary>
    /// Typ des Spiels
    /// </summary>
    public enum GameType
    {
        SlotMachine,
        BlackJack
    }

    /// <summary>
    /// Spielergebnisse für BlackJack
    /// </summary>
    public enum BlackJackResult
    {
        Win,
        Lose,
        Push,
        BlackJack,
        Bust
    }

    /// <summary>
    /// Karten-Farben
    /// </summary>
    public enum CardSuit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    /// <summary>
    /// Karten-Werte
    /// </summary>
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
        Jack = 11,    // Diese Werte für die Enum-Definition müssen unterschiedlich sein
        Queen = 12,   // aber in der Spiellogik werden sie als 10 behandelt
        King = 13,
        Ace = 14
    }

    /// <summary>
    /// Symbole für die Slot Machine
    /// </summary>
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

    /// <summary>
    /// Aktion im BlackJack Spiel
    /// </summary>
    public enum BlackJackAction
    {
        Hit,
        Stand,
        DoubleDown,
        Split
    }
}
