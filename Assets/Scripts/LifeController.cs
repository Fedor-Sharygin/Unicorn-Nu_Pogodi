using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{

    private Image[] lives;
    private int curLife;

    // Start is called before the first frame update
    void Start()
    {
        /// collect all of the images in the children
        /// guaranteed to be in this game/UI object
        lives = new Image[gameObject.transform.childCount];
        for (int i = 0; i < lives.Length; ++i)
            lives[i] = gameObject.transform.GetChild(i).GetComponent<Image>();

        /// this variable is used to destroy lives
        curLife = lives.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetDamaged()
    {
        if (curLife >= 0)
        {
            /// make the picture of life red and transparent
            lives[curLife].color = new Vector4(1, 0, 0, 0.23f);
            --curLife;
        }
        
        if (curLife == -1)
        {
            GameObject.FindObjectOfType<GameManager>().gameEnd = true;
        }
    }
}
