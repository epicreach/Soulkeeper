using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    CapsuleCollider2D collider;

    Damagable damagable;

    float health = 50f;

    void Awake() {
        collider = GetComponent<CapsuleCollider2D>();
        damagable = GetComponent<Damagable>();
    }



    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            /// TODO DAMAGE PLAYER
            Debug.Log("Damaged Player");
        }
    }

    public void takeDamage(float damage) {
        health -= damage;
    }


    public void killTentacle() {
        Destroy(gameObject);
    }

    void FixedUpdate() {
        if (damagable.Health <= 0) {
            killTentacle();
            }
    }


}
