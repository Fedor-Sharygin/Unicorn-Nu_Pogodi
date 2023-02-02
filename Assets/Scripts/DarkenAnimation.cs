using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenAnimation : MonoBehaviour
{

    private bool animationState = false;
    private float animationTime = .12f, curTime = 0f;

    private void Update()
    {
        if (animationState)
        {
            if (curTime >= animationTime)
            {
                curTime = 0f;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            }
            else
            {
                curTime += Time.deltaTime;
            }
        }
    }

    public void Animate()
    {
        animationState = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(.4f, .4f, .4f);
    }

}
