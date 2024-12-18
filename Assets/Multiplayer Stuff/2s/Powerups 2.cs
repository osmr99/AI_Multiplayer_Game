using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups2 : MonoBehaviour
{
    [SerializeField] PlayerMovement2 player;
    [SerializeField] GameObject playerSize;
    [SerializeField] GameObject magnetRadius;
    [SerializeField] GameObject shockwaveRadius;
    float playerspeed;

    private void Start()
    {
        playerspeed = player.speed;
    }

    private void Update()
    {
        if (player.speed < (playerspeed * 1.5f))
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(SpeedBoost());
            }
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Growth());
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(Magnet());
        }
    }

    IEnumerator SpeedBoost()
    {
        player.speed *= 1.5f;
        yield return new WaitForSecondsRealtime(10);
        player.speed = playerspeed;
    }

    IEnumerator Magnet()
    {
        magnetRadius.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        magnetRadius.SetActive(false);
    }
    IEnumerator Growth()
    {
        yield return new WaitForSecondsRealtime(1);
        playerSize.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        yield return new WaitForSecondsRealtime(1);
        playerSize.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        yield return new WaitForSecondsRealtime(1);
        playerSize.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        yield return new WaitForSecondsRealtime(1);
        playerSize.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        yield return new WaitForSecondsRealtime(1);
        playerSize.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
    }

    void Shockwave()
    {
        shockwaveRadius.SetActive(true);
        shockwaveRadius.SetActive(false);
    }


    void SplitShield()
    {

    }
}
