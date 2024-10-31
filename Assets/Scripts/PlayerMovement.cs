using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;

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
    [SerializeField] Dotcolors myColor;
    Color rngColor;
    int num;
    int spawnX;
    int spawnY;
    public DotCount dotCount;
    ScoreScriptable scoreAmount;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawner = FindObjectOfType<DotSpawner>();
        playerCircle = GetComponent<SpriteRenderer>();
        playerCircle.enabled = true;
        num = Random.Range(0, playerColors.colors.Count);
        rngColor = playerColors.colors[num];
        rngColor.a = 1;
        myColor.colors[0] = rngColor;
        playerCircle.color = myColor.colors[0];
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
        else
        {
            if(collision.transform.localScale.x < this.transform.localScale.x)
            {
                transform.localScale += collision.transform.localScale;
                StartCoroutine(playerDeath(collision.gameObject));
            }
        }
    }

    public void AddAgainToQueue(GameObject e)
    {
        spawner.dotPool.Enqueue(e);
        e.SetActive(false);
    }

    void resetMyUI()
    {
        if (playerIndex == 1)
        {
            score = GameObject.Find("Player 1 UI").GetComponentInChildren<Score>();
            score.ResetUI();
        }
        if (playerIndex == 2)
        {
            score = GameObject.Find("Player 2 UI").GetComponentInChildren<Score>();
            score.ResetUI();
        }
        if (playerIndex == 3)
        {
            score = GameObject.Find("Player 3 UI").GetComponentInChildren<Score>();
            score.ResetUI();
        }
        if (playerIndex == 4)
        {
            score = GameObject.Find("Player 4 UI").GetComponentInChildren<Score>();
            score.ResetUI();
        }
    }

    IEnumerator playerDeath(GameObject player)
    {
        player.SetActive(false);
        yield return new WaitForSecondsRealtime(5);
        player.SetActive(true);
        player.transform.localScale = new Vector2(1, 1);
        this.scoreAmount.currentScore = 0;
        resetMyUI();
        spawnX = Random.Range(-23, 23);
        spawnY = Random.Range(-14, 14);
        player.transform.position = new Vector2(spawnX, spawnY);
    }
}
