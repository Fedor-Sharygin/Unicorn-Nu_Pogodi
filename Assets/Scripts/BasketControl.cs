using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasketControl : MonoBehaviour
{
    private const float timeUntilIdle = 3.0f;
    private float curPosTime = 0.0f;


    [Serializable]
    public class CatchValue
    {
        public BoxCollider2D touchBox;
        public Transform catchPosition;
        public int animationIndex;
        public List<KeyCode> keyControl;
        public GameObject keyStrokeTxt;
        public DarkenAnimation darken;
    }
    [SerializeField] private List<CatchValue> m_CatchValues;

    private CupcakeManager cupcakeManager;

    public int curScore = 0;

    private GameManager gameManager;
    private Vector2 startPos;
    private bool positionReturn = false;

    //private FaceBasket playerControl;
    private PlayerAnimation playerAnim;

    private TMPro.TextMeshProUGUI score;

    // Start is called before the first frame update
    public void MyStart()
    {
        startPos = transform.position;
        cupcakeManager = GameObject.FindObjectOfType<CupcakeManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        GameObject[] touchAreas = GameObject.FindGameObjectsWithTag("TouchArea");

        score = GameObject.Find("Score").GetComponent<TMPro.TextMeshProUGUI>();
        score.text = "0";
        playerAnim = GameObject.FindObjectOfType<PlayerAnimation>();

        foreach (CatchValue cval in m_CatchValues)
        {
            cval.keyStrokeTxt.GetComponent<TextMeshProUGUI>().text = cval.keyControl[0].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /// stop any updates if the game has ended
        if (gameManager.gameEnd)
        {
            if (!positionReturn)
            {
                transform.position = startPos;
                positionReturn = true;
                //playerControl.SetIdle();
                playerAnim.ChangeAnimation(0);
            }
            return;
        }


        if (Time.timeScale != 0f)
        {
            if (Input.touchCount > 0)
            {
                transform.position = Input.GetTouch(0).position - new Vector2(0, 1);
            }

            if (Input.GetMouseButtonDown(0))
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var realPos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

                for (int i = 0; i < m_CatchValues.Count; ++i)
                {
                    if (m_CatchValues[i].touchBox.OverlapPoint(realPos))
                    {
                        transform.position = m_CatchValues[i].catchPosition.position;
                        //playerControl.ChangeRotation(transform.position);
                        playerAnim.ChangeAnimation(m_CatchValues[i].animationIndex);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_CatchValues.Count; ++i)
                {
                    foreach (KeyCode kc in m_CatchValues[i].keyControl)
                    {
                        if (Input.GetKeyDown(kc))
                        {
                            transform.position = m_CatchValues[i].catchPosition.position;
                            m_CatchValues[i].darken.Animate();
                            //playerControl.ChangeRotation(transform.position);
                            playerAnim.ChangeAnimation(m_CatchValues[i].animationIndex);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (gameManager.gameEnd)
        {
            if (!positionReturn)
            {
                transform.position = startPos;
                positionReturn = true;
                //playerControl.SetIdle();
                playerAnim.ChangeAnimation(0);
            }
            return;
        }


        if (Time.timeScale != 0f)
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var realPos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            for (int i = 0; i < m_CatchValues.Count; ++i)
            {
                if (m_CatchValues[i].touchBox.OverlapPoint(realPos))
                {
                    transform.position = m_CatchValues[i].catchPosition.position;
                    //playerControl.ChangeRotation(transform.position);
                    playerAnim.ChangeAnimation(m_CatchValues[i].animationIndex);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_CatchValues.Count; ++i)
            {
                foreach (KeyCode kc in m_CatchValues[i].keyControl)
                {
                    if (Input.GetKeyDown(kc))
                    {
                        transform.position = m_CatchValues[i].catchPosition.position;
                        m_CatchValues[i].darken.Animate();
                        //playerControl.ChangeRotation(transform.position);
                        playerAnim.ChangeAnimation(m_CatchValues[i].animationIndex);
                        break;
                    }
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.gameEnd)
        {
            if (!positionReturn)
            {
                transform.position = startPos;
                positionReturn = true;
                //playerControl.SetIdle();
                playerAnim.ChangeAnimation(0);
            }
            return;
        }


        if (Time.timeScale != 0f)
        {
            CupcakeMovement cm = collision.gameObject.GetComponent<CupcakeMovement>();
            if (cm != null)
            {
                curScore += cm.cupcakeScore;
                if (cm.GetHealer())
                    GameObject.FindObjectOfType<LifeController>().GetHealed();
                cupcakeManager.DestroyCupcake(collision.gameObject);
                score.text = curScore.ToString();
            }
        }
    }


}
