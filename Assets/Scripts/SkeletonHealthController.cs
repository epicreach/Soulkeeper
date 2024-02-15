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
    float timer = 1.1f;
    bool isAlive = true;
    int previousHealth = 100;
    Damagable damageable;
    [SerializeField]
    AudioSource audioSrc;
    [SerializeField]
    AudioClip hit;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SkeletonMovement = GetComponent<SkeletonMovement>();
        damageable = GetComponent<Damagable>();
    }
   
    
    // Update is called once per frame
    void Update()
    {
        if (previousHealth > damageable.Health)
        {
            audioSrc.PlayOneShot(hit);
        }
        previousHealth = damageable.Health;
        if(timer <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        if (!isAlive)
        {
            timer -= Time.deltaTime;
        }
        if(damageable.Health <= 0)
        {
            
            SkeletonMovement.setStopState(true);
            anim.SetTrigger("Death");
            isAlive = false;

        }
    }
}
