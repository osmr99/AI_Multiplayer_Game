using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 1;
    Rigidbody2D rb;
    Vector2 movement;
    public DotSpawner spawner;
    Score score;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Dot(Clone)")
        {
            AddAgainToQueue(collision.gameObject);
            score = FindObjectOfType<Score>();
            score.updateScore();
        }
            
    }

    public void AddAgainToQueue(GameObject e)
    {
        spawner.dotPool.Enqueue(e);
        e.SetActive(false);
    }
}
