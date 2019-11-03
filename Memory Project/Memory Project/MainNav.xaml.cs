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
    /// Interaction logic for MainNav.xaml
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

        //Wordt uit gevoerd zodra een van de dropdowns van de highscore pagina gesloten wordt.
        private void Mode_Change(object sender, EventArgs e)
        {
            //Hier wordt gekeken of er al highscores op de pagina staan, als dit het geval is dan wordt deze uigevoerd.
            if (HighGrid.Children.Count > 0)
            {
                //Hier haalt hij de highscore pagina leeg zodat er weer een nieuwe op kan staan, anders heb je namelijk meerdere highscores door elkaar staan.
                HighGrid.Children.Clear();
            }
            try
            {
                //playercount wordt gehaald uit de dropdown van het aantal spelers dat er mee speelde aan het spel, hier wordt +1 gedaan omdat hij bij 0 begint met de index.
                int playercount = hiscr.SelectedIndex + 1;
                //height wordt weg gehaald uit de eerste van de twee cijfers op de highscores pagina, hier wordt +2 gedaan omdat de index bij 0 begint en de eerste van de lijst is 2.
                int height = Height.SelectedIndex + 2;
                //width wordt weg gehaald uit de tweede van de twee cijfers op de highscores pagina, hier wordt +2 gedaan omdat de index bij 0 begint en de eerste van de lijst is 2.
                int width = Width.SelectedIndex + 2;
                //Het maakt niet uit voor de highscore page of je 3x2 of 2x3 doet want hij kijkt naar het aantal kaarten en dat is voor zowel 3x2 als 2x3 6, het spel zelf is dan ook het zelfde dus het maakt niet echt uit.


                //size is hieght*width, zo weet je dus hoeveel kaarten er op het speelveld staat
                int size = height * width;

                //loadmode is het zelfde als "mode" van highscores.cs, hier wordt het aantal spelers voor "player_" gezet met daar achter het aantal kaarten op het speelveld.
                string loadmode = playercount + "player_" + size;
                HighScores h = new HighScores();
                //Hier zet hij de highscores van Highscores in een list.
                List<KeyValuePair<string, int>> HighScreen = h.MainDic[loadmode].ToList();
                //Hier kijkt hij naar het aantal highscores in highscreen.
                for (int i = 0; i < HighScreen.Count; i++)
                {
                    try
                    {
                        //Hier maakt hij een nieuwe row aan.
                        HighGrid.RowDefinitions.Add(new RowDefinition());

                        //Hier wordt de positie van de score aangegeven met een int, de eerste is 1 en gaat met 1 omhoog voor elke speler met een score.
                        TextBlock Position = new TextBlock();
                        Position.TextAlignment = TextAlignment.Left;
                        Position.FontSize = 25;
                        Position.Foreground = Brushes.White;
                        Position.SetValue(Grid.RowProperty, i);
                        Position.SetValue(Grid.ColumnProperty, 0);
                        Position.Text = i + 1 + ".";
                        HighGrid.Children.Add(Position);

                        //Hier zet hij de naam van de speler neer naast de positie.
                        TextBlock PlayerName = new TextBlock();
                        PlayerName.TextAlignment = TextAlignment.Left;
                        PlayerName.FontSize = 25;
                        PlayerName.Foreground = Brushes.White;
                        PlayerName.SetValue(Grid.RowProperty, i);
                        PlayerName.SetValue(Grid.ColumnProperty, 1);
                        PlayerName.Text = HighScreen[i].Key;
                        HighGrid.Children.Add(PlayerName);

                        //Hier zet hij de score van de speler neer van de speler, dit zet hij naast de speler neer.
                        TextBlock Scores = new TextBlock();
                        Scores.TextAlignment = TextAlignment.Left;
                        Scores.FontSize = 25;
                        Scores.Foreground = Brushes.White;
                        Scores.SetValue(Grid.RowProperty, i);
                        Scores.SetValue(Grid.ColumnProperty, 2);
                        Scores.Text = Convert.ToString(HighScreen[i].Value);
                        HighGrid.Children.Add(Scores);
                    }
                    //Als er geen highscores zijn voor een gamemode dan staat er ook niks op de highscore pagina.
                    catch
                    {
                    }
                }
            }
            //Als er geen highscores zijn voor een gamemode dan staat er ook niks op de highscore pagina.
            catch
            {
            }
        }
    }
}