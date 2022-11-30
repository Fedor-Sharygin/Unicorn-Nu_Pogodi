using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private CupcakeManager cmgr;
    private int diffLvl = 0, prvLvl = 0;

    public void MyStart()
    {
        cmgr = GameObject.FindObjectOfType<CupcakeManager>();
        UpdateSpawners(2.5f, 6.3f,
                       .05f, .15f,
                       7.5f, 10f,
                       .7f);
    }


    private void UpdateSpawners(
        float n_minDelay, float n_maxDelay,
        float n_minVel, float n_maxVel,
        float n_minTrvl, float n_maxTrvl,
        float n_healProb
        )
    {
        cmgr.SetDelayRange(n_minDelay, n_maxDelay);
        cmgr.SetVelocityRange(n_minVel, n_maxVel);
        cmgr.SetTravelTimeRange(n_minTrvl, n_maxTrvl);
        cmgr.SetHealCupcakeProb(n_healProb);
    }

    void Update()
    {
        float curLevelTime = Time.timeSinceLevelLoad;

        prvLvl = diffLvl;
        if (prvLvl < 3 && curLevelTime >= 300f)
            ++diffLvl;
        else if (prvLvl < 2 && curLevelTime >= 150f)
            ++diffLvl;
        else if (prvLvl < 1 && curLevelTime >= 60f)
            ++diffLvl;

        if (prvLvl != diffLvl)
        {
            if (diffLvl == 1)
            {
                UpdateSpawners(1.2f, 3.4f,
                               .11f, .22f,
                               6.5f, 8f,
                               .51f);
            }
            else if (diffLvl == 2)
            {
                UpdateSpawners(.7f, 2f,
                               .19f, .4f,
                               4.2f, 6.5f,
                               .4f);
            }
            else
            {
                UpdateSpawners(.2f, 1.2f,
                               .35f, .7f,
                               2.4f, 4f,
                               .24f);
            }
        }
    }
}
