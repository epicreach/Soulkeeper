using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class healthBottleScript : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerPotionScript potionScript;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    public Sprite noPotion;
    [SerializeField]
    public Sprite onePotion;
    [SerializeField]
    public Sprite TwoPotion;
    [SerializeField]
    public Sprite ThreePotion;

    int amountOfHealthPotions = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ThreePotion;
        potionScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPotionScript>();
        
    }

    private void FixedUpdate()
    {

        if (potionScript != null) {  amountOfHealthPotions = potionScript.getPotionAmount(); }
       
        switch (amountOfHealthPotions)
        {
            case 0:
                spriteRenderer.sprite = noPotion; 
            break;
            case 1:
                spriteRenderer.sprite = onePotion;
            break;
            case 2:
                spriteRenderer.sprite = TwoPotion;
            break;
            case 3:
                spriteRenderer.sprite = ThreePotion;
            break;
        }
    }
}
