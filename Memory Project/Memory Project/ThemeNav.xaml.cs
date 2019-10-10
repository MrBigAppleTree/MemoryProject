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

namespace Memory_Project
{
    /// <summary>
    /// Interaction logic for HighNav.xaml
    /// </summary> 
    public partial class HighNav : Page
    {
        public Image CardImg { get; set; }
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
            string currentTheme = (string)Application.Current.Resources["Theme"];
            BackgroundImg.Source = new BitmapImage(new Uri(@"../../images/" + currentTheme + "/MenuBackground.png", UriKind.Relative));
            startMusic(currentTheme);
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

        private void startMusic(string theme)
        {
            try
            {

                SoundPlayer player = new SoundPlayer();
                player.Stop();
                player.SoundLocation = "music/" + theme + "/BackgroundMusic.wav";
                player.PlayLooping();
            }
            catch (Exception e) { }
        }
    }
}