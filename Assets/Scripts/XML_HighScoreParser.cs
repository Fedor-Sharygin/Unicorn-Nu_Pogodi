using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class XML_HighScoreParser : MonoBehaviour
{

    [System.Serializable]
    public class Highscores
    {
        public List<string> levelNames = new List<string>();
        public List<int> levelScores = new List<int>();
    }

    public Highscores highscores;

    static XML_HighScoreParser instance = null;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            if (!Directory.Exists(Application.persistentDataPath + "/Highscores/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Highscores/");
                File.Create(Application.persistentDataPath + "/Highscores/highscores.xml");
            }
        }
    }


    public void InputScore(string lvlName, int lvlScore)
    {
        for (int i = 0; i < highscores.levelNames.Count; ++i)
        {
            if (highscores.levelNames[i] == lvlName)
            {
                highscores.levelScores[i] = Mathf.Max(highscores.levelScores[i], lvlScore);
                return;
            }
        }

        highscores.levelNames.Add(lvlName);
        highscores.levelScores.Add(lvlScore);
    }    

    /// logic copied from: https://gamedevbeginner.com/how-to-keep-score-in-unity-with-loading-and-saving/#save_high_score
    public void SaveScores()
    {
        XmlSerializer serializer = new XmlSerializer( typeof(Highscores) );
        FileStream fstream = new FileStream(Application.persistentDataPath + "/Highscores/highscores.xml", FileMode.OpenOrCreate);
        serializer.Serialize(fstream, highscores);
        fstream.Close();
    }


    public void LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/Highscores/highscores.xml"))
        {
            FileStream fstream = new FileStream(Application.persistentDataPath + "/Highscores/highscores.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer( typeof(Highscores) );
            highscores = serializer.Deserialize(fstream) as Highscores;
        }
    }

    public Dictionary<string, int> GetScores(bool loadFromFile = false)
    {
        if (loadFromFile)
            LoadScores();
        Dictionary<string, int> scoreDict = new Dictionary<string, int>();

        for (int i = 0; i < highscores.levelNames.Count; ++i)
            scoreDict.Add(highscores.levelNames[i], highscores.levelScores[i]);

        return scoreDict;
    }

}
