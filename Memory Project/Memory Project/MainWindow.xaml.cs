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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Creates a rectangle with the given arguments
        /// </summary>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        /// <param name="margin">Margin of the rectangle</param>
        /// <param name="bgArgb">Background colour of rectangle in ARGB order. Where A is transparency from 0 to 255</param>
        /// <param name="sgArgb">Background colour of rectangle in ARGB order. Where A is transparency from 0 to 255</param>
        /// <returns>A rectangle</returns>
        /// 
        /// Usage:
        ///             List<byte> testB = new List<byte> { 128, 0, 0, 0 };
        ///             List<byte> testBo = new List<byte> { 255, 255, 255, 255 };
        ///
        ///             Rectangle rect = MainWindow.createRectangle(100, 100, 20, testB, testBo);
        ///
        ///             Element.Children.Add(rect);
        ///             
        public static Rectangle createRectangle(int width, int height, int margin, List<byte> bgArgb, List<byte> sgArgb)
        {

            // Background RGB values
            byte bgA = bgArgb[0];
            byte bgR = bgArgb[1];
            byte bgG = bgArgb[2];
            byte bgB = bgArgb[3];

            // Border RGB values
            byte sgA = sgArgb[0];
            byte sgR = sgArgb[1];
            byte sgG = sgArgb[2];
            byte sgB = sgArgb[3];

            // Set brushes to colours
            SolidColorBrush background = new SolidColorBrush();
            background.Color = Color.FromArgb(bgA, bgR, bgG, bgB);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(sgA, sgR, sgG, sgB);

            // Set rectangle values
            Rectangle rect = new Rectangle();
            rect.Width = width;
            rect.Height = height;
            rect.Fill = background;
            rect.StrokeThickness = 2;
            rect.Stroke = brush;
            rect.Margin = new Thickness(margin);
            
            return rect;
        }

    }
}
