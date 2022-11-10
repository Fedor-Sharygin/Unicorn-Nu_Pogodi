using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketControl : MonoBehaviour
{
    private const float timeUntilIdle = 3.0f;
    private float curPosTime = 0.0f;


    [SerializeField]  private List<BoxCollider2D> m_TouchBoxes;
    [SerializeField]  private List<Transform> m_CatchPositions;
    [SerializeField]  private List<int> m_AnimationIndex;

    private CupcakeManager cupcakeManager;

    private int curScore = 0;

    private GameManager gameManager;
    private Vector2 startPos;
    private bool positionReturn = false;

    //private FaceBasket playerControl;
    private PlayerAnimation playerAnim;


    // Start is called before the first frame update
    public void MyStart()
    {
        startPos = transform.position;
        cupcakeManager = GameObject.FindObjectOfType<CupcakeManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        GameObject[] touchAreas = GameObject.FindGameObjectsWithTag("TouchArea");

        /*catchPositions = new List<Tuple<BoxCollider2D, Vector2>>(touchAreas.Length);
        for (int i = 0; i < touchAreas.Length; ++i)
        {
            catchPositions.Add(new Tuple<BoxCollider2D, Vector2>(
                touchAreas[i].GetComponent<BoxCollider2D>(),
                touchAreas[i].transform.GetChild(1).transform.position)
            );
        }
        GameObject.Find("Score").GetComponent<Text>().text = "0";*/

        //playerControl = GameObject.FindObjectOfType<FaceBasket>();
        playerAnim = GameObject.FindObjectOfType<PlayerAnimation>();
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

                for (int i = 0; i < m_TouchBoxes.Count; ++i)
                {
                    if (m_TouchBoxes[i].OverlapPoint(realPos))
                    {
                        transform.position = m_CatchPositions[i].position;
                        //playerControl.ChangeRotation(transform.position);
                        playerAnim.ChangeAnimation(m_AnimationIndex[i]);
                        break;
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

            for (int i = 0; i < m_TouchBoxes.Count; ++i)
            {
                if (m_TouchBoxes[i].OverlapPoint(realPos))
                {
                    transform.position = m_CatchPositions[i].position;
                    //playerControl.ChangeRotation(transform.position);
                    playerAnim.ChangeAnimation(m_AnimationIndex[i]);
                    break;
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
                cupcakeManager.DestroyCupcake(collision.gameObject);
                GameObject.Find("Score").GetComponent<Text>().text = curScore.ToString();
            }
        }
    }


}
