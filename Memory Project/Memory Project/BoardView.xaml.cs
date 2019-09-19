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
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Page
    {

        public BoardView()
        {
            InitializeComponent();
            try
            {
                string currentTheme = (string)Application.Current.Resources["Theme"];
                BackgroundImg.Source = new BitmapImage(new Uri(@"Images/" + currentTheme + "/MenuBackground.png", UriKind.Relative));
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void addToGrid(int height, int width)
        {
            for (int i = 0; i < width; i++)
            {
                playGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < height; i++)
            {
                playGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        public void addCard(Card card)
        {
            Button btn = new Button();
            btn.SetValue(Grid.ColumnProperty, card.getXPos());
            btn.SetValue(Grid.RowProperty, card.getYPos());
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(card.getbackImg(), UriKind.Relative));
            img.Stretch = Stretch.Fill;
            btn.Content = img;
            btn.Click += new RoutedEventHandler(card_click);
            playGrid.Children.Add(btn);
        }

        private void card_click(object sender, RoutedEventArgs e)
        {
            int x = Grid.GetColumn((Button)sender);
            int y = Grid.GetRow((Button)sender);

        }
    }
}
