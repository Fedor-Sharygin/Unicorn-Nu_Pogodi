using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    static MusicPlayer instance = null;
    static int curScene = 0;

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
        }
    }

    public void SetTheme(AudioClip audioClip, int levelTag)
    {
        AudioSource asrc = gameObject.GetComponent<AudioSource>();
        if (curScene != levelTag)
        {
            curScene = levelTag;
            asrc.Stop();
            asrc.clip = audioClip;
            asrc.pitch = 1f;
            asrc.Play();
        }
    }

    public void SetPitch(float pitchLevel)
    {
        gameObject.GetComponent<AudioSource>().pitch = pitchLevel;
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed MusicPlayer");
    }


}
