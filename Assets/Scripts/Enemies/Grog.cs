using UnityEngine;

public class Grog : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    private float attackCooldownTimer = Mathf.Infinity;

    private EnemyPatrol enemyPatrol;

    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
       attackCooldownTimer += Time.deltaTime;

        //attack if sees player
        if(seesPlayer())
        {
            Debug.Log("attacked"); 
            if(attackCooldownTimer >= attackCooldown)
            {
                // Attack
                attackCooldownTimer = 0;
                animator.SetTrigger("attack");
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !seesPlayer();
        }
    }

    private bool seesPlayer(){
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        Debug.DrawRay(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, Vector2.left * range, Color.red);
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
