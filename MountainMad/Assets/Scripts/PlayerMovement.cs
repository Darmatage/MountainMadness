using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float move_speed = 1f;
    public float jump_force = 1f;
    public Transform ground_point;
    public LayerMask whatIsGround;


    private bool isGrounded;
    private Rigidbody2D rb;
    Vector2 move_amount;

    private float inputX;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        inputX = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jump_force);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(ground_point.position, 0.2f, whatIsGround);
        // Move player from inputs
        rb.velocity = new Vector2(inputX * move_speed * 100 * Time.fixedDeltaTime, rb.velocity.y);

    }
} 
