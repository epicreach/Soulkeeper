using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealthController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private float health;
    private float invincibleTimer = 0f;
    public float invincibleMaxTime = 3.0f;
    private SkeletonMovement SkeletonMovement;
    private bool tookDamage = false;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SkeletonMovement = GetComponent<SkeletonMovement>();
    }
    public void takeDamage(float damage)
    {
        if (invincibleTimer <= 0)
        {
            SkeletonMovement.setStopState(true);
            getHit();
            Debug.Log("Skeleton took damage");
        } 
        
    }
    private void getHit()
    {
        if (rb.velocity.x != 0) 
        {
            anim.SetTrigger("hitWalking");
        }
        else
        {
            anim.SetTrigger("hitIdle");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }
    }
}
