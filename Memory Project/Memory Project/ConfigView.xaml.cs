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
        public bool checkBoxState { get; set; }
        public ConfigView()
        {
            InitializeComponent();
            currentTheme = (string)Application.Current.Resources["Theme"];
            checkBoxState = (bool)Application.Current.Resources["MusicToggle"]; //page change doesn't keep visual checked value
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
            Application.Current.Resources["MusicToggle"] = true;
            MusicToggle(currentTheme);
        }

        private void SoundToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["MusicToggle"] = false;
            MusicToggle(currentTheme);
        }


        //Changed to always fullscreen

        private void Resolution_DropDownClosed(object sender, EventArgs e)
        {
            switch (Resolution.SelectedIndex)
            {
                case 0:
                    Application.Current.Resources["Width"] = 1280;
                    Application.Current.Resources["Height"] = 720;
                    break;
                case 1:
                    Application.Current.Resources["Width"] = 1600;
                    Application.Current.Resources["Height"] = 900;
                    break;
                case 2:
                    Application.Current.Resources["Width"] = 1920;
                    Application.Current.Resources["Height"] = 1080;
                    break;

            }
            Application.Current.MainWindow.Width = 1600;
            Application.Current.MainWindow.Height = 900;
            //this.NavigationService.Refresh();
        }
        public void MusicToggle(string theme)
        {
            bool musicToggle = (bool)Application.Current.Resources["MusicToggle"];
            if (musicToggle)
            {
                try
                {
                    SoundPlayer player = new SoundPlayer();
                    player.SoundLocation = "music/" + theme + "/BackgroundMusic.wav";
                    player.Stop();
                }
                catch (Exception e) { }
            }
            else
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
}
