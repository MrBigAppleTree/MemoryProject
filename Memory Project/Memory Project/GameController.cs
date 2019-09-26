using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System.Windows.Media.Imaging;

namespace Memory_Project
{
    public class GameController
    {
        private int height;
        private int width;
        
        private List<Player> players;
        List<Button> selectedCards = new List<Button>();

        private string theme = (string)Application.Current.Resources["Theme"];

        private Board board;
        private BoardView view;


        public GameController(int height, int width, List<Player> players)
        {
            if(height < width)
            {
                this.height = width;
                this.width = height;
            } else
            {
                this.height = height;
                this.width = width;
            }
            
            this.players = players;

            view = new BoardView(this);
            board = new Board(this.height, this.width, view);
            view.loadPlayers(players);
            view.loadButtons();
        }

        public int getHeight() { return height; }
        public int getWidth() { return width; }

        public BoardView getView() { return view; }
        public Board getBoard() { return board; }

        public Card btnToCard(Button b)
        {
            int x = Grid.GetColumn(b);
            int y = Grid.GetRow(b);
            return board.locationToCard(x, y);
        }

        public void removeCard(Card c)
        {
            board.getBoardList().Remove(c);
            Console.WriteLine(board.getBoardList().Count);
        }

        public bool gameFin()
        {
            return board.getBoardList().Count <= 1;
        }
    }
}
