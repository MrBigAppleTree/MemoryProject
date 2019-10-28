using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Project
{
    [Serializable]
    class AI : Player
    {

        private List<Card> knownCards = new List<Card>();
        [NonSerialized]
        Random r = new Random();

        public AI(string name) : base(name)
        {
            
        }

        public Tuple<Card, Card> determineMove(Board b)
        {
            if(knownCards.Count > 1)
            {
                foreach(Card c in knownCards)
                {
                    string img = c.getFrontImg();
                    foreach (Card d in knownCards)
                    {
                        if (c.Equals(d))
                        {
                            continue;
                        } 
                        else
                        {
                            if (d.getFrontImg().Equals(img))
                            {
                                return new Tuple<Card, Card>(c, d);
                            }
                        }
                    }
                    Console.WriteLine(c.getFrontImg());
                }
                return randomCards(b);
            }
            else
            {
                return randomCards(b);
            }





            //if(knownCards.Count == 0)
            //{
            //    return randomCards(b);
            //}
            //Card first = knownCards.First();
            //foreach(Card c in knownCards)
            //{
            //    foreach(Card d in knownCards)
            //    {
            //        if (d.Equals(first))
            //        {
            //            continue;
            //        }
            //        if (d.getFrontImg() == first.getFrontImg())
            //        {
            //            Tuple<Card, Card> cards = new Tuple<Card, Card>(c, first);
            //            return cards;
            //        }
            //    }
            //    first = c;
            //}

            //return randomCards(b);
        }

        private int rnd(int min, int max)
        {
            
            return r.Next(min, max);
        }

        private Tuple<Card, Card> randomCards(Board b)
        {
            int card1 = rnd(0, b.getBoardList().Count - 1);
            int card2 = rnd(0, b.getBoardList().Count - 1);

            //Console.WriteLine("Gen1: " + card1 + " Gen2: " + card2);
            if (card1 == card2)
            {
                return randomCards(b);
            }
            Card c1 = b.getBoardList()[card1];
            Card c2 = b.getBoardList()[card2];
            Tuple<Card, Card> cards = new Tuple<Card, Card>(c1, c2);
            return cards;

        }

        public void saveCard(Card c)
        {
            knownCards.Add(c);
        }

        public void removeCard(Card c)
        {
            knownCards.Remove(c);
        }

    }
}
