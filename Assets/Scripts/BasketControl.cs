using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketControl : MonoBehaviour
{
    private const float timeUntilIdle = 3.0f;
    private float curPosTime = 0.0f;

    private List<Tuple<BoxCollider2D, Vector2>> catchPositions;

    private CupcakeManager cupcakeManager;

    private int curScore = 0;

    private GameManager gameManager;
    private Vector2 startPos;
    private bool positionReturn = false;

    private FaceBasket playerControl;


    // Start is called before the first frame update
    public void MyStart()
    {
        startPos = transform.position;
        cupcakeManager = GameObject.FindObjectOfType<CupcakeManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        GameObject[] touchAreas = GameObject.FindGameObjectsWithTag("TouchArea");

        catchPositions = new List<Tuple<BoxCollider2D, Vector2>>(touchAreas.Length);
        for (int i = 0; i < touchAreas.Length; ++i)
        {
            catchPositions.Add(new Tuple<BoxCollider2D, Vector2>(
                touchAreas[i].GetComponent<BoxCollider2D>(),
                touchAreas[i].transform.GetChild(1).transform.position)
            );
        }
        GameObject.Find("Score").GetComponent<Text>().text = "0";

        playerControl = GameObject.FindObjectOfType<FaceBasket>();
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
                playerControl.SetIdle();
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

                for (int i = 0; i < catchPositions.Count; ++i)
                {
                    if (catchPositions[i].Item1.OverlapPoint(realPos))
                    {
                        transform.position = catchPositions[i].Item2;
                        playerControl.ChangeRotation(transform.position);
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
                playerControl.SetIdle();
            }
            return;
        }


        if (Time.timeScale != 0f)
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var realPos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            for (int i = 0; i < catchPositions.Count; ++i)
            {
                if (catchPositions[i].Item1.bounds.Contains(realPos))
                {
                    transform.position = catchPositions[i].Item2 - new Vector2(1, 1);
                    playerControl.ChangeRotation(transform.position);
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
                playerControl.SetIdle();
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
