using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulkeeperEntityController : MonoBehaviour
{

    Damagable damagable;

    void Awake() {
        damagable = GetComponent<Damagable>();
    }

    void FixedUpdate() {
        if (damagable.Health <= 0) {
            Destroy(gameObject);
        }
    }
}
