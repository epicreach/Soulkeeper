using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{

    Animator animator;

    public float walkSpeed = 5f;
    public float jumpHeight = 6f;
    public int maxJumps = 2;
    public int jumpCount = 0;

    private DefaultPlayerInputs input = null;
    private TouchingDirections touchingDirections;

    private SpriteRenderer spriteRenderer;

    private Vector2 inputVector = Vector2.zero;

    Rigidbody2D rb;

    private void Awake() {
        input = new DefaultPlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;
        input.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable() {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Jump.performed -= OnJumpPerformed;

    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(inputVector.x * walkSpeed, rb.velocity.y);

        if (touchingDirections.IsGrounded) { jumpCount = 0; }


    }

    private void OnMovementPerformed(InputAction.CallbackContext context) {
        Debug.Log("Moved");
        inputVector = context.ReadValue<Vector2>();
        animator.SetBool("IsRunning", true);
        animator.SetBool("FacingRight", true);

        if (inputVector.x < 0) {
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.flipX = false;
        }

    }

    private void OnMovementCancelled(InputAction.CallbackContext context) {
        Debug.Log("Stopped Moving");
        inputVector = Vector2.zero;
        animator.SetBool("IsRunning", false);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context) {
        Debug.Log("Jumped");
        if (jumpCount < maxJumps - 1) {
             rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
             jumpCount++;
        }
    }

}
