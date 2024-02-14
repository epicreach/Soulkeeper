using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class HealthPotion : MonoBehaviour
{
    private PlayerPotionScript playerScript; //TODO REMOVE AND SWITCH OUT WITH PLAYER HEALTH SYSTEM
    bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPotionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            performPotionIncrease();
        }
    }
    void performPotionIncrease()
    {
        if (playerScript != null)
        {
            playerScript.addHealthPotion();
            GameObject.Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
        hit = true;

        }
        else
        {
            Debug.Log(collision.gameObject.tag);
        }
    }
}
