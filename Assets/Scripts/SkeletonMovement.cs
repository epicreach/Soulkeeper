using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("IsRunning",true);
    }

    private void Update()
    {
//        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }
}
