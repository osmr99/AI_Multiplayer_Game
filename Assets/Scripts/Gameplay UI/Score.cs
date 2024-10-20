using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] float startingFontSize;
    [SerializeField] float startingScale;
    [SerializeField] int dotScore;
    Vector3 scaleChange;
    int scoreAmount = 0;

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
        scoreAmount += dotScore;
        score.text = "Score: " + scoreAmount.ToString();
        score.transform.localScale = scaleChange;
        score.transform.DOScale(startingScale, 0.2f).ForceInit(); ;
    }    
}
