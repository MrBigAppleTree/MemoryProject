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
        /// <summary>
        /// Constructer for ConfigView
        /// </summary>
        public ConfigView()
        {

            InitializeComponent();
            FullscreenToggle.IsChecked = (bool)Application.Current.Resources["FullscreenToggle"];
            SoundToggle.IsChecked = (bool)Application.Current.Resources["MusicToggle"];
            currentTheme = (string)Application.Current.Resources["Theme"];
            BackgroundImg.Source = new BitmapImage(new Uri(@"../../images/" + currentTheme + "/MenuBackground.png", UriKind.Relative));
        }
        /// <summary>
        /// Navigates to previous page
        /// </summary>
        /// <param name="sender">Button that has been clicked</param>
        /// <param name="e">Event arguments</param>
        private void back_click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));

        }
        /// <summary>
        /// Toggles music muting for when checkbox is checked
        /// </summary>
        /// <param name="sender">Checkbox that has been clicked</param>
        /// <param name="e">Event arguments</param>
        private void SoundToggle_Checked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["MusicToggle"] = true;
            MusicToggle(currentTheme);
        }
        /// <summary>
        /// Toggles music muting for when checkbox is unchecked
        /// </summary>
        /// <param name="sender">Checkbox that has been clicked</param>
        /// <param name="e">Event arguments</param>
        private void SoundToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["MusicToggle"] = false;
            MusicToggle(currentTheme);
        }
        /// <summary>
        /// Toggles fullscreen for when checkbox is checked
        /// </summary>
        /// <param name="sender">Checkbox that has been clicked</param>
        /// <param name="e">Event arguments</param>
        private void FullscreenToggle_Checked(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            Application.Current.MainWindow.WindowStyle = WindowStyle.None;
            Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
            Application.Current.Resources["FullscreenToggle"] = true;
        }
        /// <summary>
        /// Toggles fullscreen for when checkbox is unchecked
        /// </summary>
        /// <param name="sender">Checkbox that has been clicked</param>
        /// <param name="e">Event arguments</param>
        private void FullscreenToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            Application.Current.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
            Application.Current.Resources["FullscreenToggle"] = false ;
        }

        //Changed to always fullscreen/same Resolution

        //private void Resolution_DropDownClosed(object sender, EventArgs e)
        //{
        //    switch (Resolution.SelectedIndex)
        //    {
        //        case 0:
        //            Application.Current.Resources["Width"] = 1280;
        //            Application.Current.Resources["Height"] = 720;
        //            break;
        //        case 1:
        //            Application.Current.Resources["Width"] = 1600;
        //            Application.Current.Resources["Height"] = 900;
        //            WindowHeight = (double)Application.Current.Resources["Height"];
        //            WindowWidth = (double)Application.Current.Resources["Width"];
        //            MessageBox.Show(Application.Current.Resources["Width"].ToString());
        //            break;
        //        case 2:
        //            Application.Current.Resources["Width"] = 1920;
        //            Application.Current.Resources["Height"] = 1080;
        //            break;

        //    }

        //}

        /// <summary>
        /// Toggles music muting
        /// </summary>
        /// <param name="theme">Current theme</param>
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
