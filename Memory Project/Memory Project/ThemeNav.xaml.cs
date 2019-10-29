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
        int n;
        int maxCards;
        string currentTheme;
        
        public HighNav()
        {
            CheckTheme();
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));
        }

        private void CheckTheme()
        {
            InitializeComponent();
            currentTheme = (string)Application.Current.Resources["Theme"];
            maxCards = (Directory.GetFiles("../../images/" + currentTheme).Length) - 3;
            BackgroundImg.Source = new BitmapImage(new Uri(@"../../images/" + currentTheme + "/MenuBackground.png", UriKind.Relative));
            startMusic(currentTheme);
            SelectCard();
        }


        private void LOTR_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["Theme"] = "LOTR";
            CheckTheme();
        }

        private void Avatar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["Theme"] = "Avatar";
            CheckTheme();
        }

        private void NHLStenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["Theme"] = "NHLStenden";
            CheckTheme();
        }
        private void SelectCard()
        {
            
            n= 1;
            string frontpath = "/images/" + currentTheme + "/card" + n + ".png";
            CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));

        }
        private void next_click(object sender, RoutedEventArgs e)
        {
            if (n < maxCards)
            {
                n++;
                Console.WriteLine(n);
                string frontpath = "/images/" + currentTheme + "/card" + n + ".png";
                CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));
            }
            else
            {
                n = 1;
                Console.WriteLine(n);
                string frontpath = "/images/" + currentTheme + "/card" + n + ".png";
                CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));
            }

        }
        private void prev_click(object sender, RoutedEventArgs e)
        {
            if (n > 1)
            {
                n--;
                string frontpath = "/images/" + currentTheme + "/card" + n + ".png";
                CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));
            }
            else
            {
                n = maxCards;
                Console.WriteLine(n);
                string frontpath = "/images/" + currentTheme + "/card" + n + ".png";
                CardPreview.Source = new BitmapImage(new Uri(frontpath, UriKind.RelativeOrAbsolute));
            }
        }

        private void startMusic(string theme)
        {
            try
            {

                SoundPlayer player = new SoundPlayer();
                player.Stop();
                player.SoundLocation = "music/" + theme + "/BackgroundMusic.wav";
                player.PlayLooping();
            }
            catch (Exception e)
            {
            }
        }

        private void Mute_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}