using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 100f;
    Vector2 moveAmount;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        // read the value for the "move" action each event call
        moveAmount = value.Get<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move player from inputs
        rb.MovePosition(rb.position + moveAmount * moveSpeed * Time.fixedDeltaTime);
    }
} 
