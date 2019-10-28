using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Controls;

namespace Memory_Project
{
    /// <summary>
    /// Backend logic for the game board.
    /// </summary>
    [Serializable]
    public class Board
    {
        private int height;
        private int width;
        private List<Card> boardList = new List<Card>();
        [NonSerialized]
        private List<int> availableCards = new List<int>();
        [NonSerialized]
        private List<Tuple<int, int>> availableCoords = new List<Tuple<int, int>>();
        string currentTheme;
        string backpath;
        [NonSerialized]
        BoardView view;

        /// <summary>
        /// Creates an instance of board and randomly generates an amount of cards based op height and width parameters.
        /// </summary>
        /// <param name="height">Height of the board measured in cards</param>
        /// <param name="width">Witdh of the board measured in cards</param>
        /// <param name="b">The boardview instance upon which the game will be played</param>
        public Board(int height, int width, BoardView b, string theme)
        {
            this.height = height;
            this.width = width;
            view = b;

            currentTheme = theme;
            backpath = "images/" + currentTheme + "/CardBack.png";
            //Console.WriteLine(backpath);
            generateImages();
            generateCoords();
            prepareCards();
            prepareBoard();
        }

        /// <summary>
        /// Randomly grabs the necessary amount of card faces needed for the game.
        /// </summary>
        private void generateImages()
        {
            int maxCards = (Directory.GetFiles("../../images/" + currentTheme).Length) - 3;
            Console.WriteLine("Max:" + maxCards);
            for(int i = 1; i <= maxCards; i++)
            {
                availableCards.Add(i);
            }
        }

        /// <summary>
        /// Generates a list of all possible coordinates and shuffles them.
        /// </summary>
        private void generateCoords()
        {
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    availableCoords.Add(new Tuple<int, int>(i, j));
                }
            }
            for (int i=0; i < 5; i++){
                availableCoords.listShuffle();
            }
            
        }

        /// <summary>
        /// Creates the cards from the selected images and coordinates and puts them in a list.
        /// </summary>
        private void prepareCards()
        {
            int maxCards = (Directory.GetFiles("../../images/"+currentTheme).Length) - 3;
            int imgNumber = (int)Math.Floor((double)(height * width / 2));
            for(int i = 0; i < imgNumber;i++)
            {
                int img = selectImg();
                string frontpath = "images/" + currentTheme + "/Card" + img + ".png";
                //Console.WriteLine("imgpath: " + frontpath);
                for (int j=0; j < 2; j++)
                {
                    Tuple<int, int> coord = coords();
                    Card temp = new Card(coord.Item1, coord.Item2, frontpath, backpath);
                    boardList.Add(temp);
                }
            }
            if ((height * width) % 2 != 0)
            {
                int img = selectImg();
                string frontpath = "images/" + currentTheme + "/card" + img + ".png";
                Tuple<int, int> coord = coords();
                Card temp = new Card(coord.Item1, coord.Item2, frontpath, backpath);
                boardList.Add(temp);
            }
        }

        /// <summary>
        /// Adds all cards to the boardview
        /// </summary>
        public void prepareBoard()
        {
            view.addToGrid(height, width);
            foreach(Card c in boardList)
            {
                view.addCard(c);
            }
        }

        /// <summary>
        /// Randomly generates a number between the min and max
        /// </summary>
        /// <param name="min">the minimum number to be generated</param>
        /// <param name="max">the maximum number to be generated</param>
        /// <returns>A random integer</returns>
        private int genRand(int min, int max)
        {
            Random r = new Random();
            return r.Next(min, max);
        }

        /// <summary>
        /// Randomly selects a cardface based on its index in the list of available cards
        /// </summary>
        /// <returns>The integer number of the card</returns>
        private int selectImg()
        {
            int index = genRand(0, (availableCards.Count-1));
            int img = availableCards[index];
            availableCards.RemoveAt(index);
            return img;
        }

        /// <summary>
        /// Randomly selects a coordinate from the list of available coordinates
        /// </summary>
        /// <returns>A random coordinate in tuple form</returns>
        private Tuple<int, int> coords()
        {
            int index = genRand(0, (availableCoords.Count-1));
            Tuple<int, int> coord = availableCoords[index];
            availableCoords.RemoveAt(index);
            return coord;
        }

        /// <summary>
        /// Selects a cards front image path based on the given coordinates
        /// </summary>
        /// <param name="x">The x coordinate of the card on the board</param>
        /// <param name="y">The y coordinate of the card on the board</param>
        /// <returns>The image path of the front image of the card</returns>
        public string getFrontImg(int x, int y)
        {
            foreach(Card c in boardList)
            {
                if(c.locationCheck(x, y))
                {
                    return c.getFrontImg();
                }
            }
            return null;
        }

        /// <summary>
        /// Selects a card based on the given coordinates
        /// </summary>
        /// <param name="x">The x coordinate of the card on the board</param>
        /// <param name="y">The y coordinate of the card on the board</param>
        /// <returns>The card at the input location</returns>
        public Card locationToCard(int x, int y)
        {
            foreach (Card c in boardList)
            {
                if (c.locationCheck(x, y))
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// Return the boardlist of all cards
        /// </summary>
        /// <returns>Return the boardlist of all cards</returns>
        public List<Card> getBoardList()
        {
            return this.boardList;
        }

        public void setView(BoardView b)
        {
            this.view = b;
        }

    }

    public static class Randomization
    {
        public static void listShuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider();
            int i = list.Count;
            while (i > 1)
            {
                byte[] box = new byte[1];
                do
                {
                    rnd.GetBytes(box);
                } while (!(box[0] < i * (Byte.MaxValue / i)));
                var k = (box[0] % i);
                i--;
                var value = list[k];
                list[k] = list[i];
                list[i] = value;
            }
        }
    }
}
