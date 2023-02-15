using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movement_speed;
    private Rigidbody2D rb;
    private bool facingright = true;

    private float move_direction;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player inputs
        move_direction = Input.GetAxis("Horizontal");

        // Move player from inputs
        rb.velocity = new Vector2(move_direction * movement_speed, rb.velocity.y);
    }
}
