using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpingController : MonoBehaviour
{

    private bool isWallSliding;
    private float wallSlidingSpeed = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private TouchingDirections touchingDirections;
    
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    void FixedUpdate() {
        WallSlide();
    }

    void WallSlide() {
        isWallSliding = touchingDirections.TouchingWall && !touchingDirections.IsGrounded;

        if (isWallSliding) {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            animator.SetBool("IsWallSliding", true);
        }
        else {
            animator.SetBool("IsWallSliding", false);
        }
    }



}
