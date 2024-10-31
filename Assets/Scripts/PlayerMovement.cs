using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public int playerIndex;
    public float speed = 1;
    Rigidbody2D rb;
    Vector2 movement;
    DotSpawner spawner;
    Score score;
    SpriteRenderer playerCircle;
    [SerializeField] Dotcolors playerColors;
    Color rngColor;
    int num;
    public DotCount dotCount;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawner = FindObjectOfType<DotSpawner>();
        playerCircle = GetComponent<SpriteRenderer>();
        playerCircle.enabled = true;
        num = Random.Range(0, playerColors.colors.Count);
        rngColor = playerColors.colors[num];
        rngColor.a = 1;
        playerCircle.color = rngColor;
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
            dotCount.currentCount--;
            if (playerIndex == 1)
            {
                score = GameObject.Find("Player 1 UI").GetComponentInChildren<Score>();
                score.updateScoreAndSize();
            }
            if (playerIndex == 2)
            {
                score = GameObject.Find("Player 2 UI").GetComponentInChildren<Score>();
                score.updateScoreAndSize();
            }
            if (playerIndex == 3)
            {
                score = GameObject.Find("Player 3 UI").GetComponentInChildren<Score>();
                score.updateScoreAndSize();
            }
            if (playerIndex == 4)
            {
                score = GameObject.Find("Player 4 UI").GetComponentInChildren<Score>();
                score.updateScoreAndSize();
            }
        }
    }

    public void AddAgainToQueue(GameObject e)
    {
        spawner.dotPool.Enqueue(e);
        e.SetActive(false);
    }
}
