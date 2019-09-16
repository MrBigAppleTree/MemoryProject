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
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Page
    {
        private int height = 4;
        private int width = 4;

        Board b;

        public BoardView()
        {
            InitializeComponent();
            b = new Board(height, width);
            
        }

        public BoardView(int height, int width):this()
        {
            this.height = height;
            this.width = width;
        }
    }
}
