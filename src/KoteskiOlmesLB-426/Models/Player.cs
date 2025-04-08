using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KoteskiOlmesLB_426.Models
{
    public class Player : INotifyPropertyChanged
    {
        private string _name;
        private int _balance;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Balance
        {
            get => _balance;
            set
            {
                if (_balance != value)
                {
                    _balance = value;
                    OnPropertyChanged();
                }
            }
        }

        public void AddBalance(int amount)
        {
            Balance += amount;
        }


        public Player(string name = "Spieler", int initialBalance = 1000)
        {
            Name = name;
            Balance = initialBalance;
        }

        public bool PlaceBet(int amount)
        {
            if (amount <= 0)
                return false;

            if (amount > Balance)
                return false;

            Balance -= amount;
            return true;
        }

        public void AddWinnings(int amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
