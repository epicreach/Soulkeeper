using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulkeeperEntityHandler : MonoBehaviour
{


    public GameObject soulKeeperEntity;

    void Awake() {
        Instantiate(soulKeeperEntity, new Vector2(24,7.85f), Quaternion.identity);
    }

}
