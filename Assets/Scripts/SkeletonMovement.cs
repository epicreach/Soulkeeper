using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    bool lookingRight;
    private SpriteRenderer spriteRenderer;
    private bool stop = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        anim = GetComponent<Animator>();
        
        currentPoint = pointB.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        anim.SetBool("isRunning",true);
        lookingRight = true;
    }

    private void Update()
    {
        if (stop)
        {
            rb.velocity = new Vector2(0,0);
        }
        if (!stop)
        {
            patrolBetweenPoints();
        }
        

       
    }
    public void setStopState(bool stopState)
    {
        stop = stopState;
    }

    private void patrolBetweenPoints()
    {
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            spriteRenderer.flipX = false;
            currentPoint = pointB.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            spriteRenderer.flipX = true;
            currentPoint = pointA.transform;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position,0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position,pointB.transform.position);
    }
}
