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
    /// Interaction logic for FinishedView.xaml
    /// </summary>
    public partial class FinishedView : Page
    {
        public FinishedView()
        {
            InitializeComponent();

            // Set the Theme
            string currentTheme = (string)Application.Current.Resources["Theme"];

            // Set the background image based on theme
            BackgroundImg.ImageSource = new BitmapImage(new Uri(@"../../images/" + currentTheme + "/BoardBackground.png", UriKind.Relative));

            // Set winner text.
            List<Player> players = getPlayers();
            if (players.Count == 1)
            {
                setSpWinnerText();
            } else
            {
                setMpWinnerText();
            }
            
        }

        private Player getWinner()
        {
            return (Player)Application.Current.Properties["winner"];
        }

        private List<Player> getPlayers()
        {
            return (List<Player>)Application.Current.Properties["players"];
        }

        private void setSpWinnerText()
        {
            Player winner = getWinner();

            WinnerText.Text = $"Congratulations {winner.getName()}You've finished the game with {winner.getScore()} points!";
        }

        private void setMpWinnerText()
        {

            Player winner = getWinner();
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

            // Set winners
            WinnerText.Text = $"Congratulations {winner.getName()}You've finished the game with {winner.getScore()} points!";

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
    }
}