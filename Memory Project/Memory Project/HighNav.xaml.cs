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
    /// Interaction logic for HighNav.xaml
    /// </summary>
    public partial class HighNav : Page
    {
        public HighNav()
        {
            InitializeComponent();
        }
        private void Back_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainNav.xaml", UriKind.Relative));
        }
        private void High2_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("HighNav2.xaml", UriKind.Relative));
        }
    }
}