using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Memory_Project
{
    public class GameController
    {
        private int height;
        private int width;
        private int players;

        private string theme = (string)Application.Current.Resources["Theme"];

        private Board board;
        private BoardView view;


        public GameController(int height, int width, int players)
        {
            this.height = height;
            this.width = width;
            this.players = players;

            view = new BoardView(this);
            board = new Board(height, width, view);
        }

        public GameController(int height, int width) : this(height, width, 2)
        {
        }

        public int getHeight() { return height; }
        public int getWidth() { return width; }

        public BoardView getView() { return view; }
        public Board getBoard() { return board; }

        private void startMusic()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "music/" + theme + "/BackgroundMusic.wav";
            player.PlayLooping();
        }

        private void turnHandler()
        {

        }



    }
}
