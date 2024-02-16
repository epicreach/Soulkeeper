using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    
    private Rigidbody2D playerBody;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    private SpriteRenderer spriteRenderer;
    private bool stop = false;
    private bool patrol;
    private float timeSinceSeen;
    public float stopPatrolAfter = 3.0f;
    public float sightDistance = 3.0f;
    bool facingRight;
    public float patrolDistance = 3.0f;
    [SerializeField]
    AudioSource audioSrc;
    private void Start()
    {
        playerBody = FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>();
        if(playerBody == null)
        {
            Debug.Log("Player rigidbody can not be found through controller");
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        currentPoint = pointB.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        anim.SetBool("isRunning",true);
        patrol = true;
        facingRight = true;
        
    }

    private void Update()
    {
        //Debug.Log("Patrol: " + patrol);
        calculatePatrolStatus();

        if (stop)
        {
            
            rb.velocity = new Vector2(0, 0);
        }
        if (!stop)
        {
            
            calculatePatrolStatus();
            if (patrol)
            {
                patrolBetweenPoints();
            }
            else
            {
                followCharacter();
            }

        }

        correctSpriteDirection();



    }
    // Corrects the way that the sprite is watching
    private void correctSpriteDirection()
    {
        if (facingRight)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void calculatePatrolStatus()
    {
        bool playerSeen = canSeePlayer();
        // if the player has not been seen in a long time switch to patrol
        if (timeSinceSeen > stopPatrolAfter)
        {
            switchToPatrol();
        }
        //Increase the time since seen if the player is not seen
        if (!patrol && !playerSeen)
        {
            timeSinceSeen += Time.deltaTime;
        }
        //If the player is seen set it to not patrol mode and set the time since seen to 0
        if (playerSeen)
        {
            timeSinceSeen = 0;
            patrol = false;
        }
    }

    // Determines if the enemy can see the player by raycasting and analyzing the object that it hit
    private bool canSeePlayer()
    {
        
        Debug.DrawRay(rb.position, playerBody.position - rb.position, Color.white, sightDistance);
        RaycastHit2D ray = Physics2D.Raycast(rb.position,playerBody.position - rb.position,sightDistance,~LayerMask.GetMask("SkeletonEnemy"));
        
        if(ray.collider != null && ray.collider.gameObject.tag != null)
        {
            if (ray.collider.gameObject.tag == "SkeletonEnemy")
            {
                Debug.Log("Ray hit the enemy");
            }
            if (ray.collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        
        return false;
    }
    // Follows the character in the x direction.
    private void followCharacter()
    {
        if(rb.position.x - playerBody.position.x< 0)
        {
            facingRight = true;
            rb.velocity = new Vector2(speed,0);
        }
        else
        {
            facingRight = false;
            rb.velocity = new Vector2(-speed,0);
        }
    }

    private void switchToPatrol()
    {
        pointA.transform.position = new Vector3(rb.transform.position.x - patrolDistance / 2, rb.transform.position.y, rb.transform.position.z);
        pointB.transform.position = new Vector3(rb.transform.position.x + patrolDistance / 2, rb.transform.position.y, rb.transform.position.z);
        patrol = true;
        timeSinceSeen = 0;
    }
    public void setStopState(bool stopState)
    {
        if (stopState)
        {
            audioSrc.Stop();
        }
        else
        {
            audioSrc.Play();
        }
        
        stop = stopState;
    }
    public bool getStopState()
    {
        return stop;
    }

    // sets the velocity depending on where the enemy is inbetween the points
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
            facingRight = true;
            
            currentPoint = pointB.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            facingRight = false;
            
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
