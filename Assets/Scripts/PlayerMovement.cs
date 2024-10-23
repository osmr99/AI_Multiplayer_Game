using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2;
    Rigidbody2D rb;
    Vector2 movement;
    public DotSpawner spawner;
    Score score;
    public CinemachineVirtualCamera cam;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam.m_Lens.OrthographicSize = 5;
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
            transform.localScale += new Vector3(0.1f, 0.1f, 0f);
            cam.m_Lens.OrthographicSize += 0.05f;
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
