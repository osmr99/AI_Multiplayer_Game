using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;
using DG.Tweening.Plugins;

public class PlayerMovement : MonoBehaviour
{
    public int playerIndex;
    public float speed = 1;
    Rigidbody2D rb;
    Vector2 movement;
    DotSpawner spawner;
    Score score;
    PowerupUI powerup;
    SpriteRenderer playerCircle;
    [SerializeField] Dotcolors playerColors;
    [SerializeField] Dotcolors myColor;
    Color rngColor;
    int num;
    int spawnX;
    int spawnY;
    public DotCount dotCount;
    [SerializeField] ScoreScriptable playerOneStats;
    [SerializeField] ScoreScriptable playerTwoStats;
    [SerializeField] ScoreScriptable playerThreeStats;
    [SerializeField] ScoreScriptable playerFourStats;
    int scoreGained;
    int sizeGained;

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
            if(collision.transform.localScale.x < transform.localScale.x)
            {
                transform.localScale += collision.transform.localScale;

                if (playerIndex == 1)
                {
                    playerOneStats.currentScore += collision.GetComponentInChildren<PlayerMovement>().returnStats()[0];
                    playerOneStats.currentSize += collision.GetComponentInChildren<PlayerMovement>().returnStats()[1];
                }
                if (playerIndex == 2)
                {
                    playerTwoStats.currentScore += collision.GetComponentInChildren<PlayerMovement>().returnStats()[0];
                    playerTwoStats.currentSize += collision.GetComponentInChildren<PlayerMovement>().returnStats()[1];
                }
                if (playerIndex == 3)
                {
                    playerThreeStats.currentScore += collision.GetComponentInChildren<PlayerMovement>().returnStats()[0];
                    playerThreeStats.currentSize += collision.GetComponentInChildren<PlayerMovement>().returnStats()[1];
                }
                if (playerIndex == 4)
                {
                    playerFourStats.currentScore += collision.GetComponentInChildren<PlayerMovement>().returnStats()[0];
                    playerFourStats.currentSize += collision.GetComponentInChildren<PlayerMovement>().returnStats()[1];
                }

                if (collision.GetComponentInChildren<PlayerMovement>().playerIndex == 1)
                {
                    score = GameObject.Find("Player 1 UI").GetComponentInChildren<Score>();
                    powerup = GameObject.Find("Player 1 UI").GetComponentInChildren<PowerupUI>();
                    powerup.allowPowerup = false;
                    playerOneStats.currentScore = 0;
                    playerOneStats.currentSize = 0;
                    score.ResetUI();
                    Debug.Log(1);
                }
                if (collision.GetComponentInChildren<PlayerMovement>().playerIndex == 2)
                {
                    score = GameObject.Find("Player 2 UI").GetComponentInChildren<Score>();
                    powerup = GameObject.Find("Player 2 UI").GetComponentInChildren<PowerupUI>();
                    powerup.allowPowerup = false;
                    playerTwoStats.currentScore = 0;
                    playerTwoStats.currentSize = 0;
                    score.ResetUI();
                    Debug.Log(2);
                }
                if (collision.GetComponentInChildren<PlayerMovement>().playerIndex == 3)
                {
                    score = GameObject.Find("Player 3 UI").GetComponentInChildren<Score>();
                    powerup = GameObject.Find("Player 3 UI").GetComponentInChildren<PowerupUI>();
                    powerup.allowPowerup = false;
                    playerThreeStats.currentScore = 0;
                    playerThreeStats.currentSize = 0;
                    score.ResetUI();
                    Debug.Log(3);
                }
                if (collision.GetComponentInChildren<PlayerMovement>().playerIndex == 4)
                {
                    score = GameObject.Find("Player 4 UI").GetComponentInChildren<Score>();
                    powerup = GameObject.Find("Player 4 UI").GetComponentInChildren<PowerupUI>();
                    powerup.allowPowerup = false;
                    playerFourStats.currentScore = 0;
                    playerFourStats.currentSize = 0;
                    score.ResetUI();
                    Debug.Log(4);
                }
                StartCoroutine(playerDeath(collision.gameObject));
            }
        }
    }

    public void AddAgainToQueue(GameObject e)
    {
        spawner.dotPool.Enqueue(e);
        e.SetActive(false);
    }

    IEnumerator playerDeath(GameObject player)
    {
        powerup.allowPowerup = false;
        player.transform.position = new Vector2(100, 100);
        yield return new WaitForSecondsRealtime(5);
        //player.SetActive(true);
        powerup.allowPowerup = true;
        player.transform.localScale = new Vector2(1, 1);
        spawnX = Random.Range(-23, 23);
        spawnY = Random.Range(-14, 14);
        player.transform.position = new Vector2(spawnX, spawnY);
    }

    public int[] returnStats()
    {
        int[] ints = new int[2];
        if (playerIndex == 1)
        {
            scoreGained = playerOneStats.currentScore;
            sizeGained = playerOneStats.currentSize;
            ints[0] = scoreGained;
            ints[1] = sizeGained;
            return ints;
        }
        if (playerIndex == 2)
        {
            scoreGained = playerTwoStats.currentScore;
            sizeGained = playerTwoStats.currentSize;
            ints[0] = scoreGained;
            ints[1] = sizeGained;
            return ints;
        }
        if (playerIndex == 3)
        {
            scoreGained = playerThreeStats.currentScore;
            sizeGained = playerThreeStats.currentSize;
            ints[0] = scoreGained;
            ints[1] = sizeGained;
            return ints;
        }
        if (playerIndex == 4)
        {
            scoreGained = playerFourStats.currentScore;
            sizeGained = playerFourStats.currentSize;
            ints[0] = scoreGained;
            ints[1] = sizeGained;
            return ints;
        }
        return null;
    }
}
