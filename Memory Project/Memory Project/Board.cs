﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Project
{
    class Board
    {
        private int height;
        private int width;
        private int[,] boardarray;

        public Board(int height, int width)
        {
            this.height = height;
            this.width = width;
            boardarray = new int[height, width];
        }

        
    }
}