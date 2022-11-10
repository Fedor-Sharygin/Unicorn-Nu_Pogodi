using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// This is used for demo player rotations
public class FaceBasket : MonoBehaviour
{

    float curXRelation = 1;

    public void ChangeRotation(Vector3 position)
    {
        float xRelation = Mathf.Sign(position.x - transform.position.x);
        float yRelation = Mathf.Sign(position.y - transform.position.y);
        float rotation = Vector3.Angle((position - transform.position).normalized, new Vector3(xRelation, 0, 0));


        transform.localScale = new Vector3(curXRelation * xRelation * transform.localScale.x,
            transform.localScale.y, transform.localScale.z);
        transform.eulerAngles = new Vector3(0, 0, xRelation * yRelation * rotation);

        curXRelation = xRelation;
    }


    public void SetIdle()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        transform.eulerAngles = new Vector3(0, 0, 0);
    }


}
