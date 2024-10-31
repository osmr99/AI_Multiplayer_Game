using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PowerupUI : MonoBehaviour
{
    [SerializeField] TMP_Text powerup;
    [SerializeField] float startingFontSize;
    [SerializeField] float time;
    [SerializeField] int strength;
    [SerializeField] int vibrato;
    [SerializeField] int randomness;
    float startingScale;
    Vector3 scaleChange;

    enum PowerupList
    {
        SPLIT_SHIELD,
        MAGNET,
        SPEED_BOOST,
        SHOCKWAVE,
        GROWTH,
        NONE
    }

    PowerupList currentPowerup = PowerupList.NONE;

    // Start is called before the first frame update
    void Start()
    {
        powerup.fontSize = startingFontSize;
        startingScale = powerup.transform.localScale.x;
        scaleChange = new Vector3(1.075f, 1.075f, 1.075f);
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            ChangeColor(Color.green);
            ChangeText("Speed Boost");
            StartCoroutine(SpeedBoostUI());
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
        {
            ChangeColor(Color.yellow);
            ChangeText("Growth");
            StartCoroutine(GrowthUI());
        }

        if(UnityEngine.Input.GetKeyDown(KeyCode.I))
        {
            ChangeColor(Color.red);
            ChangeText("Magnet");
            StartCoroutine(MagnetUI());
        }

    }

    void RngPowerup()
    {
        currentPowerup = (PowerupList)Random.Range(0, 5);
        switch(currentPowerup)
        {
            case PowerupList.SPLIT_SHIELD:
                ChangeColor(Color.blue); ChangeText("Split Shield"); break;
            case PowerupList.MAGNET:
                ChangeColor(Color.red); ChangeText("Magnet"); break;
            case PowerupList.SPEED_BOOST:
                ChangeColor(Color.green); ChangeText("Speed Boost"); break;
            case PowerupList.SHOCKWAVE:
                ChangeColor(Color.magenta); ChangeText("Shockwave"); break;
            case PowerupList.GROWTH:
                ChangeColor(Color.yellow); ChangeText("Growth"); break;
        }
    }

    void ChangeColor(Color targetColor)
    {
        powerup.color = Color.white;
        powerup.DOColor(targetColor, time);
    }

    void ChangeText(string text)
    {
        powerup.text = "Powerup: " + text;
        powerup.transform.localScale = scaleChange;
        powerup.transform.DOScale(startingScale, time).ForceInit();
    }

    void ResetText()
    {
        powerup.text = "Powerup: None";
        powerup.DOColor(Color.white, time);
    }

    IEnumerator SpeedBoostUI()
    {
        float timeRemaining = 10.0f;
        for(int i = 0; i <= 100; i++)
        {
            powerup.text = "Powerup: Speed Boost\n(" + timeRemaining.ToString("F1") + "s)";
            timeRemaining -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        ResetText();
    }

    IEnumerator GrowthUI()
    {
        float timeRemaining = 5.0f;
        for (int i = 0; i <= 50; i++)
        {
            powerup.text = "Powerup: Growth\n(" + timeRemaining.ToString("F1") + "s)";
            timeRemaining -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        ResetText();
    }

    IEnumerator MagnetUI()
    {
        float timeRemaining = 10.0f;
        for (int i = 0; i <= 100; i++)
        {
            powerup.text = "Powerup: Magnet\n(" + timeRemaining.ToString("F1") + "s)";
            timeRemaining -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        ResetText();
    }
}
