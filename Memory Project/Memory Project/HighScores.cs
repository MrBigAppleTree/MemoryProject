using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Memory_Project
{
    [Serializable]
    class HighScores
    {
        [NonSerialized]
        static IFormatter serializer = new BinaryFormatter();
        [NonSerialized]
        private int width;
        [NonSerialized]
        private int height;
        [NonSerialized]
        private int score;
        [NonSerialized]
        private string name;
        [NonSerialized]
        private List<Player> players;

        //Dictionary<(string mode), Dictionary<(string playername), (int score)>>
        public Dictionary<string, Dictionary<string, int>> MainDic = new Dictionary<string, Dictionary<string, int>>();

        public HighScores(int width, int height, int score, string name, List<Player> players)
        {
            this.players = players;

            this.width = width;
            this.height = height;
            int size = this.width * this.height;
            this.score = score;
            this.name = name;

            //string mode is het aantal speler (bijvoorbeeld 3) met "player_" er achteraan, 
            //en als laatste de grootte van het speelveld (bijvoorbeel 64) 
            //dit weet hij door de height keer de width te doen (in het geval van 64 is dat dus height 8 keer width 8)
            //dit plakt hij dan allemaal achter elkaar om een string te krijgen met in dit geval 3player_64 .

            string mode = players.Count + "player_" + size;


            HighScores q;
            q = Load();
            try
            {
                //Als q.MainDic de opgegeven mode heeft, in ons geval 3player_64, dan gaat hij hier verder.
                if (q.MainDic.ContainsKey(mode))
                {
                    //Als de value van q.MainDic[mode] de naam heeft van de speler dan gaat hij verder.
                    if (q.MainDic[mode].ContainsKey(name))
                    {
                        //Hie kijkt hij of de score van de speler hoger of lager is van de al opgeslagen highscores en slaat dan de hoogste van de twee op.
                        q.MainDic[mode][name] = Math.Max(q.MainDic[mode][name], score);
                    }
                    //Als de value van q.MainDic[mode] de naam van de speler niet heeft dan gaat hij hier verder.
                    else
                    {
                        //Hier slaat hij de naam met de score op van de speler, hier wordt niet gekeken welke hoger is omdat we al weten dat er nog geen score is van deze speler.
                        q.MainDic[mode].Add(name, score);
                    }
                }
                //Als q.MainDic niet de opgegeven mode heeft, in ons geval 3player_64, dan gaat hij hier verder.
                else
                {
                    //Hier maakt hij een nieuw dictionary aan voor de mode en doet daar de spelers met hun score toevoegen.
                    Dictionary<string, int> temp = new Dictionary<string, int>();
                    temp.Add(name, score);
                    q.MainDic.Add(mode, temp);
                }
            }
            //Als het niet lukt de bovenstaande code uit te voeren doet hij hier alles opslaan.
            catch (Exception e)
            {
                Dictionary<string, int> temp = new Dictionary<string, int>();
                temp.Add(name, score);
                MainDic.Add(mode, temp);
                Save(this);
                return;
            }
            //Hier gooit hij de dictionary in een lijst.
            List<KeyValuePair<string, int>> tijdelijk = q.MainDic[mode].ToList();
            //Hier sorteert hij de score zodat de speler met de hoogste score bovenaan staat
            tijdelijk.Sort((x, y) => (y.Value.CompareTo(x.Value)));
            //Hier override hij de dictionary met de gesorteerde data.
            q.MainDic[mode] = tijdelijk.ToDictionary(pair => pair.Key, pair => pair.Value);

            Save(q);
        }

        //Dit is de save methode, hier slaat hij de data op in "highscores.txt"
        public static void Save(HighScores load)
        {
            Stream stream = new FileStream("../../highscores.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, load);
            stream.Close();
        }

        //Dit is de load methode, hier laadt hij de data vannuit "highscores.txt"
        public static HighScores Load()
        {
            Stream stream = new FileStream("../../highscores.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            //Als er data in staat geeft hij de inhoud daar van terug.
            try
            {
                HighScores load = (HighScores)serializer.Deserialize(stream);
                stream.Close();
                return load;
            }
            //Als er geen data is dan gaat hij hier naar toe.
            catch
            {
                stream.Close();
                return null;
            }
        }

        //Deze methode is er voor het hoofdmenu om de Highscores in te laden.
        public HighScores()
        {
            HighScores q = Load();
            MainDic = q.MainDic;
        }
    }
}