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
            DontDestroyOnLoad(this.gameObject);

            themeSlider.value = 1f;
            themeSource.volume = 1;
            themeSlider.onValueChanged.AddListener((val) =>
            {
                themeSource.volume = themeSlider.value;
            });

            sfxSlider.value = 1f;
            sfxSource.volume = 1;
            sfxSlider.onValueChanged.AddListener((val) =>
            {
                sfxSource.volume = sfxSlider.value;
            });
        }
    }


}
