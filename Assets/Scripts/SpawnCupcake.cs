using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCupcake : MonoBehaviour
{
    [SerializeField] private GameObject cupcakePrefab;

    [SerializeField] private bool isRoute;
    private Vector2 startPoint, endPoint;
    private Route route;

    public List<GameObject> cupcakesOnLine { get; } = new List<GameObject>(3); /// temporary number
    private int curCupcakeNumber = 0;
    private float spawnDelay = 2, curDelay = 0;
    private float timeToFall = 10, fallSpeed = .2f;

    private LifeController lifeController;

    // Start is called before the first frame update
    public void MyStart()
    {
        if (!isRoute)
        {
            startPoint = gameObject.transform.GetChild(0).position;
            endPoint = gameObject.transform.GetChild(1).position;
        }
        else
        {
            route = gameObject.transform.GetChild(0).GetComponent<Route>();
        }
        //for (int i = 0; i < cupcakesOnLine.Capacity; ++i)
        //    cupcakesOnLine.Add(null);

        lifeController = GameObject.FindObjectOfType<LifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (curDelay <= 0)
        {
            //CreateCupcake();
        }
        else
        {
            curDelay -= Time.deltaTime;
        }

        for (int i = 0; i < cupcakesOnLine.Count; ++i)
        {
            GameObject cup = cupcakesOnLine[i];
            if (cup.transform.position.y < -6)
            {
                lifeController.GetDamaged();
                cupcakesOnLine.RemoveAt(i);
                --i;
                Destroy(cup);
            }
        }

    }

    public bool CreateCupcake()
    {
        //if (curDelay <= 0)
        //{
            for (int i = 0; i < cupcakesOnLine.Capacity; ++i)
            {
                if (i == cupcakesOnLine.Count)
                {
                    cupcakesOnLine.Add(Instantiate(cupcakePrefab, startPoint, Quaternion.identity));
                    CupcakeMovement cpm = cupcakesOnLine[i].GetComponent<CupcakeMovement>();
                    if (isRoute)
                    {
                        cpm.SetPath(route);
                        cpm.SetPathSpeed(fallSpeed);
                    }
                    else
                    {
                        cpm.SetPath(startPoint, endPoint);
                        cpm.SetFallTime(timeToFall);
                    }
                    curDelay = spawnDelay;
                    return true;
                }
            }
        //}
        return false;
    }

    public bool DestroyCupcake(GameObject cupcake)
    {
        for (int i = cupcakesOnLine.Count - 1; i > -1; --i)
        {
            if (cupcakesOnLine[i] == cupcake)
            {
                cupcakesOnLine.RemoveAt(i);
                Destroy(cupcake);
                return true;
            }
        }
        return false;
    }

    public Vector2 GetStartPos()
    {
        return startPoint;
    }

    public Vector2 GetEndPos()
    {
        return endPoint;
    }
}
