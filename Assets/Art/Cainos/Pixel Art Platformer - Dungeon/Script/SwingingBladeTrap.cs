using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class SwingingBladeTrap : MonoBehaviour
    {
        [Header("Params")]
        public AnimationCurve bladeRotationCurve;
        public float bladeRotationMaxAngle = 50.0f;
        public float bladeRotationTime = 3.0f;

        [Header("Objects")]
        public Transform blade;

        private float timer;
        private float v;

        // Collider reference
        private Collider2D bladeCollider;

        private void Start()
        {
            // Get the Collider component from the blade GameObject
            bladeCollider = blade.GetComponent<Collider2D>();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > bladeRotationTime) timer = 0.0f;

            v = bladeRotationCurve.Evaluate(timer / bladeRotationTime);

            blade.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, v * bladeRotationMaxAngle);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // Check for collisions on the bladeCollider
            if (other == bladeCollider && other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Damagable>().Health -= 10;
                Debug.Log("Took 10 damage from blade trap!");
            }
        }
    }
}
