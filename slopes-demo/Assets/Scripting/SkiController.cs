using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded = false;
    public float pushStrength = 16;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D c) { isGrounded = true; }
    private void OnCollisionExit2D(Collision2D c) { isGrounded = false; }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            Vector2 xForce = new Vector2(Input.GetAxisRaw("Horizontal") * pushStrength, 0);
            rb.AddForce(xForce);
        }
    }
}
