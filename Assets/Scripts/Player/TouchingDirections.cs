using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    public ContactFilter2D castFilter;
    public float raycastDistance = 0.2f;

    private bool isGround;

    public bool IsGrounded { get { 
        return isGround; } 
        private set {
        isGround = value;
    } }


    Rigidbody2D rb;
    CapsuleCollider2D collider;

    RaycastHit2D[] raycastHits = new RaycastHit2D[5];


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGround = collider.Cast(Vector2.down, castFilter, raycastHits, raycastDistance) > 0;
    }
}
