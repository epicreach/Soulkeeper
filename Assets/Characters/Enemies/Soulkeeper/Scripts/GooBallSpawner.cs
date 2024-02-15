using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooBallSpawner : MonoBehaviour
{

    float spawnTime = 2.0f;
    float currentTime = 2.0f;

    bool spawnable;

    public GameObject Gooball;

    void Update() {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0) {
            spawnable = true;
            currentTime = spawnTime;
        }
    }

    void FixedUpdate() {
        if (spawnable) SpawnGooBall();
    }

    void SpawnGooBall() {
        Vector2 position = this.transform.position;
        position.y += 0.7f;
        GameObject obj = Instantiate(Gooball, position, Quaternion.identity);
        GameObject.Destroy(obj, 10);
        spawnable = false;

    }

}
