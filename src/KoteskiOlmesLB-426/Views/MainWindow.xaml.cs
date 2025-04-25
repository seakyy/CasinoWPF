using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KoteskiOlmesLB_426.Views;

namespace KoteskiOlmesLB_426.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new StartPage());
            try
            {
                this.Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/app_icon.ico"
                ));
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine("Error setting the window icon: " + ex.Message);
#else
                // Log the error or suppress it in production
#endif
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown(); 
        }

    }
}
