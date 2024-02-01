using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour
{


    public float jumpHeight = 6f;
    public int maxJumps = 2;
    public int jumpCount = 0;

    private DefaultPlayerInputs input = null;
    private Animator animator;
    private TouchingDirections touchingDirections;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;


    private void Awake() {
        input = new DefaultPlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        input.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable() {
        input.Player.Jump.performed -= OnJumpPerformed;

    }

    private void OnJumpPerformed(InputAction.CallbackContext context) {
        Debug.Log("Jumped");
        if (jumpCount < maxJumps - 1) {
             rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
             jumpCount++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (touchingDirections.IsGrounded) { jumpCount = 0; }
    }
}
