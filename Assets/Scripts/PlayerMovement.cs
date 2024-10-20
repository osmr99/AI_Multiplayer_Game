using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 1;
    Rigidbody2D rb;
    Vector2 movement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    private void OnMove(InputValue input)
    {
        movement = input.Get<Vector2>();
    }
}
