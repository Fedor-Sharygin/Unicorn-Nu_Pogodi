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
        public Dictionary<string, int> highscores = new Dictionary<string, int>();
    }

    public Highscores highscores;

    static XML_HighScoreParser instance = null;
    private void Awake()
    {
        if (instance == null)
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
            }
        }
    }


    /// logic copied from: https://gamedevbeginner.com/how-to-keep-score-in-unity-with-loading-and-saving/#save_high_score
    public void SaveScores(Dictionary<string, int> nHighscores)
    {
        highscores.highscores = nHighscores;
        XmlSerializer serializer = new XmlSerializer( typeof(Highscores) );
        FileStream fstream = new FileStream(Application.persistentDataPath + "/Highscores/highscores.xml", FileMode.Create);
        serializer.Serialize(fstream, highscores);
        fstream.Close();
    }


    public Dictionary<string, int> LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/Highscores/highscores.xml"))
        {
            FileStream fstream = new FileStream(Application.persistentDataPath + "/Highscores/highscores.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer( typeof(Highscores) );
            highscores = serializer.Deserialize(fstream) as Highscores;
        }

        return highscores.highscores;
    }

}
