using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackController : MonoBehaviour
{
    Rigidbody2D rb;
    bool attackActive = false;
    private float attackDistance = 1f;
    private Rigidbody2D playerBody;
    private float timeUntilDamage = 0.2f;
    Animator anim;
    private float timeSinceLastAttack = 0f;
    private float timeBetweenAttacks = 3f;
    // Can be used to change time until damage in the unity editor
    private float TimeForDamage;
    // If player is close enough attack, wait a few milliseconds and check again, if player is in range deal damage.
    //TODO make it so that the enemy can not attack when it is in hit mode.
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerBody = FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>();
        if (playerBody == null)
        {
            Debug.Log("Player rigidbody can not be found through controller");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack -= Time.deltaTime;
        bool playerInRange = isPlayerInRange();
        if (playerInRange && !attackActive && timeSinceLastAttack <= 0)
        {
            timeSinceLastAttack = timeBetweenAttacks;
            attackActive = true;
            // perform attack animation
            if (playerBody.velocity.x == 0)
            {
                anim.SetTrigger("idleAttack");
                
            }
            else
            {
                anim.SetTrigger("movingAttack");
            }
        } 
        if (attackActive)
        {
            timeUntilDamage -= Time.deltaTime;
            if(timeUntilDamage <= 0)
            {
                Debug.Log("Possible damage");
                if (playerInRange)
                {
                    Debug.Log("Enemy dealt damage");
                    //todo deal damage to player
                }
                timeUntilDamage = 0.2f;
                attackActive = false;

            }
        }
    }
    
    private bool isPlayerInRange()
    {

        
        RaycastHit2D ray = Physics2D.Raycast(rb.position, rb.velocity, attackDistance, ~LayerMask.GetMask("SkeletonEnemy"));

        if (ray.collider != null && ray.collider.gameObject.tag != null)
        {
            if (ray.collider.gameObject.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }

}
