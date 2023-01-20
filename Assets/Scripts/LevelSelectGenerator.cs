using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class LevelSelectGenerator : MonoBehaviour
{

    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;


    private void Start()
    {
        Dictionary<string, int> levelHighscores = new Dictionary<string, int>();
        if (File.Exists(Application.persistentDataPath + "/Highscores/highscores.xml"))
        {
            levelHighscores = GameObject.FindObjectOfType<XML_HighScoreParser>().LoadScores();
        }

        Sprite[] levelThumbnails = Resources.LoadAll<Sprite>("LevelThumbnails");
        FileInfo[] levelInfo = new DirectoryInfo("Assets\\Resources\\Levels").GetFiles();

        for (int i = 0; i < levelThumbnails.Length; ++i)
        {
            Sprite thumbnail = levelThumbnails[i];
            string levelName = levelInfo[i].Name;
            if (!levelHighscores.ContainsKey(levelName))
                levelHighscores.Add(levelName, 0);

            GameObject container = Instantiate(levelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtonContainer.transform, false);
            container.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(levelName.Substring(0, levelName.LastIndexOf('.'))));
            container.transform.GetChild(0).GetComponent<TMP_Text>().text = levelName.Split('_')[1].Split('.')[0]
                + ": " + levelHighscores[levelName].ToString();
        }
    }

}
