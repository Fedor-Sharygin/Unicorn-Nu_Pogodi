using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeManager : MonoBehaviour
{
    private SpawnCupcake[] cupcakeSpawners;
    private int maxCupcakeCount = 10;
    private float minDelay = 0.55f, maxDelay = 3.05f, curDelay = 0;

    //private BasketControl basket;

    // Start is called before the first frame update
    void Start()
    {
        cupcakeSpawners = GameObject.FindObjectsOfType<SpawnCupcake>();
        //basket = GameObject.FindObjectOfType<BasketControl>();
    }

    // Update is called once per frame
    void Update()
    {
        int curCupcakeCount = 0;
        for (int i = 0; i < cupcakeSpawners.Length; ++i)
        {
            curCupcakeCount += cupcakeSpawners[i].cupcakesOnLine.Count;
        }
        if (curDelay <= 0 && curCupcakeCount < maxCupcakeCount)
        {
            for (int i = 0; i < cupcakeSpawners.Length; ++i)
            {
                int k = Random.Range(0, cupcakeSpawners.Length);
                if (cupcakeSpawners[k].CreateCupcake())
                {
                    curDelay = Random.Range(minDelay, maxDelay);
                    break;
                }
            }
        }
        else if (curCupcakeCount < maxCupcakeCount)
        {
            curDelay -= Time.deltaTime;
        }
    }


    public bool DestroyCupcake(GameObject cupcake)
    {
        for (int i = 0; i < cupcakeSpawners.Length; ++i)
        {
            if (cupcakeSpawners[i].DestroyCupcake(cupcake))
                return true;
        }
        return false;
    }

    /// setters of cupcake movement
    public void SetDelayRange(float nminDelay, float nmaxDelay)
    {
        minDelay = nminDelay; maxDelay = nmaxDelay;
    }

    public void SetVelocityRange(float n_minVel, float n_maxVel)
    {
        for (int i = 0; i < cupcakeSpawners.Length; ++i)
            cupcakeSpawners[i].SetVelocityRange(n_minVel, n_maxVel);
    }

    public void SetTravelTimeRange(float n_minTime, float n_maxTime)
    {
        for (int i = 0; i < cupcakeSpawners.Length; ++i)
            cupcakeSpawners[i].SetFallTimeRange(n_minTime, n_maxTime);
    }

    public void SetHealCupcakeProb(float n_healProb)
    {
        for (int i = 0; i < cupcakeSpawners.Length; ++i)
            cupcakeSpawners[i].SetHealCupcakeProb(n_healProb);
    }
}

