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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void Play_click(object sender, RoutedEventArgs e)
        {
            int height = Convert.ToInt32(comboHeight.Text);
            int width = Convert.ToInt32(comboWidth.Text);
            int amount = height*width;
            MessageBox.Show($"Play button has been pressed height {height} width {width} so you have {amount} cards total");
            this.NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));

        }

        private void Theme_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Theme button has been pressed");
        }

        private void Config_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Configuration button has been pressed");
        }

        private void High_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HighNav.xaml", UriKind.Relative));
        }

        private void Close_click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}