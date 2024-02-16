using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SoulkeeperEntityController : MonoBehaviour
{

    Damagable damagable;

    BoxCollider2D collider;
    int health;

    Animator animator;

    void Awake() {
        damagable = GetComponent<Damagable>();
        health = damagable.Health;
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate() {

        if (health != damagable.Health) {
            animator.Play("SoulkeeperSink");
            health = damagable.Health;
            collider.enabled = false;
            Invoke("Teleport", 1.2f);
        }

        if (damagable.Health <= 0) {
            Destroy(gameObject);
            SceneManager.LoadSceneAsync(3);
        }
    }

    public void Teleport() {
        float currentX = 0;
        float newX = 0;

        newX = Random.Range(5,35);
        transform.position = new Vector2(newX, transform.position.y);
        collider.enabled = true;
    }

    void OnTriggerStay2D(Collider2D other) {

        Damagable damagable = other.GetComponent<Damagable>();

        if (damagable != null) {
            damagable.Hit(20);
        }

    }

}
