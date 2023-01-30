using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSingleton : MonoBehaviour
{

    static MainMenuSingleton instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            instance.ResetLevelSelect();
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void ResetLevelSelect()
    {
        Button lSelectButton = this.transform.GetChild(1).gameObject.GetComponent<Button>();
        lSelectButton.onClick.RemoveAllListeners();

        GameObject lsMenu = GameObject.FindObjectOfType<LevelSelectMenu>(true).gameObject;
        lSelectButton.onClick.AddListener(() =>
        {
            lsMenu.SetActive(true);
            gameObject.SetActive(false);
        });

        Button lsMenuBack = lsMenu.transform.GetChild(1).GetComponent<Button>();
        lsMenuBack.onClick.RemoveAllListeners();
        lsMenuBack.onClick.AddListener(() =>
        {
            lsMenu.SetActive(false);
            gameObject.SetActive(true);
        });
    }

}
