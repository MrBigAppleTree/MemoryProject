using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class PlayNav : Page
    {
        private string theme;
        private int maxCards;

        IFormatter serializer = new BinaryFormatter();

        //Databinding variables for combobox items height/width
        public ObservableCollection<ComboBoxItem> cbItems1 { get; set; }
        public ComboBoxItem SelectedcbItem1 { get; set; }
        public ObservableCollection<ComboBoxItem> cbItems2 { get; set; }
        public ComboBoxItem SelectedcbItem2 { get; set; }

        //Databinding variables for Visibility property player RichTextBoxes
        public Visibility textBoxVisibility1 { get; set; }
        public Visibility textBoxVisibility2 { get; set; }
        public Visibility textBoxVisibility3 { get; set; }
        public Visibility textBoxVisibility4 { get; set; }

        public PlayNav()
        {
            InitializeComponent();
            theme = (string)Application.Current.Resources["Theme"];
            maxCards = (Directory.GetFiles("../../images/" + theme).Length) - 3;

            InitializePlayers();
            comboboxItems1();
            comboboxItems2();


            BackgroundImg.Source = new BitmapImage(new Uri(@"../../images/" + theme + "/MenuBackground.png", UriKind.Relative));

        }

        //Standard player selection is 2, so show only 2 player richtextboxes by changing visibility
        private void InitializePlayers()
        {
            textBoxVisibility1 = Visibility.Visible;
            textBoxVisibility2 = Visibility.Visible;
            textBoxVisibility3 = Visibility.Hidden;
            textBoxVisibility4 = Visibility.Hidden;
        }

        //When the (number of) Players combobox is dropped down it triggers event to match number of richtextboxes to value of Players combobox
        private void PlayerChanged(object sender, EventArgs e)
        {
            if(Players.IsLoaded) //To prevent triggering before complete page load
            {
                int numPlayers = Convert.ToInt32(Players.Text);
                List<RichTextBox> richTextBoxes = new List<RichTextBox> { Player0, Player1, Player2, Player3 };

                for (int i = 0; i < 4; i++)
                {
                    if (i < numPlayers)
                    {
                        richTextBoxes[i].Visibility = Visibility.Visible;
                    }
                    else
                    {
                        richTextBoxes[i].Visibility = Visibility.Hidden;
                    }
                   
                    
                }
            }
            else
            {
                return;
            }

        }

        private void comboboxItems1()
        {

            DataContext = this;
            cbItems1 = new ObservableCollection<ComboBoxItem>();

            // Get the comboBoxItems template and put it in the template property of the dynamically added cb items
            ControlTemplate CBITemplate;
            CBITemplate = (ControlTemplate)this.FindResource("ComboBoxItemControlTemplate1");

            for (int i = 0; i <= Math.Sqrt(maxCards * 2); i++)
            {
                if (i == 4)
                {
                    var cbItem = new ComboBoxItem { Content = i, Template = CBITemplate };
                    SelectedcbItem1 = cbItem;
                    cbItems1.Add(cbItem);
                }
                else if (i != 0 && i != 1)
                {
                    cbItems1.Add(new ComboBoxItem { Content = i, Template = CBITemplate });
                }
            }
            /*

            Generate Combobox items dependend on theme for the xaml

            */

        }

        private void comboboxItems2()
        {
            //Dictionary<string, int> maxCard = new Dictionary<string, int>();
            //maxCard.Add(theme, maxCards);

            DataContext = this;
            cbItems2 = new ObservableCollection<ComboBoxItem>();

            // Get the comboBoxItems template and put it in the template property of the dynamically added cb items
            ControlTemplate CBITemplate;
            CBITemplate = (ControlTemplate)this.FindResource("ComboBoxItemControlTemplate1");

            for (int i = 0; i <= Math.Sqrt(maxCards * 2); i++)
            {
                if (i == 4)
                {
                    var cbItem = new ComboBoxItem { Content = i, Template = CBITemplate };
                    SelectedcbItem2 = cbItem;
                    cbItems2.Add(cbItem);
                }
                else if (i != 0 && i != 1)
                {
                    cbItems2.Add(new ComboBoxItem { Content = i, Template = CBITemplate });
                }
            }
        }
        private void back_click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));

        }

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
        public void new_click(object sender, RoutedEventArgs e)
        {
            List<Player> players = new List<Player>();
            int numPlayers = Convert.ToInt32(Players.Text);
            int height = Convert.ToInt32(comboHeight.Text);
            int width = Convert.ToInt32(comboWidth.Text);
            Application.Current.Resources["cardY"] = height;
            Application.Current.Resources["cardX"] = width;
            

            for (int i = 0; i < numPlayers; i++)
            {
                RichTextBox rtb = (RichTextBox)FindName("Player" + (i));
                if(StringFromRichTextBox(rtb).Length < 16)
                {
                    players.Add(new Player(StringFromRichTextBox(rtb)));
                }
                else
                {
                    MessageBox.Show("Player " + (i+1) + " name too long");
                    return;
                }
            }

            GameController controller = new GameController(height, width, players, theme, serializer);
            this.NavigationService.Navigate(controller.getView());
        }

        public string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
          );
            return textRange.Text;


            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
        }

        //Character limit for playername input to prevent overflow on BoardView
        //Add method per Player
        private void RichTextKeyDown0(object sender, KeyEventArgs e)
        {
            TextRange tr = new TextRange(Player0.Document.ContentStart, Player0.Document.ContentEnd);
            if ((tr.Text.Length >= 13 || e.Key == Key.Enter) && e.Key != Key.Back)
            {
                e.Handled = true;
                return;
            }
        }
        
        private void RichTextKeyDown1(object sender, KeyEventArgs e)
        {
            TextRange tr = new TextRange(Player1.Document.ContentStart, Player1.Document.ContentEnd);
            if ((tr.Text.Length >= 13 || e.Key == Key.Enter) && e.Key != Key.Back)
            {
                e.Handled = true;
                return;
            }
        }
        private void RichTextKeyDown2(object sender, KeyEventArgs e)
        {
            TextRange tr = new TextRange(Player2.Document.ContentStart, Player2.Document.ContentEnd);
            if ((tr.Text.Length >= 13 || e.Key == Key.Enter) && e.Key != Key.Back)
            {
                e.Handled = true;
                return;
            }
        }
        private void RichTextKeyDown3(object sender, KeyEventArgs e)
        {
            TextRange tr = new TextRange(Player3.Document.ContentStart, Player3.Document.ContentEnd);
            if ((tr.Text.Length >= 13 || e.Key == Key.Enter) && e.Key != Key.Back)
            {
                e.Handled = true;
                return;
            }
        }

        /// <summary>
        /// Handles the loading of a stored game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void load_Click(object sender, RoutedEventArgs e)
        {
            Stream stream = new FileStream("../../Save/Save.sav", FileMode.Open, FileAccess.Read, FileShare.Read);
            GameController controller = (GameController)serializer.Deserialize(stream);
            controller.setSerializer(serializer);
            stream.Close();
            controller.createBoardView();
            this.NavigationService.Navigate(controller.getView());
        }
    }

}
