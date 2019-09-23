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
        private List<Player> players;

        private string theme = (string)Application.Current.Resources["Theme"];

        private Board board;
        private BoardView view;


        public GameController(int height, int width, List<Player> players)
        {
            this.height = height;
            this.width = width;
            this.players = players;

            view = new BoardView(this);
            board = new Board(height, width, view);
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
