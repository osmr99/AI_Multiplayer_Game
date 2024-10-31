using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DirectorAI : MonoBehaviour
{
    // Director AI TLDR - Biggest player gets speedboost, the others get growth

    public ScoreScriptable[] scores;
    public Dotcolors myColors;
    TMP_Text text;
    public int[] theScores;
    public int[] playerScoresByOrder;
    public int targetScore;

    GameObject player1UI;
    GameObject player2UI;
    GameObject player3UI;
    GameObject player4UI;

    PlayerMovement player1;
    PlayerMovement player2;
    PlayerMovement player3;
    PlayerMovement player4;

    SceneHandler2 sceneHandler;

    public int numOfPlayers = 0;
    bool stopEverything = false;

    public void TheStart()
    {
        StartCoroutine(TheStartTwo());
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < numOfPlayers; i++)
        {
            if (scores[i].currentScore >= targetScore && stopEverything == false)
            {
                stopEverything = true;
                StartCoroutine(loadEnding());
            }
            else if(stopEverything)
            {
                if (numOfPlayers == 1)
                {
                    player1.speed = 0;
                }
                else if (numOfPlayers == 2)
                {
                    player1.speed = 0;
                    player2.speed = 0;
                }
                else if (numOfPlayers == 3)
                {
                    player1.speed = 0;
                    player2.speed = 0;
                    player3.speed = 0;
                }
                else if (numOfPlayers == 4)
                {
                    player1.speed = 0;
                    player2.speed = 0;
                    player3.speed = 0;
                    player4.speed = 0;
                }
            }
        }
    }

    IEnumerator TheStartTwo()
    {
        yield return new WaitForSeconds(0.05f);
        text = GameObject.Find("Power Up Cooldown").GetComponentInChildren<TMP_Text>();
        player1UI = GameObject.Find("Player 1 UI");
        player2UI = GameObject.Find("Player 2 UI");
        player3UI = GameObject.Find("Player 3 UI");
        player4UI = GameObject.Find("Player 4 UI");
        player1UI.SetActive(false);
        player2UI.SetActive(false);
        player3UI.SetActive(false);
        player4UI.SetActive(false);
        sceneHandler = FindAnyObjectByType<SceneHandler2>();
        for(int i = 0; i < sceneHandler.myArray.Length; i++)
        {
            if(sceneHandler.myArray[i].playerIndex == 1)
            {
                player1 = sceneHandler.myArray[i];
            }
            if(sceneHandler.myArray[i].playerIndex == 2)
            {
                player2 = sceneHandler.myArray[i];
            }
            if(sceneHandler.myArray[i].playerIndex == 3)
            {
                player3 = sceneHandler.myArray[i];
            }
            if (sceneHandler.myArray[i].playerIndex == 4)
            {
                player4 = sceneHandler.myArray[i];
            }
        }
        if (player1 != null)
            numOfPlayers++;
            if (player2 != null)
                numOfPlayers++;
                    if (player3 != null)
                        numOfPlayers++;
                            if (player4 != null)
                                numOfPlayers++;
        if (numOfPlayers == 1)
        {
            player1UI.SetActive(true);
        }
        else if (numOfPlayers == 2)
        {
            player1UI.SetActive(true);
            player2UI.SetActive(true);
        }
        else if (numOfPlayers == 3)
        {
            player1UI.SetActive(true);
            player2UI.SetActive(true);
            player3UI.SetActive(true);
        }
        else if(numOfPlayers == 4)
        {
            player1UI.SetActive(true);
            player2UI.SetActive(true);
            player3UI.SetActive(true);
            player4UI.SetActive(true);
        }

        if(numOfPlayers > 1)
        {
            StartCoroutine(powerUpGiverUI());
        }
        else
        {
            text.text = "Playground mode";
        }

    }

    IEnumerator powerUpGiverUI()
    {
        if(!stopEverything)
        {
            float timeRemaining = 12f;
            for (int i = 0; i <= 8; i++)
            {
                text.text = "Next powerup in " + timeRemaining.ToString("F0") + "s";
                timeRemaining -= 1.0f;
                yield return new WaitForSeconds(1.0f);
            }
            for (int i = 0; i <= 30; i++)
            {
                text.text = "Next powerup in " + timeRemaining.ToString("F1") + "s";
                timeRemaining -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            PowerUpGiver();
            yield return new WaitForSeconds(1);
            ChangeColor(myColors.colors[0]);
            StartCoroutine(powerUpGiverUI());
        }
    }

    void PowerUpGiver() // Director AI
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
        Array.Sort(theScores); // Then it's sorted from strongest (>0) to weaker (0)
        StartCoroutine(PowerUpGiverTwo());
    }

    // Director AI TLDR - Biggest player gets speedboost, the others get growth
    IEnumerator PowerUpGiverTwo()
    {
        yield return new WaitForSeconds(0.05f);
        if (theScores.Length == 2 && theScores[0] != theScores[1]) // 2 Players playing
        {
            // 1 strongest, 0 weakest
            if (theScores[0] == playerScoresByOrder[0]) // Player 1's score;
            {
                PlayerOneGrowth();
                PlayerTwoSpeed();
            }
            else if (theScores[0] == playerScoresByOrder[1]) // Player's 2 score
            {
                PlayerOneSpeed();
                PlayerTwoGrowth();
            }
            else
            {
                FailedCondition(); // If there's only 1 player on the screen;
            }
        }
        else if (theScores.Length == 3 && theScores[0] != theScores[1] && theScores[0] != theScores[2] && theScores[1] != theScores[2]) // 3 Players playing
        {
            // 2 strongest, 1 average, 0 weakest
            if (theScores[2] == playerScoresByOrder[0]) // Player's 1 score
            {
                PlayerOneSpeed();
                PlayerTwoGrowth();
                PlayerThreeGrowth();
            }
            else if (theScores[2] == playerScoresByOrder[1]) // Player's 2 score
            {
                PlayerOneGrowth();
                PlayerTwoSpeed();
                PlayerThreeGrowth();
            }
            else if (theScores[2] == playerScoresByOrder[2]) // Player's 3 score
            {
                PlayerOneGrowth();
                PlayerTwoGrowth();
                PlayerThreeSpeed();
            }
            else
            {
                FailedCondition(); // If there's only 1 player on the screen;
            }
        }
        else if (theScores.Length == 4 && theScores[0] != theScores[1] && theScores[0] != theScores[2] && theScores[0] != theScores[3] && theScores[1] != theScores[2] && theScores[1] != theScores[3] && theScores[2] != theScores[3]) // 4 Players playing
        {
            // 3 strongest, 2 and 1 average, 0 weakest
            if (theScores[3] == playerScoresByOrder[0]) // Player's 1 score
            {
                PlayerOneSpeed();
                PlayerTwoGrowth();
                PlayerThreeGrowth();
                PlayerFourGrowth();
            }
            else if (theScores[3] == playerScoresByOrder[1]) // Player's 2 score
            {
                PlayerOneGrowth();
                PlayerTwoSpeed();
                PlayerThreeGrowth();
                PlayerFourGrowth();
            }
            else if (theScores[3] == playerScoresByOrder[2]) // Player's 3 score
            {
                PlayerOneGrowth();
                PlayerTwoGrowth();
                PlayerThreeSpeed();
                PlayerFourGrowth();
            }
            else if (theScores[3] == playerScoresByOrder[3]) // Player's 4 score
            {
                PlayerOneGrowth();
                PlayerTwoGrowth();
                PlayerThreeGrowth();
                PlayerFourSpeed();
            }
            else
            {
                FailedCondition(); // If there's only 1 player on the screen;
            }
        }
        else
        {
            FailedCondition(); // If there's only 1 player on the screen or if a player contains the score 0 and/or is respawning
        }
    }

    void ChangeColor(Color endingColor)
    {
        text.DOColor(endingColor, 0.25f);
    }

    void FailedCondition()
    {
        text.text = "Condition not met!";
        ChangeColor(myColors.colors[1]);
    }

    IEnumerator loadEnding()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync("Ending", LoadSceneMode.Additive);
    }

    void PlayerOneGrowth()
    {
        player1.GetComponent<Powerups>().StartGrowth();
        player1UI.GetComponentInChildren<PowerupUI>().StartGrowth();
        player1UI.GetComponentInChildren<Score>().StartGrowth();
    }

    void PlayerTwoGrowth()
    {
        player2.GetComponent<Powerups>().StartGrowth();
        player2UI.GetComponentInChildren<PowerupUI>().StartGrowth();
        player2UI.GetComponentInChildren<Score>().StartGrowth();
    }

    void PlayerThreeGrowth()
    {
        player3.GetComponent<Powerups>().StartGrowth();
        player3UI.GetComponentInChildren<PowerupUI>().StartGrowth();
        player3UI.GetComponentInChildren<PowerupUI>().StartGrowth();
    }

    void PlayerFourGrowth()
    {
        player4.GetComponent<Powerups>().StartGrowth();
        player4UI.GetComponentInChildren<PowerupUI>().StartGrowth();
        player4UI.GetComponentInChildren<PowerupUI>().StartGrowth();
    }

    void PlayerOneSpeed()
    {
        player1.GetComponent<Powerups>().StartSpeedBoost();
        player1UI.GetComponentInChildren<PowerupUI>().StartSpeedBoost();
    }

    void PlayerTwoSpeed()
    {
        player2.GetComponent<Powerups>().StartSpeedBoost();
        player2UI.GetComponentInChildren<PowerupUI>().StartSpeedBoost();
    }

    void PlayerThreeSpeed()
    {
        player3.GetComponent<Powerups>().StartSpeedBoost();
        player3UI.GetComponentInChildren<PowerupUI>().StartSpeedBoost();
    }
    void PlayerFourSpeed()
    {
        player4.GetComponent<Powerups>().StartSpeedBoost();
        player4UI.GetComponentInChildren<PowerupUI>().StartSpeedBoost();
    }
}
