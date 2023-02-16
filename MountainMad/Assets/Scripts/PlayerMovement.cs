using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 moveAmount;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player inputs
        // move_direction = Input.GetAxis("Horizontal");

        // Move player from inputs
        rb.velocity = moveAmount;
    }
}
