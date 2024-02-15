using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleSpawner : MonoBehaviour
{

    int maxTentacles = 7;
    int tentacleCount = 0;

    public GameObject tentacle;

    void FixedUpdate() {

        tentacleCount = GameObject. FindGameObjectsWithTag("Tentacle").Length;

        if (tentacleCount >= maxTentacles) return;


        int x = Random.Range(0,40);
        SpawnTentacle(new Vector2(x,7.7f));

    }

    void SpawnTentacle(Vector2 position) {
        Instantiate(tentacle, position, Quaternion.identity);
    }

}
