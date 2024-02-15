using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulkeeperEntityController : MonoBehaviour
{

    Damagable damagable;

    void Awake() {
        damagable = GetComponent<Damagable>();
    }

    void FixedUpdate() {
        if (damagable.Health <= 0) {
            Destroy(gameObject);
        }
    }

    void Teleport() {
        float currentX = transform.position.x;
        float newX = Random.Range(0,40);

        transform.position = new Vector2(newX, transform.position.y);

    }

    void OnTriggerStay2D(Collider2D other) {

        Damagable damagable = other.GetComponent<Damagable>();

        if (damagable != null) {
            damagable.Hit(20);
        }

    }

}
