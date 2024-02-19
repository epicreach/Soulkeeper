using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordController : MonoBehaviour
{

    DefaultPlayerInputs input;
    Animator animator;
    public AudioSource audioSrc;
    public AudioClip clip;
    BoxCollider2D boxCollider;
    [SerializeField]
    float swordCooldown = 0.7f;
    [SerializeField] private float attackCooldown;

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

        if (boxCollider.enabled == false) return;

        boxCollider.enabled = false;
        Damagable damageable = other.gameObject.GetComponent<Damagable>();
        
        if (damageable != null)
        {
            damageable.Hit(20);
        }


    }


    void OnAttackPerformed(InputAction.CallbackContext context) {

        boxCollider.enabled = true;
        animator.Play("SwordAttack1");
        Invoke("DisableCollisionBox", swordCooldown);
        audioSrc.PlayOneShot(clip);
    }

    
    void DisableCollisionBox() {
        boxCollider.enabled = false;
    }



}
