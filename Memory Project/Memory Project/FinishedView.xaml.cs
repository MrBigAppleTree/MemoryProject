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
    public partial class FinishedView : Page
    {
        public FinishedView()
        {
            InitializeComponent();

            // Set the Theme
            string currentTheme = (string)Application.Current.Resources["Theme"];

            // Set the background image based on theme
            BackgroundImg.ImageSource = new BitmapImage(new Uri(@"../../images/" + currentTheme + "/BoardBackground.png", UriKind.Relative));

            // Set winner text based on gamemode.
            setWinnerText();

        }

        private List<Player> getWinner()
        {
            return (List<Player>)Application.Current.Properties["winner"];
        }

        private List<Player> getPlayers()
        {
            return (List<Player>)Application.Current.Properties["players"];
        }

        private void setWinnerText()
        {

            List<Player> winner = getWinner();
            List<Player> players = getPlayers();

            // Count for the forEach to assign rows.
            int count = 0;

            // Create scoreboard grid
            Grid scoreBoard = new Grid()
            {
                Margin = new Thickness() { Top = 10 }
            };

            scoreBoard.ColumnDefinitions.Add(new ColumnDefinition());
            scoreBoard.ColumnDefinitions.Add(new ColumnDefinition());

            if (winner.Count == 1)
            {
                // Set winner if only 1 player won
                WinnerText.Text = $"Congratulations {winner[0].getName()}! You've finished the game with {winner[0].getScore()} points!".Replace(Environment.NewLine, "");
            } else if (winner.Count == 2)
            {
                // Set winner if 2 players tied.
                WinnerText.Text = $"Congratulations {winner[0].getName()} and {winner[1].getName()}! You've tied the game with {winner[0].getScore()} points!".Replace(Environment.NewLine, "");
            } else
            {
                // Set winners if 3 or more players tied.
                string tiedWinners = "";
                int winnerCount = 0;
                int winnerAmount = winner.Count;
                
                foreach (Player p in winner)
                {   

                    // First winner in sentence doesn't need a comma prefix,
                    // all !first !last winners in sentence don't need the 'and' prefix
                    // Last needs 'and' prefix. winnerAmount - 1 = last
                    if (winnerCount == 0)
                    {
                        tiedWinners += p.getName();

                    } else if (winnerCount != winnerAmount - 1)
                    {
                        tiedWinners += ", " + p.getName();

                    } else 
                    {
                        tiedWinners += " and " + p.getName();

                    }

                    winnerCount++;

                }

                WinnerText.Text = $"Congratulations { tiedWinners }! You've tied the game with {winner[0].getScore()} points!".Replace(Environment.NewLine, "");
            }
            
            playerGrid.RowDefinitions.Add(new RowDefinition());
            playerGrid.Children.Add(scoreBoard);

            Grid.SetRow(menuButtons, 2);
            Grid.SetRow(scoreBoard, 1);

            // Set the name and points for each player and assign them their respective rows.
            foreach (Player p in players)
            {

                Label name = new Label()
                {
                    Content = p.getName(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = Brushes.White,
                    FontSize = 22
                }; 

                Label pts = new Label()
                {
                    Content = p.getScore() + " pts",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = Brushes.White,
                    FontSize = 22
                };

                scoreBoard.RowDefinitions.Add(new RowDefinition());
                scoreBoard.Children.Add(name);
                scoreBoard.Children.Add(pts);

                Grid.SetRow(name, count);
                Grid.SetColumn(pts, 1);
                Grid.SetRow(pts, count);

                count++;

            }
        }

        // Handle the replay button logic (Still needs XMLSerialiser for save.)
        private void replay_Click(object sender, RoutedEventArgs e)
        {

            List<Player> players = getPlayers();

            foreach (Player p in players)
            {
                p.setScore(0);
            }

            int cardX = (int)Application.Current.Resources["cardX"];
            int cardY = (int)Application.Current.Resources["cardY"];
            string theme = (string)Application.Current.Resources["Theme"];

            GameController controller = new GameController(cardX, cardY, players, theme, null);
            this.NavigationService.Navigate(controller.getView());

        }

        // Go back to the main menu.
        private void close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));
        }
    }
}