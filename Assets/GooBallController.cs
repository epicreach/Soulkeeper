using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GooBallController : MonoBehaviour
{

    GameObject player;

    Rigidbody2D rb;

    float speed = 3.0f;
    float damage = 10.0f;

        Vector2 playerPosition;
        Vector2 ballPosition;
        Vector2 travelDirection;

    void Awake() {

        player = GameObject.Find("Player");

        rb = GetComponent<Rigidbody2D>();

        playerPosition = player.transform.position;
        ballPosition = this.transform.position;
        travelDirection = new Vector2(playerPosition.x - ballPosition.x, 0);
        travelDirection.Normalize();
    }

    void FixedUpdate() {

        rb.velocity = travelDirection * speed;

    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag =="Player") {
            Debug.Log("Player Damaged");
            Destroy(gameObject);
        }

    }
    

}
