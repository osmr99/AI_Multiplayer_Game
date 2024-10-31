using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DirectorAI : MonoBehaviour
{
    public ScoreScriptable[] scores;
    TMP_Text text;
    public int[] theScores;

    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;


    public void TheStart()
    {
        StartCoroutine(TheStartTwo());
    }

    IEnumerator TheStartTwo()
    {
        yield return new WaitForSeconds(0.1f);
        text = GameObject.Find("Power Up Cooldown").GetComponentInChildren<TMP_Text>();
        player1 = GameObject.Find("Player 1 UI");
        player2 = GameObject.Find("Player 2 UI");
        player3 = GameObject.Find("Player 3 UI");
        player4 = GameObject.Find("Player 4 UI");
        StartCoroutine(powerUpGiverUI());
    }

    IEnumerator powerUpGiverUI()
    {
        float timeRemaining = 10f;
        for (int i = 0; i <= 7; i++)
        {
            text.text = "Next powerup in " + timeRemaining.ToString("F0") + "s";
            timeRemaining -= 1.0f;
            yield return new WaitForSeconds(1.0f);
        }
        for(int i = 0; i <= 30; i++)
        {
            text.text = "Next powerup in " + timeRemaining.ToString("F1") + "s";
            timeRemaining -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        powerUpGiverUI();
    }

    void PowerupGiver() // With Director AI
    {
        int count = 0;
        for(int i = 0; i < scores.Length; i++) // First, count the amount of players playing.
        {
            if (scores[i].currentScore != 0)
                count++;
        }
        theScores = new int[count + 1];
        for(int i = 0; i < theScores.Length;) // Then, get all the scores.
        {
            theScores[i] = scores[i].currentScore;
        }
        Array.Reverse(theScores); // Then it's sorted from strongest (0) to weakest (>=1)
        if(theScores.Length == 2) // 2 Players playing
        {
            for (int i = 0; i < theScores.Length; i++) // 0 strongest, 1 weakest
            {
                switch (i)
                {
                    case 0:
                        break;
                }
            }
        }
        if (theScores.Length == 3) // 3 Players playing
        {
            for (int i = 0; i < theScores.Length; i++) // 0 strongest, 2 weakest
            {
                switch (i)
                {
                    case 0:
                        break;
                }
            }
        }
        if (theScores.Length == 4) // 4 Players playing
        {
            for (int i = 0; i < theScores.Length; i++) // 0 strongest, 3 weakest
            {
                switch (i)
                {
                    case 0:
                        break;
                }
            }
        }
    }
}
