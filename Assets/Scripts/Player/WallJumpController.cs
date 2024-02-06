using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpController : MonoBehaviour
{

    Rigidbody2D rb;
    private TouchingDirections touchingDirections;

    private JumpController jumpController;

    private Animator animator;

    public float wallSlideSpeed = 1.0f;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        jumpController = GetComponent<JumpController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {

        isGrounded = touchingDirections.IsGrounded;
        isTouchingWall = touchingDirections.TouchingWall;

        Debug.Log(isWallSliding);
        if (!isGrounded && isTouchingWall) {
            isWallSliding = true;
            animator.SetBool("IsWallSliding", true);
            rb.velocity = new Vector2(rb.velocity.x,0);
            jumpController.setJumpCount(1);
        }
        else {
            isWallSliding = false;
            animator.SetBool("IsWallSliding", false);

        }

    }

}
