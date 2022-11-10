using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelSelectGenerator : MonoBehaviour
{

    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;


    private void Start()
    {
        Sprite[] levelThumbnails = Resources.LoadAll<Sprite>("LevelThumbnails");
        FileInfo[] levelInfo = new DirectoryInfo("Assets\\Resources\\Levels").GetFiles();

        for (int i = 0; i < levelThumbnails.Length; ++i)
        {
            Sprite thumbnail = levelThumbnails[i];
            string levelName = levelInfo[i].Name;
            GameObject container = Instantiate(levelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtonContainer.transform, false);
            container.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(levelName.Substring(0, levelName.LastIndexOf('.'))));
        }
    }

}
