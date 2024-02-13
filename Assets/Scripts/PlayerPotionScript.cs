using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerPotionScript : MonoBehaviour
{
    private int amountOfPotions;
    private int maxAmountOfPotions = 3;
    public InputAction usePotion;
    Damagable damageable;
    
    
    // Start is called before the first frame update
    void Start() { 
    
        damageable = GetComponent<Damagable>();
        
        usePotion.Enable();
        usePotion.performed += useHealthPotion;
        amountOfPotions = maxAmountOfPotions;
        
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
            Debug.Log(damageable);
            if(damageable != null)
            {
                damageable.Health = damageable.Health + 20;
            }

        }
    }
    public int getPotionAmount()
    {
        return amountOfPotions;
    }
}
