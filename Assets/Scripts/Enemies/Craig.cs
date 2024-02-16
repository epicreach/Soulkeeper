using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craig : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float rangeLeft;
    [SerializeField] private float rangeRight;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxColliderLeft;
    [SerializeField] private BoxCollider2D boxColliderRight;
    [SerializeField] private LayerMask playerLayer;

    private float attackCooldownTimer = Mathf.Infinity;

    private Animator animator;

    private EnemyAttackController attackController;
    
    private void Awake(){
        animator = GetComponent<Animator>();
        attackController = GetComponent<EnemyAttackController>();
    }

    private void Update()
    {
       attackCooldownTimer += Time.deltaTime;

        //attack if sees player to the left
        if(seesPlayerLeft())
        {
            Debug.Log("attacked"); 
            if(attackCooldownTimer >= attackCooldown)
            {
                // Attack left
                attackCooldownTimer = 0;
                animator.SetTrigger("attack_left");
                attackController.attack();
            }
        }

        else if(seesPlayerRight())
        {
            Debug.Log("attacked"); 
            if(attackCooldownTimer >= attackCooldown)
            {
                // Attack right
                attackCooldownTimer = 0;
                animator.SetTrigger("attack_right");
                attackController.attack();
            }
        }
    }

    private bool seesPlayerLeft(){
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxColliderLeft.bounds.center + transform.right * rangeLeft * transform.localScale.x * colliderDistance,
            new Vector3(boxColliderLeft.bounds.size.x * rangeLeft, boxColliderLeft.bounds.size.y, boxColliderLeft.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        Debug.DrawRay(boxColliderLeft.bounds.center + transform.right * rangeLeft * transform.localScale.x * colliderDistance, Vector2.left * rangeLeft, Color.red);
        return hit.collider != null;
    }

    private bool seesPlayerRight(){
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxColliderRight.bounds.center + transform.right * rangeRight * transform.localScale.x * colliderDistance,
            new Vector3(boxColliderRight.bounds.size.x * rangeRight, boxColliderRight.bounds.size.y, boxColliderRight.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        Debug.DrawRay(boxColliderRight.bounds.center + transform.right * rangeRight * transform.localScale.x * colliderDistance, Vector2.left * rangeRight, Color.red);
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxColliderLeft.bounds.center + transform.right * rangeLeft * transform.localScale.x * colliderDistance,
        new Vector3(boxColliderLeft.bounds.size.x * rangeLeft, boxColliderLeft.bounds.size.y, boxColliderLeft.bounds.size.z));
        Gizmos.DrawWireCube(boxColliderRight.bounds.center + transform.right * rangeRight * transform.localScale.x * colliderDistance,
        new Vector3(boxColliderRight.bounds.size.x * rangeRight, boxColliderRight.bounds.size.y, boxColliderRight.bounds.size.z));
    
    }

}
