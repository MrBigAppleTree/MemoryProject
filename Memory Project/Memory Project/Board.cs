using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Memory_Project
{
    class Board
    {
        private int height;
        private int width;
        private List<Card> boardList = new List<Card>();
        private List<int> takenCards = new List<int>();
        private List<Tuple<int, int>> takenCoords = new List<Tuple<int, int>>();
        string currentTheme;
        string backpath;
        BoardView board;

        public Board(int height, int width, BoardView b)
        {
            this.height = height;
            this.width = width;
            board = b;

            currentTheme = (string)Application.Current.Resources["Theme"];
            backpath = "images/" + currentTheme + "CardBack";
            prepareCards();
            prepareBoard();
        }

        private void prepareCards()
        {
            int maxCards = (Directory.GetFiles("../../images/"+currentTheme).Length) - 3;
            int imgNumber = (int)Math.Floor((double)(height * width / 2));
            for(int i=0; i < imgNumber;i++)
            {
                int img = selectImg(maxCards);
                string frontpath = "images/" + currentTheme + "/card" + img;
                Console.WriteLine("imgpath: " + frontpath);
                for (int j=0; j < 2; j++)
                {
                    Tuple<int, int> coord = coords();
                    Card temp = new Card(coord.Item1, coord.Item2, frontpath, backpath);
                    boardList.Add(temp);
                }
            }
            if ((height * width) % 2 != 0)
            {
                int img = selectImg(maxCards);
                string frontpath = "images/" + currentTheme + "/card" + img;
                Tuple<int, int> coord = coords();
                Card temp = new Card(coord.Item1, coord.Item2, frontpath, backpath);
                boardList.Add(temp);
            }
        }

        private void prepareBoard()
        {
            board.addToGrid(height, width);
            foreach(Card c in boardList)
            {
                board.addCard(c);
            }
        }

        private int genRand(int min, int max)
        {
            Random r = new Random();
            return r.Next(min, max);
        }

        private int selectImg(int maxCards)
        {
            while (true)
            {
                int cardnum = genRand(1, maxCards);
                if (takenCards.Contains(cardnum))
                {
                    continue;
                } else
                {
                    takenCards.Add(cardnum);
                    return cardnum;
                }
            }
        }

        private Tuple<int, int> coords()
        {
            while (true)
            {
                int x = genRand(0, width);
                int y = genRand(0, height);
                Tuple<int,int> coord = new Tuple<int,int>(x, y);
                if (takenCoords.Contains(coord)){
                    continue;
                } else
                {
                    takenCoords.Add(coord);
                    return coord;
                }
            }
        }

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
        
    }
}
