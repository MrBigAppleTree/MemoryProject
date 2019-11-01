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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class MainNav : Page
    {
        public MainNav()
        {
            InitializeComponent();
            string currentTheme = (string)Application.Current.Resources["Theme"];
            BackgroundImg.Source = new BitmapImage(new Uri(@"../../images/" + currentTheme + "/MenuBackground.png", UriKind.Relative));

        }

        private void play_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PlayNav.xaml", UriKind.Relative));
        }

        private void theme_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ThemeNav.xaml", UriKind.Relative));
        }
        private void config_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ConfigView.xaml", UriKind.Relative));
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void Mode_Change(object sender, EventArgs e)
        {
            if (HighGrid.Children.Count > 0)
            {
                HighGrid.Children.Clear();
            }
            try
            {
                int playercount = hiscr.SelectedIndex + 1;
                int height = Height.SelectedIndex + 2;
                int width = Width.SelectedIndex + 2;
                int size = height * width;
                string loadmode = playercount + "player_" + size;
                HighScores h = new HighScores();
                List<KeyValuePair<string, int>> HighScreen = h.MainDic[loadmode].ToList();
                for (int i = 0; i < HighScreen.Count; i++)
                {
                    try
                    {
                        HighGrid.RowDefinitions.Add(new RowDefinition());
                        TextBlock Position = new TextBlock();
                        Position.TextAlignment = TextAlignment.Left;
                        Position.FontSize = 25;
                        Position.Foreground = Brushes.White;
                        Position.SetValue(Grid.RowProperty, i);
                        Position.SetValue(Grid.ColumnProperty, 0);
                        Position.Text = i + 1 + ".";
                        HighGrid.Children.Add(Position);

                        TextBlock PlayerName = new TextBlock();
                        PlayerName.TextAlignment = TextAlignment.Left;
                        PlayerName.FontSize = 25;
                        PlayerName.Foreground = Brushes.White;
                        PlayerName.SetValue(Grid.RowProperty, i);
                        PlayerName.SetValue(Grid.ColumnProperty, 1);
                        PlayerName.Text = HighScreen[i].Key;
                        HighGrid.Children.Add(PlayerName);

                        TextBlock Scores = new TextBlock();
                        Scores.TextAlignment = TextAlignment.Left;
                        Scores.FontSize = 25;
                        Scores.Foreground = Brushes.White;
                        Scores.SetValue(Grid.RowProperty, i);
                        Scores.SetValue(Grid.ColumnProperty, 2);
                        Scores.Text = Convert.ToString(HighScreen[i].Value);
                        HighGrid.Children.Add(Scores);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
    }
}