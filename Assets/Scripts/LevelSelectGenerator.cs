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
    static private bool startGame = true;


    private void Awake()
    {
        XML_HighScoreParser scoreParser = GameObject.FindObjectOfType<XML_HighScoreParser>();
        Dictionary<string, int> levelHighscores = scoreParser.GetScores(startGame);
        startGame = false;

        Sprite[] levelThumbnails = Resources.LoadAll<Sprite>("LevelThumbnails");

        for (int i = 0; i < levelThumbnails.Length; ++i)
        {
            Sprite thumbnail = levelThumbnails[i];
            string levelName = thumbnail.name;
            if (!levelHighscores.ContainsKey(levelName))
            {
                levelHighscores.Add(levelName, 0);
                scoreParser.InputScore(levelName, 0);
            }

            GameObject container = Instantiate(levelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
            container.transform.SetParent(levelButtonContainer.transform, true);
            container.transform.localScale = new Vector3(1,1,1);
            container.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(levelName.Split('.')[0]));
            container.transform.GetChild(0).GetComponent<TMP_Text>().text = levelName.Split('_')[1].Split('.')[0]
                + ": " + levelHighscores[levelName].ToString();
        }
    }

}
