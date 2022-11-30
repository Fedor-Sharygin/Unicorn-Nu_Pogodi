using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// This class prefers to get an even number of points on the path >= 4
/// 
/// You CAN simulate straight path using this class by duplicating the first and second points
/// BUT it will have the effect of increasing the speed of the object right after the first and before the last point
public class Route : MonoBehaviour
{

    [SerializeField]
    private Transform[] routePoints;
    private Vector3 pathPoint;

    /// Route Points used for path interpolation require to contain 4 points
    /// to create a smooth cubic path
    /// to simulate such behavior with less amount of points (first and last points as well)
    /// they are duplicated for the effect
    /// 
    /// IT IS BAD PRACTICE TO ONLY GIVE ONE POINT ON A ROUTE
    /// ADD AT LEAST TWO POINTS TO THE ROUTE
    /// 
    /// Preferrable add 2k+2 points on path (k >= 1)
    public Transform[] GetRoutePoints()
    {
        return routePoints;
    }

    private void DrawPath(int i)
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            Vector3 firstPoint, lastPoint;
            if (i == 0)
                firstPoint = routePoints[0].position;
            else
                firstPoint = (routePoints[i].position + routePoints[i + 1].position) / 2;

            if (i >= routePoints.Length - 4)
                lastPoint = routePoints[routePoints.Length - 1].position;
            else
                lastPoint = (routePoints[i + 2].position + routePoints[i + 3].position) / 2;

            /// Bezier cubic curve formula
            pathPoint = Mathf.Pow(1 - t, 3) * firstPoint +
                3 * Mathf.Pow(1 - t, 2) * t * routePoints[i + 1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * routePoints[i + 2].position +
                Mathf.Pow(t, 3) * lastPoint;

            Gizmos.DrawWireSphere(pathPoint, .1f);
        }
        Gizmos.DrawLine(routePoints[i].position, routePoints[i + 1].position);
        Gizmos.DrawLine(routePoints[i + 1].position, routePoints[i + 2].position);
        Gizmos.DrawLine(routePoints[i + 2].position, routePoints[i + 3].position);

        Gizmos.DrawWireSphere(routePoints[i + 1].position, .3f);
        Gizmos.DrawWireSphere(routePoints[i + 2].position, .3f);
        Gizmos.DrawWireSphere(routePoints[i + 3].position, .3f);
    }

    private void OnDrawGizmos()
    {
        //GetRoutePoints();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(routePoints[0].position, .3f);
        for (int i = 0; i < routePoints.Length-2; i += 2)
        {
            DrawPath(i);
        }
    }

}
