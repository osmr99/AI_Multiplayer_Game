using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Powerups : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    float playerspeed;

    private void Start()
    {
        playerspeed = player.speed;
    }

    private void Update()
    {
        if (player.speed < (playerspeed * 1.5f))
        {
            //if (Input.GetKeyDown(KeyCode.P))
            //{
                //StartCoroutine(SpeedBoost());
            //}
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
        yield return new WaitForSecondsRealtime(10);
    }

    void Shockwave()
    {

    }

    void Growth()
    {

    }

    void SplitShield()
    {

    }
}
