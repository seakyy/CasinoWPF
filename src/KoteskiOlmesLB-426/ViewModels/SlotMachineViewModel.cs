using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KoteskiOlmesLB_426.ViewModels
{
    internal class SlotMachineViewModel
    {
        public ICommand ReturnToSelectionCommand { get; }
        public ICommand ReturnToMainMenuCommand { get; }

        public event EventHandler<NavigationEventArgs> NavigationRequested;

        public SlotMachineViewModel()
        {
            ReturnToSelectionCommand = new RelayCommand(_ => OnNavigationRequested("GameSelection"));
            ReturnToMainMenuCommand = new RelayCommand(_ => OnNavigationRequested("MainMenu"));
        }

        private void OnNavigationRequested(string target)
        {
            NavigationRequested?.Invoke(this, new NavigationEventArgs(target));
        }

    }
}
