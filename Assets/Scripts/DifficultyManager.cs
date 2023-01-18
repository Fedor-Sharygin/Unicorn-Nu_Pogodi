using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DifficultyManager : MonoBehaviour
{

    [System.Serializable]
    private class DifficultyParameters
    {
        public float timeStart;
        public float spawnDelayMin, spawnDelayMax;
        public float travelVctyMin, travelVctyMax;
        public float travelTimeMin, travelTimeMax;
        public float spawnHealProb;

        public float themePitch;
    }

    private CupcakeManager cmgr;
    private int diffLvl = 0, prvLvl = 0;
    /// <int, Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>, int>>
    [SerializeField] List<DifficultyParameters> timeDifficulty;

    public void MyStart()
    {
        cmgr = GameObject.FindObjectOfType<CupcakeManager>();
        UpdateSpawners();
    }


    private void UpdateSpawners()
    {
        DifficultyParameters diffParams = timeDifficulty[prvLvl];

        cmgr.SetDelayRange      (diffParams.spawnDelayMin, diffParams.spawnDelayMax);
        cmgr.SetVelocityRange   (diffParams.travelVctyMin, diffParams.travelVctyMax);
        cmgr.SetTravelTimeRange (diffParams.travelTimeMin, diffParams.travelTimeMax);
        cmgr.SetHealCupcakeProb (diffParams.spawnHealProb);

        GameObject.FindObjectOfType<MusicPlayer>().SetPitch(diffParams.themePitch);
    }


    void Update()
    {
        float curLevelTime = Time.timeSinceLevelLoad;

        prvLvl = diffLvl;
        if (prvLvl < timeDifficulty.Capacity && curLevelTime >= timeDifficulty[prvLvl].timeStart)
            ++diffLvl;

        if (prvLvl != diffLvl)
            UpdateSpawners();
    }
}
