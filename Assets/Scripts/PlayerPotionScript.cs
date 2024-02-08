using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerPotionScript : MonoBehaviour
{
    private int amountOfPotions;
    private int maxAmountOfPotions = 3;
    public InputAction usePotion;
    // private PlayerHealthScript playerScript 
    
    // Start is called before the first frame update
    void Start()
    {
        
        usePotion.Enable();
        usePotion.performed += useHealthPotion;
        amountOfPotions = maxAmountOfPotions;
        // this.gameObject.PlayerHealthScript
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addHealthPotion()
    {
        if(amountOfPotions < maxAmountOfPotions)
        {
            amountOfPotions++;
        }
    }
    private void useHealthPotion(InputAction.CallbackContext callback) 
    {
        if(amountOfPotions > 0)
        {
            amountOfPotions--;
            //todo heal player

        }
    }
    public int getPotionAmount()
    {
        return amountOfPotions;
    }
}
