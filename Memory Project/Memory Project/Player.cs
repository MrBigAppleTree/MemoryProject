using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Project
{
    public class Player
    {
        string name;
        int score;
        List<Card> collectedCards = new List<Card>();

        public Player(string name)
        {
            this.name = name;
            score = 0;
            collectedCards.Clear();
        }

        public string getName() { return name; }
        public int getScore() { return score; }

        public void setScore(int score) { this.score = score; }
        public void setName(string name) { this.name = name; }
    }
}
