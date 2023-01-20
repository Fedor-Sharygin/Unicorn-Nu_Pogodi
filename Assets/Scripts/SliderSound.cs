using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderSound : MonoBehaviour, IPointerUpHandler
{

    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip snd;
    [SerializeField] private Slider slider;
    public void OnPointerUp(PointerEventData eventData)
    {
        sfx.PlayOneShot(snd);
    }


}
