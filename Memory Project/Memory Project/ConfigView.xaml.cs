using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Interaction logic for ConfigView.xaml
    /// </summary>
    public partial class ConfigView : Page
    {
        public string currentTheme;
        public ConfigView()
        {
            InitializeComponent();
            currentTheme = (string)Application.Current.Resources["Theme"];
            BackgroundImg.Source = new BitmapImage(new Uri(@"../../images/" + currentTheme + "/MenuBackground.png", UriKind.Relative));
        }
        private void back_click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));

        }
        private void theme_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ThemeNav.xaml", UriKind.Relative));
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void SoundToggle_Checked(object sender, RoutedEventArgs e)
        {
            stopMusic(currentTheme);
        }

        private void SoundToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            startMusic(currentTheme);
        }

        private void Resolution_DropDownClosed(object sender, EventArgs e)
        {

        }
        private void stopMusic(string theme)
        {
            try
            {
                SoundPlayer player = new SoundPlayer();
                player.SoundLocation = "music/" + theme + "/BackgroundMusic.wav";
                player.Stop();
            }
            catch (Exception e) { }

        }
        private void startMusic(string theme)
        {
            try
            {
                SoundPlayer player = new SoundPlayer();
                player.SoundLocation = "music/" + theme + "/BackgroundMusic.wav";
                player.PlayLooping();
            }
            catch (Exception e) { }

        }
    }
}
