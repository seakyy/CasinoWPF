using KoteskiOlmesLB_426.Models;

namespace KoteskiOlmesLB_426.Games
{
    public class ComputerPlayer : Player
    {
        public List<Card> Hand { get; private set; }
        public bool HasStood { get; private set; }
        public int CurrentBet { get; private set; }

        public ComputerPlayer(string name, int initialBalance) : base(name, initialBalance)
        {
            Hand = new List<Card>();
            HasStood = false;
        }

        public void ClearHand()
        {
            Hand = new List<Card>();
            HasStood = false;
        }

        public void AddCard(Card card)
        {
            Hand.Add(card);
        }

        public void SetBet(int amount)
        {
            if (PlaceBet(amount))
            {
                CurrentBet = amount;
            }
        }

        public void Stand()
        {
            HasStood = true;
        }
    }
}