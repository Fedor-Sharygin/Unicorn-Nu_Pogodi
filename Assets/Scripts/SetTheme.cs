using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetTheme : MonoBehaviour
{

    [SerializeField] private AudioClip clip;

    private void Awake()
    {
        GameObject.FindObjectOfType<MusicPlayer>().SetTheme(clip, SceneManager.GetActiveScene().buildIndex);
    }

}
