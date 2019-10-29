using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace Memory_Project
{
    /// <summary>
    /// Handles the connection between the board, the boardview and the save function.
    /// </summary>
    [Serializable]
    public class GameController
    {
        private int height;
        private int width;
        private List<Player> players;
        [NonSerialized]
        private List<Button> selectedCards = new List<Button>();
        private string theme;
        private Board board;
        [NonSerialized]
        private BoardView view;
        [NonSerialized]
        private IFormatter serializer;

        private int turnCounter;

        /// <summary>
        /// Creates an instance of the GameController class
        /// </summary>
        /// <param name="height">Height of the board</param>
        /// <param name="width">Width of the board</param>
        /// <param name="players">List of all players in this game</param>
        public GameController(int height, int width, List<Player> players, string theme, IFormatter serializer)
        {
            if (height == 2 && width > 4 || height == 3 && width > 6)
            {
                this.height = width;
                this.width = height;
            }
            else
            {
                this.height = height;
                this.width = width;
            }

            this.serializer = serializer;
            this.players = players;
            this.theme = theme;
            view = new BoardView(this, theme);
            board = new Board(this.height, this.width, view, theme);
            view.loadPlayers(players);
            view.loadButtons();
        }

        //Getters for height and width
        public int getHeight() { return height; }
        public int getWidth() { return width; }

        //Getters for boardview and board
        public BoardView getView() { return view; }
        public Board getBoard() { return board; }

        /// <summary>
        /// Converts button from boardview into card from board based on coordinates.
        /// </summary>
        /// <param name="b">Button from the boardview</param>
        /// <returns>Returns the card corresponding to the button on the boardview</returns>
        public Card btnToCard(Button b)
        {
            int x = Grid.GetColumn(b);
            int y = Grid.GetRow(b);
            return board.locationToCard(x, y);
        }

        /// <summary>
        /// Removes particular card from the board
        /// </summary>
        /// <param name="c">The card to be removed from the board</param>
        public void removeCard(Card c)
        {
            board.getBoardList().Remove(c);
            Console.WriteLine(board.getBoardList().Count);
        }

        public bool gameFin()
        {
            return board.getBoardList().Count <= 1;
        }

        public void Save(int turnCounter)
        {
            this.turnCounter = turnCounter;
            Stream stream = new FileStream("../../Save/Save.sav", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, this);
            stream.Close();
        }

        public void createBoardView()
        {
            foreach(Player p in players)
            {
                p.remakeButtonList();
            }
            view = new BoardView(this, theme);
            view.turnCounter = turnCounter;
            board.setView(view);
            board.prepareBoard();
            view.loadPlayers(players);
            view.loadButtons();
        }

        public void setSerializer(IFormatter serializer)
        {
            this.serializer = serializer;
        }
    }
}
