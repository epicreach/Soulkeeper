using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPlatform : MonoBehaviour
{

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            // Make the player a child of the platform
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            // Un-parent the player from the platform
            this.transform.parent = null;
        }
    }
}



