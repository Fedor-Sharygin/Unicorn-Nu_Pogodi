using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuCommands : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        GameObject.FindObjectOfType<XML_HighScoreParser>().SaveScores();
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

        GameObject.FindObjectOfType<XML_HighScoreParser>().InputScore(SceneManager.GetActiveScene().name, GameObject.FindObjectOfType<BasketControl>().curScore);

        MainMenuSingleton mMenu = GameObject.FindObjectOfType<MainMenuSingleton>(true);
        mMenu.gameObject.SetActive(true);
    }

}
