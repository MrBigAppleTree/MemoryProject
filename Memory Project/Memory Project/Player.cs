using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Memory_Project
{
    public class Player
    {
        string name;
        int score;
        List<Card> collectedCards = new List<Card>();
        List<Button> clickedButtons = new List<Button>();

        public Player(string name)
        {
            this.name = name;
            score = 0;
            collectedCards.Clear();
        }

        public string getName() { return name; }
        public int getScore() { return score; }
        public List<Button> getClickedBtns() { return clickedButtons; }

        public void setScore(int score) { this.score = score; }
        public void setName(string name) { this.name = name; }
        public void addCards(List<Card> list) { collectedCards.AddRange(list); }

        public void increaseScore(int increase)
        {
            this.score += increase;
        }
    }
}
