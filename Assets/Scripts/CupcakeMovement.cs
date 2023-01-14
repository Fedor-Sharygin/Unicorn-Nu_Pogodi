using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeMovement : MonoBehaviour
{
    public int cupcakeScore = 10;

    private bool isRoute;
    private Transform[] route;
    private Vector2 startPos;
    private Vector2 endPos;

    private float timeToDescend { get; set; }
    private float elapsedTime = 0.0f;
    
    private float descentSpeed { get; set; }
    private int routeElement;
    private float tParam;


    private bool move = false;


    [SerializeField] private List<Sprite> cupcakeSprites;

    private bool healer = false;

    private AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        int randIdx = Random.Range(0, cupcakeSprites.Count);
        GetComponent<SpriteRenderer>().sprite = cupcakeSprites[randIdx];
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (!isRoute)
            {
                /// as soon as we reach the end of the platform
                /// let the cupcake fall freely
                if (elapsedTime >= timeToDescend)
                {
                    gameObject.GetComponent<Collider2D>().isTrigger = false;
                    Rigidbody2D mBody = GetComponent<Rigidbody2D>();
                    mBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    mBody.velocity = (endPos - startPos).normalized;
                    //mBody.velocity = Vector2.zero;
                    move = false;
                }
                else
                /// otherwise slowly advance the cupcake to the end position
                {
                    transform.position = Vector2.MoveTowards(transform.position, endPos, ((endPos - startPos).magnitude / timeToDescend) * Time.deltaTime);
                    elapsedTime += Time.deltaTime;
                }
            }
            else
            {
                tParam += Time.deltaTime * descentSpeed;
                if (tParam < 1f)
                {
                    Vector3 firstPoint, lastPoint;
                    if (routeElement == 0)
                        firstPoint = route[0].position;
                    else
                        firstPoint = (route[routeElement].position + route[routeElement + 1].position) / 2;

                    if (routeElement == route.Length - 4)
                        lastPoint = route[route.Length - 1].position;
                    else
                        lastPoint = (route[routeElement + 2].position + route[routeElement + 3].position) / 2;

                    /// Bezier cubic curve formula
                    Vector3 pathPoint = Mathf.Pow(1 - tParam, 3) * firstPoint +
                        3 * Mathf.Pow(1 - tParam, 2) * tParam * route[routeElement + 1].position +
                        3 * (1 - tParam) * Mathf.Pow(tParam, 2) * route[routeElement + 2].position +
                        Mathf.Pow(tParam, 3) * lastPoint;

                    transform.position = pathPoint;
                }
                else
                {
                    if (routeElement == route.Length - 4)
                    {
                        gameObject.GetComponent<Collider2D>().isTrigger = false;
                        Rigidbody2D mBody = GetComponent<Rigidbody2D>();
                        mBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                        mBody.velocity = (route[route.Length - 1].position - route[route.Length - 2].position).normalized;
                        //mBody.velocity = Vector2.zero;
                        move = false;
                    }
                    else
                    {
                        routeElement += 2;
                        tParam = 0f;
                    }
                }
            }
        }
    }

    
    /// Setters used by SpawnCupcake in order to set cupcake parameters
    public void SetPath(Vector2 nStart, Vector2 nEnd)
    {
        isRoute = false;
        startPos = nStart; endPos = nEnd;
    }
    public void SetFallTime(float nTime)
    {
        timeToDescend = nTime;
    }
    public float GetFallTime()
    {
        return timeToDescend;
    }


    public void SetPath(Route nRoute)
    {
        isRoute = true;
        route = nRoute.GetRoutePoints();
        routeElement = 0;
        tParam = 0f;
    }
    public void SetPathSpeed(float nSpeed)
    {
        descentSpeed = nSpeed;
    }
    public float GetPathSpeed()
    {
        return descentSpeed;
    }

    public void SetHealer()
    {
        healer = true;
    }

    public bool GetHealer()
    {
        return healer;
    }


    public void Activate()
    {
        move = true;
    }


    private void OnDestroy()
    {
        int sfxIdx = 0;
        if (healer) sfxIdx = 1;
        if (transform.position.y <= -6) sfxIdx = 2;
    
        /// check for level reset
        if (audioPlayer != null)
            audioPlayer.PlaySound(sfxIdx);
    }

}
