using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallJumpingController : MonoBehaviour
{

    // Wall sliding variables
    private bool isWallSliding;
    private float wallSlidingSpeed = 0.2f;

    float direction;
    
    // Wall jumping variables
    private bool isWallJumping;
    private float wallJumpingDuration = 0.8f;
    private Vector2 wallJumpForce = new Vector2(6.0f, 3.0f);


    private Rigidbody2D rb;
    private Animator animator;
    private TouchingDirections touchingDirections;
    private DefaultPlayerInputs input;
    private PlayerController playerController;
    
    void Awake() {
        playerController = GetComponent<PlayerController>();
        input = new DefaultPlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    void OnEnable() {
        input.Enable();
        input.Player.Jump.performed += OnWallJumpPerformed;
    }

    void OnDisable() {
        input.Disable();
        input.Player.Jump.performed -= OnWallJumpPerformed;
    }

    void FixedUpdate() {
        WallSlide();

        if (isWallJumping) {
            Debug.Log("WallJumped");
            playerController.input.Player.Disable();
            rb.velocity = new Vector2(direction * wallJumpForce.x, wallJumpForce.y);
            playerController.input.Player.Enable();

        }

    }

    void WallSlide() {
        isWallSliding = touchingDirections.TouchingWall && !touchingDirections.IsGrounded && playerController.inputVector != Vector2.zero;

        if (isWallSliding) {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            animator.SetBool("IsWallSliding", true);
        }
        else {
            animator.SetBool("IsWallSliding", false);
        }
    }

    void OnWallJumpPerformed(InputAction.CallbackContext context) {
        WallJump();
    }

    void WallJump() {
        direction = -playerController.inputVector.x;

        if (isWallSliding) {
            isWallJumping = true;
            Invoke("StopWallJump", wallJumpingDuration);
        }

    }

    void StopWallJump() {
        isWallJumping = false;
    }

    



}
