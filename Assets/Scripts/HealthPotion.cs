using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private PlayerPotionScript playerScript; //TODO REMOVE AND SWITCH OUT WITH PLAYER HEALTH SYSTEM
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPotionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript != null)
        {
            playerScript.addHealthPotion();
            GameObject.Destroy(this.gameObject);
        }
    }
}
