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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory_Project
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class PlayNav : Page
    {
        string theme = "MenuBackground";
        public PlayNav()
        {
            InitializeComponent();
        }
        private void back_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));

        }
        public void new_click(object sender, RoutedEventArgs e)
        {
            int height = Convert.ToInt32(comboHeight.Text);
            int width = Convert.ToInt32(comboWidth.Text);
            GameController controller = new GameController(height, width);
            BoardView b = controller.getView();
            this.NavigationService.Navigate(b);
        }
    }
}
