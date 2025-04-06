using KoteskiOlmesLB_426.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KoteskiOlmesLB_426.Views
{
    public partial class GameSelectionView : Page
    {
        public GameSelectionView()
        {
            InitializeComponent();
            DataContext = new GameSelectionViewModel();
        }
    }
}
