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
using System.Media;
using System.IO;

namespace Memory_Project
{
    /// <summary>
    /// Interaction logic for HighNav.xaml
    /// </summary> 
    public partial class HighNav : Page
    {
        int cardNumber;
        int maxCards;
        string currentTheme;
        
        public HighNav()
        {

            SetTheme();

        }
        /// <summary>
        /// Navigates to the previous page visited by the user
        /// </summary>
        /// <param name="sender">Back button</param>
        /// <param name="e">Event arguments</param>
        private void back_click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));

        }

        /// <summary>
        /// Constructor for the theme page
        /// </summary>
        private void SetTheme()
        {

            InitializeComponent();

            currentTheme = (string)Application.Current.Resources["Theme"];
            maxCards = (Directory.GetFiles("../../images/" + currentTheme).Length) - 3;
            BackgroundImg.Source = new BitmapImage(new Uri(@"../../images/" + currentTheme + "/MenuBackground.png", UriKind.Relative));

            ConfigView config = new ConfigView();
            config.MusicToggle(currentTheme);

            SelectCard();

        }

        /// <summary>
        /// changes the Theme stored in app.xaml to LOTR, also reloads the theme and music
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LOTR_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["Theme"] = "LOTR";
            SetTheme();
        }

        /// <summary>
        /// changes the Theme stored in app.xaml to Avatar, also reloads the theme and music
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Avatar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["Theme"] = "Avatar";
            SetTheme();
        }

        /// <summary>
        /// changes the Theme stored in app.xaml to NHLStenden, also reloads the theme and music
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NHLStenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["Theme"] = "NHLStenden";
            SetTheme();
        }
        /// <summary>
        /// The initial card preview
        /// </summary>
        private void SelectCard()
        {
            cardNumber = 1;
            string frontpath = "/images/" + currentTheme + "/card" + cardNumber + ".png";
            CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Displays the next card in the theme, when the next button is clicked by the user
        /// </summary>
        /// <param name="sender">Next button</param>
        /// <param name="e">Event arguments</param>
        private void next_click(object sender, RoutedEventArgs e)
        {

            if (cardNumber < maxCards)
            {
                cardNumber++;
                
                string frontpath = "/images/" + currentTheme + "/card" + cardNumber + ".png";
                CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));
            }
            else
            {
                cardNumber = 1;
                
                string frontpath = "/images/" + currentTheme + "/card" + cardNumber + ".png";
                CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));
            }

        }
        /// <summary>
        /// Displays the previous card in the theme, when the next button is clicked by the user
        /// </summary>
        /// <param name="sender">Previous button</param>
        /// <param name="e">Event arguments</param>
        private void prev_click(object sender, RoutedEventArgs e)
        {

            if (cardNumber > 1)
            {
                cardNumber--;
                string frontpath = "/images/" + currentTheme + "/card" + cardNumber + ".png";
                CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));
            }

            else
            {
                cardNumber = maxCards;
                string frontpath = "/images/" + currentTheme + "/card" + cardNumber + ".png";
                CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));
            }

        }
    }
}