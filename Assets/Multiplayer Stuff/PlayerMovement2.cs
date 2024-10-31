using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    public int playerIndex;
    public float speed = 2;
    Rigidbody2D rb;
    Vector2 movement;
    DotSpawner2 spawner;
    Score score;
    //public CinemachineVirtualCamera cam;
    SpriteRenderer playerCircle;
    [SerializeField] Dotcolors playerColors;
    Color rngColor;
    int num;
    public DotCount dotCount;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawner = FindObjectOfType<DotSpawner2>();
        playerCircle = GetComponent<SpriteRenderer>();
        playerCircle.enabled = true;
        //cam.m_Lens.OrthographicSize = 5;
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
            //cam.m_Lens.OrthographicSize += 0.05f;
            dotCount.currentCount--;
            if(playerIndex == 1)
            {
                score = GameObject.Find("Player 1 UI").GetComponentInChildren<Score>();
                score.updateScoreAndSize();
            }
            if (playerIndex == 2)
            {
                score = GameObject.Find("Player 2 UI").GetComponentInChildren<Score>();
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
