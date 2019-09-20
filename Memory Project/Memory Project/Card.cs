using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Project
{
    public class Card
    {
        private int xPos;
        private int yPos;
        private string frontImgPath;
        private string backImgPath;

        public Card(int x, int y, string fpath, string bpath)
        {
            xPos = x;
            yPos = y;
            frontImgPath = fpath;
            backImgPath = bpath;
        }

        public int getXPos() { return xPos; }
        public int getYPos() { return yPos; }
        public string getFrontImg() { return frontImgPath; }
        public string getbackImg() { return backImgPath; }

        public bool locationCheck(int x, int y)
        {
            return (x == xPos && y == yPos);
        }
    }
}
