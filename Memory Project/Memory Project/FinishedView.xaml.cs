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

        }



    }
}
