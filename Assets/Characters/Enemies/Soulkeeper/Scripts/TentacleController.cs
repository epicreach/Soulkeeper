using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour
{

    Damagable damagable;

    int damage = 20;

    void Awake() {
        damagable = GetComponent<Damagable>();
    }



    void OnTriggerStay2D(Collider2D other) {

        if (other.CompareTag("Player")) {
            Damagable temp = other.GetComponent<Damagable>();
            if (temp != null) {
                temp.Hit(damage);
            }
        }
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
