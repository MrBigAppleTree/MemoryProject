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

            // Set winners
            WinnerText.Text = $"Congratulations {winner.getName()}You've finished the game with {winner.getScore()} points!";

            // set other players + points in table. Left = name and Right = score + "pts"


        }
    }
}
