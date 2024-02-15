using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulkeeperEntityController : MonoBehaviour
{

    Damagable damagable;
    int health;

    void Awake() {
        damagable = GetComponent<Damagable>();
        health = damagable.Health;
    }

    void FixedUpdate() {

        if (health != damagable.Health) {
            Teleport();
            health = damagable.Health;
        }

        if (damagable.Health <= 0) {
            Destroy(gameObject);
        }
    }

    public void Teleport() {
        float currentX = transform.position.x;
        float newX = Random.Range(0,40);
        Debug.Log("Teleported");
        transform.position = new Vector2(newX, transform.position.y);

    }

    void OnTriggerStay2D(Collider2D other) {

        Damagable damagable = other.GetComponent<Damagable>();

        if (damagable != null) {
            damagable.Hit(20);
        }

    }

}
