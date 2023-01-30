using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    static AudioPlayer instance = null;
    private AudioSource aSrc;
    [SerializeField] List<AudioClip> sfx;


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
            aSrc = GetComponent<AudioSource>();
        }
    }

    public void PlaySound(int sfxIdx)
    {
        aSrc.PlayOneShot(sfx[sfxIdx]);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed AudioPlayer");
    }

}
