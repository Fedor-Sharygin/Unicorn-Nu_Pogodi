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
}
