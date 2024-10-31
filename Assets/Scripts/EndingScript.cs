using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class EndingScript : MonoBehaviour
{
    public ScoreScriptable[] finalScores;
    DirectorAI director;

    int[] theScores;
    int[] playerScoresByOrder;

    [SerializeField] TMP_Text firstPlace;
    [SerializeField] TMP_Text secondPlace;
    [SerializeField] TMP_Text thirdPlace;
    [SerializeField] TMP_Text fourthPlace;
    // Start is called before the first frame update
    void Start()
    {
        director = FindAnyObjectByType<DirectorAI>();
        theScores = new int[director.numOfPlayers];
        playerScoresByOrder = new int[director.numOfPlayers];
        for (int i = 1; i <= director.numOfPlayers; i++)
        {
            theScores[i-1] = finalScores[i-1].currentScore;
            playerScoresByOrder[i-1] = finalScores[i-1].currentScore;
        }
        Array.Sort(theScores);
        if (director.numOfPlayers == 2)
        {
            if (theScores[0] == playerScoresByOrder[0]) // If Player 1 got the worst score...
            {
                updateRanking(secondPlace, 1, theScores[1]); // Player 1, 2nd Place
                updateRanking(firstPlace, 2, theScores[0]);  // Player 2, 1st Place
            }
            else if (theScores[0] == playerScoresByOrder[1]) // If Player 2 got the worst score...
            {
                updateRanking(firstPlace, 1, theScores[1]);  // Player 1, 1st Place
                updateRanking(secondPlace, 2, theScores[0]); // Player 2, 2nd Place 
            }
        }
        else if(director.numOfPlayers == 3)
        {
            int biggest = theScores[2];
            int average = theScores[1];
            if (biggest == playerScoresByOrder[0]) // Player 1, 1st Place
            {
                updateRanking(firstPlace, 1, theScores[2]);
                if (average == playerScoresByOrder[1]) // Player 2, 2nd Place and Player 3, 3rd Place
                {
                    updateRanking(secondPlace, 2, theScores[1]);
                    updateRanking(thirdPlace, 3, theScores[0]);
                }
                else if (average  == playerScoresByOrder[2]) // Player 3, 2nd Place and Player 2, 3rd Place
                {
                    updateRanking(secondPlace, 3, theScores[1]);
                    updateRanking(thirdPlace, 2, theScores[0]);
                }
            }
            else if (biggest == playerScoresByOrder[1]) // Player 2, 1st Place
            {
                updateRanking(firstPlace, 2, theScores[2]);
                if (average == playerScoresByOrder[0]) // Player 1, 2nd Place and Player 3, 3rd Place
                {
                    updateRanking(secondPlace, 1, theScores[1]);
                    updateRanking(thirdPlace, 3, theScores[0]);
                }
                else if (average == playerScoresByOrder[2]) // Player 3, 2nd Place and Player 1, 3rd Place
                {
                    updateRanking(secondPlace, 3, theScores[1]);
                    updateRanking(thirdPlace, 1, theScores[0]);
                }
            }
            else if (biggest == playerScoresByOrder[2]) // Player 3, 1st Place
            {
                updateRanking(firstPlace, 3, theScores[2]);
                if (average == playerScoresByOrder[1]) // Player 2, 2nd Place and Player 1, 3rd Place
                {
                    updateRanking(secondPlace, 2, theScores[1]);
                    updateRanking(thirdPlace, 3, theScores[0]);
                }
                else if (average == playerScoresByOrder[0]) // Player 1, 2nd Place and Player 2, 3rd Place
                {
                    updateRanking(secondPlace, 1, theScores[1]);
                    updateRanking(thirdPlace, 2, theScores[0]);
                }
            }

        }
    }

    void updateRanking(TMP_Text place, int playerIndex, int score)
    {
        place.text = "Player " + playerIndex.ToString() + " - " + score.ToString();
    }
}
