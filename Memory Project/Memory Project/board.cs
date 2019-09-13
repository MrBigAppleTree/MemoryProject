using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Project
{
    class board
    {
        private int[,] b;
        public board(int height, int width)
        {
            b = new int[height, width];
        }

        public int[,] getBoard()
        {
            return b;
        }



    }
}
