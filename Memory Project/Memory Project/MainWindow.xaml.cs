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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void play_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Play button has been pressed");
        }
        private void theme_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Theme button has been pressed");
        }
        private void config_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Configuration button has been pressed");
        }
        private void close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
