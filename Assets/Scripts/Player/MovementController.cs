using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


[DefaultExecutionOrder(-1)] // Set the execution order to -1 for MovementController
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{

    private Animator animator;

    private float rollSpeed = 10.0f;
    private float rollDuration = 0.6f;

    private bool isRolling;

    public float walkSpeed = 5f;
    public float dashForce = 2f;
    public float dashDuration = 0.4f;
    private bool isDashing = false;

    private float dashCooldownTimer = Mathf.Infinity;
    [SerializeField] private float dashCooldown;

    [SerializeField]
    AudioSource audioSrc;
    public DefaultPlayerInputs input = null;

    private TouchingDirections touchingDirections;

    public SpriteRenderer spriteRenderer;

    public Vector2 inputVector = Vector2.zero;

    [SerializeField] private Damagable damagable;

    Rigidbody2D rb;

    private int health;

    private void Awake() {
        touchingDirections = GetComponent<TouchingDirections>();
        input = new DefaultPlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damagable = GetComponent<Damagable>();
        health = damagable.MaxHealth;
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;
        input.Player.Dash.performed += OnDashPerformed;
        input.Player.Roll.performed += OnRollPerformed;

    }

    private void OnDisable() {
        input.Disable();
        input.Player.Move.performed -= OnDashPerformed;
        input.Player.Dash.canceled -= OnDashPerformed;
        input.Player.Roll.performed -= OnRollPerformed;

    }


    void FixedUpdate() {

        if(damagable.Health == 0){
            SceneManager.LoadSceneAsync(4);
       }

        if (isDashing || isRolling) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            return;
        }
        dashCooldownTimer += Time.deltaTime;
        rb.velocity = new Vector2(inputVector.x * walkSpeed, rb.velocity.y);
        
    }

    private void OnMovementPerformed(InputAction.CallbackContext context) {
        inputVector = context.ReadValue<Vector2>();
        animator.SetBool("IsRunning", true);
        audioSrc.Play();
        if (inputVector.x < 0) {
            FlipPlayer(-1f);
        }
        else {
            FlipPlayer(1f);
        }

    }

    private void OnMovementCancelled(InputAction.CallbackContext context) {
        inputVector = Vector2.zero;
        animator.SetBool("IsRunning", false);
        audioSrc.Stop();
        
    }

    private void OnDashPerformed(InputAction.CallbackContext context) {
        if (dashCooldownTimer >= dashCooldown) {
            StartCoroutine(Dash());
            dashCooldownTimer = 0;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        animator.SetBool("IsDashing", true);
        float playerDirection = transform.localScale.x;
        rb.velocity = new Vector2(playerDirection * (walkSpeed * dashForce), 0);
        yield return new WaitForSeconds(dashDuration);
        animator.SetBool("IsDashing", false);
        isDashing = false;

    }

    public void FlipPlayer(float direction) {
            Vector3 localScale = transform.localScale;
            localScale.x = direction;
            transform.localScale = localScale;
    }



    void OnRollPerformed(InputAction.CallbackContext context) {
      if (touchingDirections.IsGrounded) {
        StartCoroutine(Roll());
      }
    }

    IEnumerator Roll() {
        isRolling = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        animator.SetTrigger("Rolling");
        float playerDirection = transform.localScale.x;
        rb.velocity = new Vector2(playerDirection * rollSpeed, 0);
        yield return new WaitForSeconds(rollDuration);
        GetComponent<CapsuleCollider2D>().enabled = true;

        isRolling = false;
    }

}
