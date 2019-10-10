using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Project
{
    /// <summary>
    /// Storage class for information about the card
    /// </summary>
    [Serializable]
    public class Card
    {
        private int xPos;
        private int yPos;
        private string frontImgPath;
        private string backImgPath;

        /// <summary>
        /// Creates an instance of the card class
        /// </summary>
        /// <param name="x">X coordinate for this card</param>
        /// <param name="y">Y coordinate for this card</param>
        /// <param name="fpath">front image path of this card</param>
        /// <param name="bpath">back image path of this card</param>
        public Card(int x, int y, string fpath, string bpath)
        {
            xPos = x;
            yPos = y;
            frontImgPath = fpath;
            backImgPath = bpath;
        }

        //Getters for the fields
        public int getXPos() { return xPos; }
        public int getYPos() { return yPos; }
        public string getFrontImg() { return frontImgPath; }
        public string getBackImg() { return backImgPath; }

        /// <summary>
        /// Boolean check if this cards location corresponds to the input
        /// </summary>
        /// <param name="x">X coordinate to be checked</param>
        /// <param name="y">Y coordinate to be checked</param>
        /// <returns>Boolean if the location matches</returns>
        public bool locationCheck(int x, int y)
        {
            return (x == xPos && y == yPos);
        }
    }
}
