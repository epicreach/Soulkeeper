using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{

    private Animator animator;

    public float walkSpeed = 5f;

    private DefaultPlayerInputs input = null;

    private SpriteRenderer spriteRenderer;

    private Vector2 inputVector = Vector2.zero;

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
    }

    private void OnDisable() {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
    }


    void FixedUpdate() {
        rb.velocity = new Vector2(inputVector.x * walkSpeed, rb.velocity.y);
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

}
