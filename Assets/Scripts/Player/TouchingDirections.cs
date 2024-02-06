using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    public ContactFilter2D castFilter;
    public Animator animator;
    public float raycastDistance = 0.2f;

    private bool isGround;

    public bool IsGrounded { get { 
        return isGround; } 
        private set {
        isGround = value;
    } }

    private bool isTouchingWall;
    public bool TouchingWall { get { return isTouchingWall; }
    private set {
        isTouchingWall = value;
    }
    }


    Rigidbody2D rb;
    CapsuleCollider2D collider;

    RaycastHit2D[] raycastHits = new RaycastHit2D[5];


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        CheckIsGrounded();
        CheckTouchingWall();
    }


    void CheckIsGrounded() {
            
        isGround = collider.Cast(Vector2.down, castFilter, raycastHits, raycastDistance) > 0;

        if (!IsGrounded) {
            animator.SetBool("IsGrounded", false);
        }
        else {
            animator.SetBool("IsGrounded", true);
        }
    }

    void CheckTouchingWall() {

        // Checks if player is touching wall to left or to right.
        isTouchingWall = collider.Cast(Vector2.right, castFilter, raycastHits, raycastDistance) > 0;
        isTouchingWall |= collider.Cast(Vector2.left, castFilter, raycastHits, raycastDistance) > 0;

    }

}
