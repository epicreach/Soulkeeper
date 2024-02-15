using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatform : MonoBehaviour
{
    public float leftLimit = -5f;  // Define the left limit
    public float rightLimit = 5f;  // Define the right limit
    public float speed = 2.0f;     // Speed of movement

    private int direction = 1;     // Initial direction of movement

    void Update()
    {
        // Check if the object reaches the right limit, change direction if it does
        if (transform.position.x > rightLimit)
        {
            direction = -1;
        }
        // Check if the object reaches the left limit, change direction if it does
        else if (transform.position.x < leftLimit)
        {
            direction = 1;
        }

        // Calculate the movement based on the direction and speed
        Vector2 movement = Vector2.right * direction * speed * Time.deltaTime;
        // Move the object
        transform.Translate(movement);
    }
}


