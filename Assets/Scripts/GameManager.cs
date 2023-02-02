using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BasketControl bc;
    [SerializeField] private SpawnCupcake[] scs;
    [SerializeField] private DifficultyManager dmgr;

    private float seconds = 0;
    private int minutes = 0;
    private TMPro.TextMeshProUGUI time;


    public bool gameEnd = false;
    private bool loseTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < scs.Length; ++i)
        {
            scs[i].MyStart();
        }
        bc.MyStart();
        dmgr.MyStart();
        time = GameObject.Find("Time").GetComponent<TMPro.TextMeshProUGUI>();
        time.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd && !loseTriggered)
        {
            loseTriggered = true;
            Lose();
        }

        seconds += Time.deltaTime;
        if (seconds >= 60)
        {
            minutes++;
            seconds -= 60.0f;
        }
        time.text = minutes.ToString();
        if (time.text.Length == 1)
            time.text = "0" + time.text;
        string secs = Math.Floor(seconds).ToString();
        if (secs.Length == 1)
            secs = "0" + secs;
        time.text += ":" + secs;
    }

    void Lose()
    {
        /// the pause button in the gameplay menu prefab
        GameObject.Find("GameplayMenu").transform.GetChild(3).gameObject.SetActive(false);

        /// the death screen in the gameplay menu prefab
        GameObject.Find("GameplayMenu").transform.GetChild(5).gameObject.SetActive(true);
    }
}
