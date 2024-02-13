using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleSpawner : MonoBehaviour
{

    int maxTentacles = 4;
    int tentacleCount = 0;

    public GameObject tentacle;

    void FixedUpdate() {

        tentacleCount = GameObject. FindGameObjectsWithTag("Tentacle").Length;

        if (tentacleCount >= maxTentacles) return;

        SpawnTentacle(new Vector2(0,0));

    }

    void SpawnTentacle(Vector2 position) {
        Instantiate(tentacle, position, Quaternion.identity);
    }

}
