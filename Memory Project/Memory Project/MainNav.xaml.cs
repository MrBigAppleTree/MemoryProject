﻿using System;
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
    public partial class MainNav : Page
    {
        public MainNav()
        {
            InitializeComponent();
        }
        private void play_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PlayNav.xaml", UriKind.Relative));
        }

        private void theme_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Theme button has been pressed");
        }

        private void config_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Configuration button has been pressed");
        }

        private void high_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Highscores button has been pressed");
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
        }
    }
}