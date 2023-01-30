using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSingleton : MonoBehaviour
{

    static OptionsSingleton instance = null;


    [SerializeField] private AudioSource themeSource;
    [SerializeField] private Slider themeSlider;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            this.gameObject.SetActive(false);
            DontDestroyOnLoad(this.gameObject);

            themeSlider.value = PlayerPrefs.GetFloat("musicVolume", .5f);
            themeSource.volume = themeSlider.value;
            themeSlider.onValueChanged.AddListener((val) =>
            {
                themeSource.volume = themeSlider.value;
                PlayerPrefs.SetFloat("musicVolume", themeSlider.value);
            });

            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", .5f);
            sfxSource.volume = sfxSlider.value;
            sfxSlider.onValueChanged.AddListener((val) =>
            {
                sfxSource.volume = sfxSlider.value;
                PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
            });
        }
    }


}
