using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        //if (Input(KeyCode.Space))
            //rngPowerup();
    }

    void rngPowerup()
    {
        currentPowerup = (PowerupList)Random.Range(0, 5);
        switch(currentPowerup)
        {
            case PowerupList.SPLIT_SHIELD:
                changeColor(Color.blue); changeText("Split Shield"); break;
            case PowerupList.MAGNET:
                changeColor(Color.red); changeText("Magnet"); break;
            case PowerupList.SPEED_BOOST:
                changeColor(Color.green); changeText("Speed Boost"); break;
            case PowerupList.SHOCKWAVE:
                changeColor(Color.magenta); changeText("Shockwave"); break;
            case PowerupList.GROWTH:
                changeColor(Color.yellow); changeText("Growth"); break;
        }
    }

    void changeColor(Color targetColor)
    {
        powerup.color = Color.white;
        powerup.DOColor(targetColor, time);
    }

    void changeText(string text)
    {
        powerup.text = "Powerup: " + text;
        powerup.transform.localScale = scaleChange;
        powerup.transform.DOScale(startingScale, time).ForceInit();
    }
}
