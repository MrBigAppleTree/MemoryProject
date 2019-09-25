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
        private string frontImgPath;
        private string frontTemp;
        private string backImgPath;
        private int cardClickCount;
        private int turnCount;
        private int playerTurnCount;
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
            view.loadPlayers(players);
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

        public Tuple<int, int> turnHandler()
        {
            while (!gameFin())
            {
                int maxPlayers = players.Count;


                if (cardClickCount == 1)
                {
                    playerTurnCount += 1;
                    if (playerTurnCount == (maxPlayers - 1))
                    {
                        playerTurnCount = 0;
                        turnCount += 1;
                    }
                    cardClickCount = 2;
                    return Tuple.Create(cardClickCount, playerTurnCount);
                }
                else
                {
                    cardClickCount = 1;
                    return Tuple.Create(cardClickCount, playerTurnCount);
                }
            }
        }
        private bool gameFin()
        {
            return board.getBoardList().Count <= 1;
        }



    }
}
