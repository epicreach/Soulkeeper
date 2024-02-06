using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[DefaultExecutionOrder(-1)] // Set the execution order to -1 for MovementController
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{

    private Animator animator;

    public float walkSpeed = 5f;
    public float dashForce = 2f;
    public float dashDuration = 0.4f;
    private bool isDashing = false;
    public DefaultPlayerInputs input = null;

    public SpriteRenderer spriteRenderer;

    public Vector2 inputVector = Vector2.zero;

    Rigidbody2D rb;

    private void Awake() {
        input = new DefaultPlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;
        input.Player.Slide.performed += OnDashPerformed;
    }

    private void OnDisable() {
        input.Disable();
        input.Player.Move.performed -= OnDashPerformed;
        input.Player.Slide.canceled -= OnDashPerformed;
    }


    void FixedUpdate() {

        if (isDashing) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            return;
        }

         rb.velocity = new Vector2(inputVector.x * walkSpeed, rb.velocity.y);
        
    }

    private void OnMovementPerformed(InputAction.CallbackContext context) {
        inputVector = context.ReadValue<Vector2>();
        animator.SetBool("IsRunning", true);

        if (inputVector.x < 0) {
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.flipX = false;
        }

    }

    private void OnMovementCancelled(InputAction.CallbackContext context) {
        inputVector = Vector2.zero;
        animator.SetBool("IsRunning", false);
    }

    private void OnDashPerformed(InputAction.CallbackContext context) {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        isDashing = true;
        animator.SetBool("IsDashing", true);
        float playerDirection = spriteRenderer.flipX ? -1f : 1f;
        rb.velocity = new Vector2(playerDirection * (walkSpeed * dashForce), 0);
        Debug.Log(rb.velocity);
        yield return new WaitForSeconds(dashDuration);
        animator.SetBool("IsDashing", false);
        isDashing = false;

    }
}
