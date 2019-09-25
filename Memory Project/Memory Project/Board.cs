using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;

namespace Memory_Project
{
    public class Board
    {
        private int height;
        private int width;
        private List<Card> boardList = new List<Card>();
        private List<int> availableCards = new List<int>();
        private List<Tuple<int, int>> availableCoords = new List<Tuple<int, int>>();
        string currentTheme;
        string backpath;
        BoardView view;

        public Board(int height, int width, BoardView b)
        {
            this.height = height;
            this.width = width;
            view = b;

            currentTheme = (string)Application.Current.Resources["Theme"];
            backpath = "images/" + currentTheme + "/CardBack.png";
            //Console.WriteLine(backpath);
            generateImages();
            generateCoords();
            prepareCards();
            prepareBoard();
        }

        private void generateImages()
        {
            int maxCards = (Directory.GetFiles("../../images/" + currentTheme).Length) - 3;
            for(int i = 1; i <= maxCards; i++)
            {
                availableCards.Add(i);
            }
        }

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

        private void prepareCards()
        {
            int maxCards = (Directory.GetFiles("../../images/"+currentTheme).Length) - 3;
            int imgNumber = (int)Math.Floor((double)(height * width / 2));
            for(int i = 0; i < imgNumber;i++)
            {
                int img = selectImg(maxCards);
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
                int img = selectImg(maxCards);
                string frontpath = "images/" + currentTheme + "/card" + img + ".png";
                Tuple<int, int> coord = coords();
                Card temp = new Card(coord.Item1, coord.Item2, frontpath, backpath);
                boardList.Add(temp);
            }
        }

        private void prepareBoard()
        {
            view.addToGrid(height, width);
            foreach(Card c in boardList)
            {
                view.addCard(c);
            }
        }

        private int genRand(int min, int max)
        {
            Random r = new Random();
            return r.Next(min, max);
        }

        private int selectImg(int maxCards)
        {
            int index = genRand(0, (availableCards.Count-1));
            int img = availableCards[index];
            availableCards.RemoveAt(index);
            return img;
        }

        private Tuple<int, int> coords()
        {
            int index = genRand(0, (availableCoords.Count-1));
            Tuple<int, int> coord = availableCoords[index];
            availableCoords.RemoveAt(index);
            return coord;
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
        public List<Card> getBoardList()
        {
            return this.boardList;
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
