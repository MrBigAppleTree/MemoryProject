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

        int turnCounter = 0;

        List<Player> players;
        Player currentPlayer;

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
            img.Source = new BitmapImage(new Uri(card.getBackImg(), UriKind.Relative));
            btn.Content = img;
            btn.Click += new RoutedEventHandler(card_click);
            playGrid.Children.Add(btn);
        }

        private void card_click(object sender, RoutedEventArgs e)
        {
            if(currentPlayer.getClickedBtns().Count < 2)
            {
                int x = Grid.GetColumn((Button)sender);
                int y = Grid.GetRow((Button)sender);
                string frontImgPath = controller.getBoard().getFrontImg(x, y);

                Button btn = sender as Button;
                if(currentPlayer.getClickedBtns().Count > 0 && currentPlayer.getClickedBtns()[0].Equals(btn)) { return; }
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(frontImgPath, UriKind.Relative));
                btn.Content = img;
                Console.WriteLine(((Image)btn.Content).Source);
                this.NavigationService.Refresh();
                currentPlayer.getClickedBtns().Add(btn);
                turnCheck();
                return;
            }
        }

        private async void turnCheck()
        {
            await Task.Delay(2500);
            if (currentPlayer.getClickedBtns().Count == 2 && compareCards(currentPlayer.getClickedBtns()))
            {
                List<Card> gainedCards = new List<Card>();
                foreach(Button b in currentPlayer.getClickedBtns())
                {
                    b.Visibility = Visibility.Hidden;
                    Card c = controller.btnToCard(b);
                    gainedCards.Add(c);
                    controller.removeCard(c);
                    
                }
                Console.WriteLine("cards moved to player: " + currentPlayer.getName());
                currentPlayer.increaseScore(100);
                updateScore();
                currentPlayer.addCards(gainedCards);
                turnHandler();
            } else if (currentPlayer.getClickedBtns().Count == 2)
            {
                foreach (Button b in currentPlayer.getClickedBtns())
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(controller.btnToCard(b).getBackImg(), UriKind.Relative));
                    b.Content = img;
                }
                turnCounter += 1;
                turnHandler();
            }
        }

        private void turnHandler()
        {
            if (controller.gameFin())
            {
                playGrid.Children.Clear();
                currentPlayer.getClickedBtns().Clear();
                Player winner = determineWinner();
                Console.WriteLine("Congratulations Winner:\n" + winner.getName());
            } else
            {
                currentPlayer = players[turnCounter % players.Count];
                currentPlayer.getClickedBtns().Clear();
                setColor(turnCounter % players.Count);
                Console.WriteLine("Player: " + currentPlayer.getName() + " turn");
            }
        }

        public void loadPlayers(List<Player> list)
        {
            players = list;
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
               // txt.Inlines.Add(new LineBreak());
                txt.Inlines.Add("Score: " + list[i].getScore());

                PlayerGrid.Children.Add(txt);
            }

            turnHandler();
        }
        public void loadButtons()
        {
            string[] bNames = new string[] { "Back", "Save", "Reset" };

            for(int i = 0; i < 3; i++)
            {  
                NavGrid.ColumnDefinitions.Add(new ColumnDefinition());
                Button b = new Button();
                b.Content = bNames[i];
                b.Click += btnClicks; //What happends when you click

                //BUTTON CSS HERE

                b.Margin = new Thickness(5);
                b.Padding = new Thickness(5);
                b.FontSize = 30;

                //===============
                int countPlayers = players.Count();
                b.SetValue(Grid.ColumnProperty, i);
                NavGrid.Children.Add(b);
            }
           
        }
        public void btnClicks(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));
        }
        private void setColor(int i)
        {
            TextBlock txt = (TextBlock)PlayerGrid.Children[i];
            foreach(UIElement e in PlayerGrid.Children){
                TextBlock t = (TextBlock)e;
                t.Foreground = Brushes.White;
            }
            txt.Foreground = Brushes.Yellow;
        }

        private void updateScore()
        {
            for(int i = 0; i < players.Count; i++)
            {
                TextBlock txt = (TextBlock)PlayerGrid.Children[i];
                txt.Inlines.Remove(txt.Inlines.LastInline);
                txt.Inlines.Add("Score: " + players[i].getScore());
            }
        }

        private bool compareCards(List<Button> cards)
        {
            try
            {
                return controller.btnToCard(cards[0]).getFrontImg().Equals(controller.btnToCard(cards[1]).getFrontImg());
            } catch(NullReferenceException e)
            {
                return false;
            }
            
        }

        private Player determineWinner()
        {
            Player winner = players.Aggregate((player1, player2) => player1.getScore() > player2.getScore() ? player1 : player1);
            return winner;
        }
    }
}
