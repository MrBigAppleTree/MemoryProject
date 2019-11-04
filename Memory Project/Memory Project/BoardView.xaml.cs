using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Memory_Project
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Page
    {
        GameController controller;

        public int turnCounter = 0;
        string theme;
        private bool select = false;

        List<Player> players;
        Player currentPlayer;

        /// <summary>
        /// Makes a new instance of the boardview upon which the game is played
        /// </summary>
        /// <param name="controller">The controller who initialised this</param>
        /// <param name="theme">The set theme of the game</param>
        public BoardView(GameController controller, string theme)
        {
            InitializeComponent();
            this.controller = controller;
            try
            {
                this.theme = theme;
                Console.WriteLine(theme);
                BackgroundImg.ImageSource = new BitmapImage(new Uri(@"../../images/"+theme+"/BoardBackground.png", UriKind.Relative));
                Console.WriteLine(BackgroundImg.ImageSource);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Makes grid rows and columns based on the desired height and width
        /// </summary>
        /// <param name="height">height of the board measured in cards</param>
        /// <param name="width">width of the board measured in cards</param>
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

        /// <summary>
        /// Adds cards from the board onto the boardview by converting them to a button with an image as content
        /// </summary>
        /// <param name="card">The card to be converted and put on te board</param>
        public void addCard(Card card)
        {
            Button btn = new Button();
            btn.SetValue(Grid.ColumnProperty, card.getXPos());
            btn.SetValue(Grid.RowProperty, card.getYPos());
            btn.Margin = new Thickness(5);
            btn.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            btn.BorderThickness = new Thickness(0);
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(card.getBackImg(), UriKind.Relative));
            img.Stretch = Stretch.Fill;
            btn.Content = img;
            btn.Background = Brushes.Transparent;
            btn.Click += new RoutedEventHandler(card_click);
            playGrid.Children.Add(btn);
        }

        /// <summary>
        /// Event handler that triggers whenever a card(button) is clicked on the board.
        /// Also handles the selection of the lonely (odd) card on odd numbered boards.
        /// </summary>
        /// <param name="sender">The button that has been clicked</param>
        /// <param name="e">Event arguments</param>
        private void card_click(object sender, RoutedEventArgs e)
        {
            if (select && currentPlayer.getClickedBtns().Count == 0)
            {
                Button btn = sender as Button;
                if (controller.btnToCard(btn).isLonely())
                {
                    LonelyCard(btn);
                }
                else
                {
                    turnCounter++;
                    turnHandler();
                }
                select = false;
                this.IsHitTestVisible = true;
                return;
            }
            else if (select)
            {
                select = false;
                return;
            }

            if(currentPlayer.getClickedBtns().Count < 2)
            {
                this.IsHitTestVisible = false;
                int x = Grid.GetColumn((Button)sender);
                int y = Grid.GetRow((Button)sender);
                string frontImgPath = controller.getBoard().getFrontImg(x, y);

                Button btn = sender as Button;
                if (currentPlayer.getClickedBtns().Count > 0 && currentPlayer.getClickedBtns()[0].Equals(btn))
                {
                    this.IsHitTestVisible = true;
                    return;
                }
                flipCard(btn, frontImgPath);
                currentPlayer.getClickedBtns().Add(btn);
                turnCheck();
            }
        }
        /// <summary>
        /// Handles the animation, related score increase and removal of the single card on boards with odd amounts of cards.
        /// Only triggers via the 'Find Lonely Card' button
        /// </summary>
        /// <param name="btn">The button which is the lonely card</param>
        private async void LonelyCard(Button btn)
        {
            this.IsHitTestVisible = false;
            await flipCard(btn, controller.btnToCard(btn).getFrontImg());
            await Task.Delay(500);
            currentPlayer.increaseScore(scoreincrease() * 2);
            updateScore();
            controller.removeCard(controller.btnToCard(btn));
            btn.Visibility = Visibility.Hidden;
            btnGrid.Children.Clear();
        }

        /// <summary>
        /// Checks whether a players turn has ended or is still ongoing.
        /// If two cards have been selected, the card faces will be compared. If equal, these cards willbe added to the players card stack
        /// and removed from the board. If not equal, both cards will be turned back around.
        /// </summary>
        private async void turnCheck()
        {
            this.IsHitTestVisible = true;
            //Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);
            
            if (currentPlayer.getClickedBtns().Count == 2 && compareCards(currentPlayer.getClickedBtns()))
            {
                await Task.Delay(2000);
                List<Card> gainedCards = new List<Card>();
                foreach(Button b in currentPlayer.getClickedBtns())
                {
                    b.Visibility = Visibility.Hidden;
                    Card c = controller.btnToCard(b);
                    gainedCards.Add(c);
                    controller.removeCard(c);
                }
                Console.WriteLine("cards moved to player: " + currentPlayer.getName());
                currentPlayer.increaseScore(scoreincrease());
                updateScore();
                currentPlayer.addCards(gainedCards);
                turnHandler();
            }
            else if (currentPlayer.getClickedBtns().Count == 2)
            {
                await Task.Delay(2000);
                foreach (Button b in currentPlayer.getClickedBtns())
                {
                    flipCard(b, controller.btnToCard(b).getBackImg());
                }
                turnCounter += 1;
                turnHandler();
            }
            
        }
        /// <summary>
        /// determines how much the score is increased by depending on the amount of turns that have passed and the total amount of cards
        /// for example: with a 4x4 boardsize you wil get 125 points per pair until turn 16, the 100 points until turn 32 after which you'll get 50 points
        /// </summary>
        /// <returns>returns an int with the score increase</returns>
        private int scoreincrease()
        {
            //turnCounter
            double totalcards = controller.getHeight() + controller.getWidth();
            double temp1 = (totalcards / 4);
            double temp2 = totalcards / 2;
            if (turnCounter < temp1)
            {
                return 125;
            }
            else if (turnCounter > temp2)
            {
                return 50;
            }
            else
            {
                return 100;
            }
        }

        /// <summary>
        /// The animation component to make the card flip around to the input image
        /// </summary>
        /// <param name="btn">The button to be flipped</param>
        /// <param name="imgPath">The image to flip the card to</param>
        private async Task flipCard(Button btn, string imgPath)
        {
            double normalWidth = btn.ActualWidth;
            int animationTimeMillis = 1000;

            playGrid.IsHitTestVisible = false;
            DoubleAnimation da1 = new DoubleAnimation();
            da1.From = normalWidth;
            da1.To = 0;
            da1.Duration = new Duration(new TimeSpan(0,0,0,0, animationTimeMillis));
            btn.BeginAnimation(FrameworkElement.WidthProperty, da1);
            await Task.Delay(animationTimeMillis + 100);

            Image img = new Image();
            img.Source = new BitmapImage(new Uri(imgPath, UriKind.Relative));
            img.Stretch = Stretch.Fill;
            btn.Content = img;

            DoubleAnimation da2 = new DoubleAnimation();
            da2.From = 0;
            da2.To = normalWidth;
            da2.Duration = new Duration(new TimeSpan(0, 0, 0, 0, animationTimeMillis));
            btn.BeginAnimation(FrameworkElement.WidthProperty, da2);
            await Task.Delay(animationTimeMillis + 100);

            playGrid.IsHitTestVisible = true;
        }

        /// <summary>
        /// Determines whose turn it is and if the game is finished.
        /// </summary>
        private void turnHandler()
        {
            if (controller.gameFin())
            {
                
                currentPlayer.getClickedBtns().Clear();
                List<Player> winner = determineWinner();

                // Clear the main panel of useless controls
                mainPanel.Children.Remove(leftPanel);
                mainPanel.Children.Remove(playGrid);
                foreach (Player p in players)
                {
                    HighScores h = new HighScores(controller.getWidth(), controller.getHeight(), p.getScore(), p.getName(), players);
                }

                // Display the finish screen
                displayFinishScreen(players, winner);

            } else
            {
                controller.Save(turnCounter);
                updateTurnCounter();
                currentPlayer = players[turnCounter % players.Count];
                currentPlayer.getClickedBtns().Clear();
                setColor(turnCounter % players.Count);
                Console.WriteLine("Player: " + currentPlayer.getName() + " turn");
            }
            
        }

        /// <summary>
        /// updates the onscreen turn counter
        /// </summary>
        private void updateTurnCounter()
        {
            TextBlock tcn = (TextBlock)TurnGrid.Children[0];
            tcn.Text = "Turn: " + (turnCounter+1);
        }

        /// <summary>
        /// loads the players onto the board dynamically
        /// </summary>
        /// <param name="list">The list of players to be loaded onto the board</param>
        public void loadPlayers(List<Player> list)
        {
            // tcn = turn counter display
            TextBlock tcn = new TextBlock();
            tcn.Name = "tcn";
            tcn.TextAlignment = TextAlignment.Center;
            tcn.FontSize = 30;
            tcn.Foreground = Brushes.White;
            tcn.Text = "Turn: " + (turnCounter+1);
            TurnGrid.Children.Add(tcn);

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
                txt.Inlines.Add("Score: " + list[i].getScore());
                txt.Inlines.Add(new LineBreak());                

                PlayerGrid.Children.Add(txt);
            }

            turnHandler();
        }

        /// <summary>
        /// Loads the back, save and reset buttons onto the screen below the players.
        /// </summary>
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
        /// <summary>
        /// Loads the 'Find lonely card' button onscreen incase of an uneven numbered board.
        /// </summary>
        public void loadUnevenButton()
        {
            Button btn = new Button();
            btn.Content = "Find the lonely card!";
            btn.Click += unevenClick;

            btn.Margin = new Thickness(5);
            btn.Padding = new Thickness(5);
            btn.FontSize = 30;

            btnGrid.Children.Add(btn);
        }

        /// <summary>
        /// Event handler for the 'Find lonely card' button.
        /// Sets select boolean to make the card click handler deal with the upcoming single card click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void unevenClick(object sender, RoutedEventArgs e)
        {
            select = true;
        }

        /// <summary>
        /// Handles the interactions for the back, save and load buttons
        /// </summary>
        /// <param name="sender">The pressed button</param>
        /// <param name="e"></param>
        public void btnClicks(object sender, RoutedEventArgs e)
        {
            switch ((string)((Button)sender).Content)
            {
                case "Back":
                    controller.Save(turnCounter);
                    NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));
                    ConfigView config = new ConfigView();
                    config.MusicToggle((string)Application.Current.Resources["Theme"]);
                    break;

                case "Save":
                    controller.Save(turnCounter);
                    break;

                case "Reset":

                    if (currentPlayer.getClickedBtns().Count == 0)
                    {
                        foreach (Player p in players)
                        {
                            p.setScore(0);
                        }

                        int cardX = (int)Application.Current.Resources["cardX"];
                        int cardY = (int)Application.Current.Resources["cardY"];
                        string theme = (string)Application.Current.Resources["Theme"];

                        GameController resetController = new GameController(cardY, cardX, players, theme, controller.getSerializer());
                        this.NavigationService.Navigate(resetController.getView());

                        break;
                    }
                    else
                    {
                        break;
                    }
            }
            
        }

        /// <summary>
        /// Sets all player text to white, then sets current players color to yellow.
        /// </summary>
        /// <param name="i">The index of the player whose turn it is</param>
        private void setColor(int i)
        {
            TextBlock txt = (TextBlock)PlayerGrid.Children[i];
            foreach(UIElement e in PlayerGrid.Children){
                TextBlock t = (TextBlock)e;
                t.Foreground = Brushes.White;
            }
            txt.Foreground = Brushes.Yellow;
        }

        /// <summary>
        /// Updates the player scores on the board after every turn.
        /// </summary>
        private void updateScore()
        {
            for(int i = 0; i < players.Count; i++)
            {
                TextBlock txt = (TextBlock)PlayerGrid.Children[i];
                txt.Inlines.Remove(txt.Inlines.LastInline);
                txt.Inlines.Remove(txt.Inlines.LastInline);
                txt.Inlines.Add("Score: " + players[i].getScore());
                txt.Inlines.Add(new LineBreak());
            }
        }

        /// <summary>
        /// Compares the imput cards and determines if the front face is the same.
        /// </summary>
        /// <param name="cards">The list of cards to be compared with one another</param>
        /// <returns>Returns whether the two cards are equal</returns>
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

        /// <summary>
        /// Determines the player (or players) with the highest score 
        /// </summary>
        /// <returns>Returns the player with the highest score or players with the highest score if tied</returns>
        private List<Player> determineWinner()
        {

            int highestScore = 0;
            List<Player> winnerList = new List<Player>();

            foreach (Player p in players)
            {
                if (p.getScore() > highestScore)
                {
                    highestScore = p.getScore();
                    winnerList.Clear();
                    winnerList.Add(p);

                } else if (p.getScore() == highestScore && p.getScore() > 0)
                {
                    winnerList.Add(p);
                }
            }

            return winnerList;
        }

        /// <summary>
        ///     Write the players and winners in application memory and 
        ///     navigate to the finish screen by creating a new finishedView object
        /// </summary>
        /// <param name="players">All the players in the game</param>
        /// <param name="winner">The winner (or winners if tied) in the game</param>
        private void displayFinishScreen(List<Player> players, List<Player> winner)
        {
            Application.Current.Properties["players"] = players;
            Application.Current.Properties["winner"] = winner;

            FinishedView finished = new FinishedView(this.theme, controller.getSerializer(), controller.getHeight(), controller.getWidth());
            NavigationService.Navigate(finished);
            //NavigationService.Navigate(new Uri("FinishedView.xaml", UriKind.Relative));
        }
    }
}
