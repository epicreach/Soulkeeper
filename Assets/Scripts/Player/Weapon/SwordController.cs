using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class SwordController : MonoBehaviour
{

    DefaultPlayerInputs input;
    Animator animator;

    float swordCooldown = 0.5f;

    BoxCollider2D boxCollider;

    private float attackCooldownTimer = Mathf.Infinity;
    [SerializeField] private float attackCooldown;

    void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        input = new DefaultPlayerInputs();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
       attackCooldownTimer += Time.deltaTime;
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

        if (boxCollider.enabled == false) return;

        boxCollider.enabled = false;
        Damagable damageable = other.gameObject.GetComponent<Damagable>();
        
        if (damageable != null)
        {
            damageable.Hit(20);
        }


    }


    void OnAttackPerformed(InputAction.CallbackContext context) {

        if(attackCooldownTimer > attackCooldown)
        {
        boxCollider.enabled = true;
        animator.Play("SwordAttack1");
        boxCollider.enabled = false;
        attackCooldownTimer = 0;    
        }        Invoke("DisableCollisionBox", swordCooldown);

    }

    void DisableCollisionBox() {
        boxCollider.enabled = false;
    }

 
}
