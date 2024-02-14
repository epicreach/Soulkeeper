using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordController : MonoBehaviour
{

    DefaultPlayerInputs input;
    Animator animator;

    BoxCollider2D boxCollider;

    void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        input = new DefaultPlayerInputs();
        animator = GetComponent<Animator>();
    }

    void OnEnable() {
        input.Enable();
        input.Player.Attack.performed += OnAttackPerformed;
    }

    void OnDisable() {
        input.Disable();
        input.Player.Attack.performed -= OnAttackPerformed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit!");
        }
        boxCollider.enabled = false;
        Damagable damageable = other.gameObject.GetComponent<Damagable>();
        if (damageable != null)
        {
            damageable.Hit(20);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        boxCollider.enabled = false;
    }

    void OnTriggerExit(Collider2D other)
    {
        boxCollider.enabled = false;
    }

    void OnAttackPerformed(InputAction.CallbackContext context) {

        boxCollider.enabled = true;
        animator.Play("SwordAttack1");

    }




}
