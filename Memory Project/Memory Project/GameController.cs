using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Memory_Project
{
    class GameController
    {
        int height;
        int width;
        int players;

        string theme = (string)Application.Current.Resources["Theme"];

        Board board;
        BoardView view;


        public GameController(int height, int width, int players)
        {
            this.height = height;
            this.width = width;
            this.players = players;

            view = new BoardView();
            board = new Board(height, width, view);
        }

        public GameController(int height, int width) : this(height, width, 2)
        {
        }

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
