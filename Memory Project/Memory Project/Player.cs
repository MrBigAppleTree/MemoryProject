using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Memory_Project
{
    /// <summary>
    /// The storage object for players.
    /// </summary>
    [Serializable]
    public class Player
    {
        string name;
        int score;
        List<Card> collectedCards = new List<Card>();
        [NonSerialized]
        List<Button> clickedButtons = new List<Button>();

        /// <summary>
        /// Makes an instance of the player class with a name
        /// </summary>
        /// <param name="name">The name of the player</param>
        public Player(string name)
        {
            this.name = name;
            score = 0;
            collectedCards.Clear();
        }

        //Getters for the fields
        public string getName() { return name; }
        public int getScore() { return score; }
        public List<Button> getClickedBtns() { return clickedButtons; }

        //Setters for the fields
        public void setScore(int score) { this.score = score; }
        public void setName(string name) { this.name = name; }
        public void addCards(List<Card> list) { collectedCards.AddRange(list); }

        /// <summary>
        /// Increases score of the player by the specified amount
        /// </summary>
        /// <param name="increase">The amount to increase the score by</param>
        public void increaseScore(int increase)
        {
            this.score += increase;
        }

        public void remakeButtonList()
        {
            clickedButtons = new List<Button>();
        }
    }
}
