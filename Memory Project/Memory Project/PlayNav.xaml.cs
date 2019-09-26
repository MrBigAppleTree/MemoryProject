using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string theme = (string)Application.Current.Resources["Theme"];
        public ObservableCollection<ComboBoxItem> cbItems1 { get; set; }
        public ComboBoxItem SelectedcbItem1 { get; set; }
        public ObservableCollection<ComboBoxItem> cbItems2 { get; set; }
        public ComboBoxItem SelectedcbItem2 { get; set; }

        public PlayNav()
        {
            InitializeComponent();
            comboboxItems1();
            comboboxItems2();

            BackgroundImg.Source = new BitmapImage(new Uri(@"../../images/" + theme + "/MenuBackground.png", UriKind.Relative));

        }
        private void comboboxItems1()
        {
            Dictionary<string, int> maxCards = new Dictionary<string, int>();
            maxCards.Add("LOTR", 32);

            DataContext = this;
            cbItems1 = new ObservableCollection<ComboBoxItem>();
            for (int i = 0; i <= Math.Sqrt(maxCards[theme] * 2); i++)
            {
                if (i == 4)
                {
                    var cbItem = new ComboBoxItem { Content = i };
                    SelectedcbItem1 = cbItem;
                    cbItems1.Add(cbItem);
                }
                else if (i != 0 && i != 1)
                {
                    cbItems1.Add(new ComboBoxItem { Content = i });
                }
            }
            /*
             * 
            Generate Combobox items dependend on theme for the xaml

            TO DO: -Dynamicly fill combobox dependend on height/width selected, for example with 32 cards having a 10*3 board
                   -Chance fetch from Dictionary to global variable

            ~~Thomas branch

            */

        }
        private void comboboxItems2()
        {
            Dictionary<string, int> maxCards = new Dictionary<string, int>();
            maxCards.Add("LOTR", 32);

            DataContext = this;
            cbItems2 = new ObservableCollection<ComboBoxItem>();
            for (int i = 0; i <= Math.Sqrt(maxCards[theme] * 2); i++)
            {
                if (i == 4)
                {
                    var cbItem = new ComboBoxItem { Content = i };
                    SelectedcbItem2 = cbItem;
                    cbItems2.Add(cbItem);
                }
                else if (i != 0 && i != 1)
                {
                    cbItems2.Add(new ComboBoxItem { Content = i });
                }
            }
        }
        private void back_click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));

        }
        public void new_click(object sender, RoutedEventArgs e)
        {
            List<Player> players = new List<Player>();
            int numPlayers = Convert.ToInt32(Players.Text);
            int height = Convert.ToInt32(comboHeight.Text);
            int width = Convert.ToInt32(comboWidth.Text);
 
            for (int i = 0; i < numPlayers; i++)
            {
                TextBox tb = (TextBox)FindName("Player" + (i + 1));
                players.Add(new Player(tb.Text));
            }
            GameController controller = new GameController(height, width, players);
            this.NavigationService.Navigate(controller.getView());
        }
    }
}
