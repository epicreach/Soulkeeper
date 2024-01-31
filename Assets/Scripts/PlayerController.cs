using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    private DefaultPlayerInputs input = null;

    private Vector2 inputVector = Vector2.zero;
    public float walkSpeed = 5f;
    public float jumpHeight = 6f;

    Rigidbody2D rb;

    private void Awake() {
        input = new DefaultPlayerInputs();
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void OnMovementPerformed(InputAction.CallbackContext context) {
        Debug.Log("Moved");
        inputVector = context.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext context) {
        Debug.Log("Stopped Moving");
        inputVector = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context) {
        Debug.Log("Jumped");
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

}
