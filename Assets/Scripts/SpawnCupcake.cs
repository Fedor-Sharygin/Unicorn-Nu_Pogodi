using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCupcake : MonoBehaviour
{
    [SerializeField] private GameObject cupcakePrefab;

    [SerializeField] private bool isRoute;
    private Vector2 startPoint, endPoint;
    private Route route;

    public List<GameObject> cupcakesOnLine { get; } = new List<GameObject>(5); /// temporary number
    private float minTimeToFall, maxTimeToFall;
    private float minFallSpeed, maxFallSpeed;
    private float healProb;

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
        for (int i = 0; i < cupcakesOnLine.Capacity; ++i)
        {
            if (i == cupcakesOnLine.Count)
            {
                cupcakesOnLine.Add(Instantiate(cupcakePrefab, startPoint, Quaternion.identity));
                CupcakeMovement cpm = cupcakesOnLine[i].GetComponent<CupcakeMovement>();
                if (lifeController.m_healMe && Random.Range(0f, 1f) < healProb)
                {
                    /// if the cupcake is to be a healer
                    /// set it as such and activate the glow
                    /// 
                    /// SpriteGlow namespace is from SpriteGlow package found at:
                    /// https://github.com/Elringus/SpriteGlow
                    cpm.SetHealer();
                    cpm.GetComponent<SpriteGlow.SpriteGlowEffect>().enabled = true;
                }
                if (isRoute)
                {
                    cpm.SetPath(route);
                    cpm.SetPathSpeed(cupcakesOnLine.Count == 1 ?
                        Random.Range(minFallSpeed, maxFallSpeed) :
                        cupcakesOnLine[i - 1].GetComponent<CupcakeMovement>().GetPathSpeed());
                }
                else
                {
                    cpm.SetPath(startPoint, endPoint);
                    cpm.SetFallTime(cupcakesOnLine.Count == 1 ?
                        Random.Range(minTimeToFall, maxTimeToFall) :
                        cupcakesOnLine[i-1].GetComponent<CupcakeMovement>().GetFallTime());
                }
                cpm.Activate();
                return true;
            }
        }
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

    public void SetVelocityRange(float n_minVel, float n_maxVel)
    {
        minFallSpeed = n_minVel; maxFallSpeed = n_maxVel;
    }
    public void SetFallTimeRange(float n_minTime, float n_maxTime)
    {
        minTimeToFall = n_minTime; maxTimeToFall = n_maxTime;
    }

    public void SetHealCupcakeProb(float n_healProb)
    {
        healProb = n_healProb;
    }
}
