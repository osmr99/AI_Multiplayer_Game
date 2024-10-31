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
    public int[] playerScoresByOrder;

    GameObject player1UI;
    GameObject player2UI;
    GameObject player3UI;
    GameObject player4UI;

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
        player1UI = GameObject.Find("Player 1 UI");
        player2UI = GameObject.Find("Player 2 UI");
        player3UI = GameObject.Find("Player 3 UI");
        player4UI = GameObject.Find("Player 4 UI");
        player1 = GameObject.Find("Player Prefab 1");
        player2 = GameObject.Find("Player Prefab 2");
        player3 = GameObject.Find("Player Prefab 3");
        player4 = GameObject.Find("Player Prefab 4");
        StartCoroutine(powerUpGiverUI());
    }

    IEnumerator powerUpGiverUI()
    {
        float timeRemaining = 12f;
        for (int i = 0; i <= 8; i++)
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
        PowerupGiver();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(powerUpGiverUI());
    }

    void PowerupGiver() // With Director AI
    {
        int count = 0;
        for(int i = 0; i < scores.Length; i++) // First, count the amount of players playing.
        {
            if (scores[i].currentScore != 0)
                count++;
        }
        theScores = new int[count];
        playerScoresByOrder = new int[count];
        for(int i = 0; i < theScores.Length; i++) // Then, get all the scores.
        {
            theScores[i] = scores[i].currentScore;
            playerScoresByOrder[i] = scores[i].currentScore;
        }
        Array.Reverse(theScores); // Then it's sorted from strongest (0) to weakest (>=1)
        if(theScores.Length == 2) // 2 Players playing
        {
            // 0 strongest, 1 weakest
            if (theScores[0] == playerScoresByOrder[0]) // Player 1's score;
            {
                PlayerTwoSpeed();
                PlayerOneGrowth();
            }
            else
            {
                PlayerOneSpeed();
                PlayerTwoGrowth();
            }
        }
    }

    void PlayerOneGrowth()
    {
        player1.GetComponentInChildren<Powerups>().StartGrowth();
        player1UI.GetComponentInChildren<PowerupUI>().StartGrowth();
        player1UI.GetComponentInChildren<Score>().StartGrowth();
    }

    void PlayerTwoGrowth()
    {
        player2.GetComponentInChildren<Powerups>().StartGrowth();
        player2UI.GetComponentInChildren<PowerupUI>().StartGrowth();
        player2UI.GetComponentInChildren<Score>().StartGrowth();
    }

    void PlayerThreeGrowth()
    {
        player3.GetComponentInChildren<Powerups>().StartGrowth();
        player3UI.GetComponentInChildren<PowerupUI>().StartGrowth();
        player3UI.GetComponentInChildren<PowerupUI>().StartGrowth();
    }

    void PlayerFourGrowth()
    {
        player4.GetComponentInChildren<Powerups>().StartGrowth();
        player4UI.GetComponentInChildren<PowerupUI>().StartGrowth();
        player4UI.GetComponentInChildren<PowerupUI>().StartGrowth();
    }

    void PlayerOneSpeed()
    {
        player1.GetComponentInChildren<Powerups>().StartSpeedBoost();
        player1UI.GetComponentInChildren<PowerupUI>().StartSpeedBoost();
    }

    void PlayerTwoSpeed()
    {
        player2.GetComponentInChildren<Powerups>().StartSpeedBoost();
        player2UI.GetComponentInChildren<PowerupUI>().StartSpeedBoost();
    }
}
