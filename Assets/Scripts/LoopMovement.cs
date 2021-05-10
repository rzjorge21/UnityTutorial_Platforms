using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMovement : MonoBehaviour
{

    public Transform[] points;
    public float speed = 3f;
    int destPoint;
    public float allowence = 0.1f;

    [Header("For delay configuration")]
    public bool hasDelay = false;
    private bool isDelayRunning = false;
    public float delayTime;

    // Use this for initialization
    void Start()
    {
        // Set first target
        UpdateTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isDelayRunning == false) { 
            Vector3 thisPos = new Vector3(transform.position.x, transform.position.y, 0f);

            if (Vector3.Distance(thisPos, points[destPoint].position) < allowence)
            {
                UpdateTarget();
            }

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, points[destPoint].position, step);
        }
    }

    void UpdateTarget()
    {
        if (points.Length == 0)
        {
            return;
        }
        transform.position = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
        if (hasDelay)
        {
            StartCoroutine(Waiter(delayTime));
        }

    }


    IEnumerator Waiter(float seconds)
    {
        isDelayRunning = true;
        yield return new WaitForSeconds(seconds);
        isDelayRunning = false;
    }

}
