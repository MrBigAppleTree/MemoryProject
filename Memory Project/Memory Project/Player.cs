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
    }
}
