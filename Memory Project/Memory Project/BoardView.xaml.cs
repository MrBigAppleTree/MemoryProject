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
        GameController controller;
        public BoardView(GameController controller)
        {
            InitializeComponent();
            this.controller = controller;
            try
            {
                string currentTheme = (string)Application.Current.Resources["Theme"];
                Console.WriteLine(currentTheme);
                BackgroundImg.ImageSource = new BitmapImage(new Uri(@"../../images/"+currentTheme+"/BoardBackground.png", UriKind.Relative));
                Console.WriteLine(BackgroundImg.ImageSource);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void addToGrid(int height, int width)
        {
            double percentageHeight = 1 / (double)height;
            double percentageWidth = 1 / (double)width;
            for (int i = 0; i < width; i++)
            {
                GridLength W1 = new GridLength(percentageWidth, GridUnitType.Star);
                playGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = W1 });
            }
            for (int i = 0; i < height; i++)
            {
                GridLength H1 = new GridLength(percentageHeight, GridUnitType.Star);
                playGrid.RowDefinitions.Add(new RowDefinition() { Height = H1 });
            }
            
        }

        public void addCard(Card card)
        {
            Button btn = new Button();
            btn.SetValue(Grid.ColumnProperty, card.getXPos());
            btn.SetValue(Grid.RowProperty, card.getYPos());
            btn.Margin = new Thickness(5);
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(card.getbackImg(), UriKind.Relative));
            btn.Content = img;
            btn.Click += new RoutedEventHandler(card_click);
            playGrid.Children.Add(btn);
            
        }

        private void card_click(object sender, RoutedEventArgs e)
        {
            int x = Grid.GetColumn((Button)sender);
            int y = Grid.GetRow((Button)sender);
            string frontImgPath = controller.getBoard().getFrontImg(x, y);

            Button btn = sender as Button;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(frontImgPath, UriKind.Relative));
            btn.Content = img;
        }

        public void loadPlayers(List<Player> list)
        {
            for (int i=0; i < list.Count; i++)
            {
                PlayerGrid.RowDefinitions.Add(new RowDefinition());
                TextBlock txt = new TextBlock();
                txt.TextAlignment = TextAlignment.Center;
                txt.FontSize = 30;
                txt.Foreground = Brushes.White;
                txt.SetValue(Grid.RowProperty, i);

                txt.Text = list[i].getName();
                txt.Inlines.Add(new LineBreak());
                txt.Inlines.Add(new LineBreak());
                txt.Inlines.Add("Score: " + list[i].getScore());

                PlayerGrid.Children.Add(txt);
            }
        }
    }
}
