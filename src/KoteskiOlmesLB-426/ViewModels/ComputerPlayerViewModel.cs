// ComputerPlayerViewModel.cs
using System.Collections.ObjectModel;
using KoteskiOlmesLB_426.Games;
using KoteskiOlmesLB_426.Models;

namespace KoteskiOlmesLB_426.ViewModels
{
    public class ComputerPlayerViewModel : ViewModelBase
    {
        private readonly ComputerPlayer _computerPlayer;
        private ObservableCollection<Card> _cards;

        public string Name => _computerPlayer.Name;
        public int HandValue => CalculateHandValue(_computerPlayer.Hand);

        public ObservableCollection<Card> Cards
        {
            get => _cards;
            set => SetProperty(ref _cards, value);
        }

        public ComputerPlayerViewModel(ComputerPlayer computerPlayer)
        {
            _computerPlayer = computerPlayer;
            Cards = new ObservableCollection<Card>(computerPlayer.Hand);
        }

        private int CalculateHandValue(List<Card> hand)
        {
            int value = 0;
            int aces = 0;

            foreach (var card in hand)
            {
                switch (card.Value)
                {
                    case CardValue.Ace:
                        value += 11;
                        aces++;
                        break;
                    case CardValue.Jack:
                    case CardValue.Queen:
                    case CardValue.King:
                        value += 10;
                        break;
                    default:
                        value += (int)card.Value;
                        break;
                }
            }

            while (value > 21 && aces > 0)
            {
                value -= 10;
                aces--;
            }

            return value;
        }
    }
}