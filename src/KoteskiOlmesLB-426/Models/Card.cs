using System;
using System.Windows.Media;
using KoteskiOlmesLB_426.Models;

namespace KoteskiOlmesLB_426.Models
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

        public string DisplayValue => Value switch
        {
            CardValue.Ace => "A",
            CardValue.King => "K",
            CardValue.Queen => "Q",
            CardValue.Jack => "J",
            _ => ((int)Value).ToString()
        };

        public string SuitSymbol => Suit switch
        {
            CardSuit.Hearts => "♥",
            CardSuit.Diamonds => "♦",
            CardSuit.Clubs => "♣",
            CardSuit.Spades => "♠",
            _ => "?"
        };

        public Brush SuitColor => Suit switch
        {
            CardSuit.Hearts or CardSuit.Diamonds => Brushes.Red,
            CardSuit.Clubs or CardSuit.Spades => Brushes.Black,
            _ => Brushes.Gray
        };
    }
}