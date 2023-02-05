using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{

    private Image[] lives;
    private int curLife;

    public bool m_healMe = false;

    private bool isReact = false, isPaused = false;
    private int flashNum = 2, curNum = 0;
    private float flashTime = .06f, pauseTime = .03f, curTime = 1f;
    [SerializeField] private RawImage damageEffect;
    private Color reaction;
    private Color damage = new Color(1.0f, 0f, 0f, 80f/255f), heal = new Color(1f, 1f, 1f, 80f/255f);

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
        if (isReact)
        {
            if (curNum == flashNum)
            {
                curNum = 0;
                curTime = 1f;
                isReact = false;
                return;
            }

            if (isPaused)
            {
                if (curTime >= pauseTime)
                {
                    curTime = 0f;
                    isPaused = false;
                    damageEffect.color = reaction;
                }
                else
                {
                    curTime += Time.deltaTime;
                }
                return;
            }

            if (curTime >= flashTime)
            {
                ++curNum;
                damageEffect.color = new Vector4(0f,0f,0f,0f);
                isPaused = true;
                return;
            }

            curTime += Time.deltaTime;
        }
    }


    public void GetDamaged()
    {
        if (curLife >= 0)
        {
            /// make the picture of life red and transparent
            lives[curLife].color = new Vector4(1, 0, 0, 0.23f);
            --curLife;
            m_healMe = true;
            isReact = true;
            reaction = damage;
        }
        
        if (curLife == -1)
        {
            GameObject.FindObjectOfType<GameManager>().gameEnd = true;
        }
    }

    public void GetHealed()
    {
        if (curLife < lives.Length-1)
        {
            isReact = true;
            reaction = heal;
            ++curLife;
            /// restore the original color
            lives[curLife].color = new Vector4(1f,1f,1f,1f);
            if (curLife == lives.Length-1)
                m_healMe = false;
        }
    }
}
