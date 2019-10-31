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
            string mode = players.Count + "player_" + size;


            HighScores q;
            q = Load();
            try
            {
                if (q.MainDic.ContainsKey(mode))
                {
                    if (q.MainDic[mode].ContainsKey(name))
                    {
                        q.MainDic[mode][name] = Math.Max(q.MainDic[mode][name], score);
                    }
                    else
                    {
                        q.MainDic[mode].Add(name, score);
                    }
                }
                else
                {
                    Dictionary<string, int> temp = new Dictionary<string, int>();
                    temp.Add(name, score);
                    q.MainDic.Add(mode, temp);
                }
            }
            catch (Exception e)
            {
                Dictionary<string, int> temp = new Dictionary<string, int>();
                temp.Add(name, score);
                MainDic.Add(mode, temp);
                Save(this);
                return;
            }
            List<KeyValuePair<string, int>> tijdelijk = q.MainDic[mode].ToList();
            tijdelijk.Sort((x, y) => (y.Value.CompareTo(x.Value)));
            q.MainDic[mode] = tijdelijk.ToDictionary(pair => pair.Key, pair => pair.Value);
            Save(q);
        }
        public static void Save(HighScores load)
        {
            Stream stream = new FileStream("../../highscores.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, load);
            stream.Close();
        }
        public static HighScores Load()
        {
            Stream stream = new FileStream("../../highscores.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                HighScores load = (HighScores)serializer.Deserialize(stream);
                stream.Close();
                return load;
            }
            catch
            {
                
                stream.Close();
                return null;
            }
        }
        public HighScores()
        {
            HighScores q = Load();
            MainDic = q.MainDic;
        }
    }
}