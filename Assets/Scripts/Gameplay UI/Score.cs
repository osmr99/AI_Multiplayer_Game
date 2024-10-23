using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    float startingFontSize = 75;
    float startingScale = 1;
    int dotScore = 50;
    public ScoreScriptable scoreAmount;
    Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        score.fontSize = startingFontSize;
        startingScale = score.transform.localScale.x;
        scaleChange = new Vector3(1.075f, 1.075f, 1.075f);
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.G)) // Here will be when the player collects a dot (this is just a template)
            //updateScore();
    }

    public void updateScore()
    {
        scoreAmount.currentScore += dotScore;
        score.text = "Score: " + scoreAmount.currentScore;
        score.transform.localScale = scaleChange;
        score.transform.DOScale(startingScale, 0.2f).ForceInit(); ;
    }    
}
