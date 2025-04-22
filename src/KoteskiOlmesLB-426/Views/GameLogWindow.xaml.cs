using System.Windows;
using KoteskiOlmesLB_426.Services;

namespace KoteskiOlmesLB_426.Views
{
    public partial class GameLogWindow : Window
    {
        public GameLogWindow()
        {
            InitializeComponent();
            DataContext = GameLogService.Instance;
        }
    }
}

