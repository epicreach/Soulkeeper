using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Damagable>() != null)
        {
            other.gameObject.GetComponent<Damagable>().Health -= 10;
            Debug.Log("Took 10 damage from spear trap!");
        } 
    }
}
