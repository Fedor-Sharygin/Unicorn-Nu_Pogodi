using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    private AudioSource aSrc;
    [SerializeField] List<AudioClip> sfx;


    private void Start()
    {
        aSrc = GetComponent<AudioSource>();
    }

    public void PlaySound(int sfxIdx)
    {
        aSrc.PlayOneShot(sfx[sfxIdx]);
    }

}
