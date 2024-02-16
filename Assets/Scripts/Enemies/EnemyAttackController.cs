using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    bool attackActive = false;
    [SerializeField] private int damage;
    [SerializeField] private float attackDistance ;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float timeUntilDamage;
    [SerializeField] private LayerMask playerLayer;
    Animator anim;
    private float timeSinceLastAttack = 0f;
    [SerializeField] private float timeBetweenAttacks;
    // Can be used to change time until damage in the unity editor
    private float TimeForDamage;
    // If player is close enough attack, wait a few milliseconds and check again, if player is in range deal damage.
    //TODO make it so that the enemy can not attack when it is in hit mode.

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void attack()
{
    RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackDistance * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackDistance, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        Debug.DrawRay(boxCollider.bounds.center + transform.right * attackDistance * transform.localScale.x * colliderDistance, Vector2.left * attackDistance, Color.green);
    if (hit.collider != null)
    {
        Damagable other = hit.collider.gameObject.GetComponent<Damagable>();
        if (other != null)
        {
            other.Hit(damage); 
            Debug.Log("hit for " + damage + " damage");
        }
    }
}

private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackDistance * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * attackDistance, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
