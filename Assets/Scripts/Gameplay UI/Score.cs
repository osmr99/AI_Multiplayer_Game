using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text size;
    float startingFontSize = 35;
    float startingScale = 1;
    int dotScore = 50;
    public ScoreScriptable scoreAmount;
    int sizeAmount = 0;
    Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        score.fontSize = startingFontSize;
        scaleChange = new Vector3(1.075f, 1.075f, 1.075f);
    }

    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(GrowthScore());
    }

    public void updateScoreAndSize()
    {
        scoreAmount.currentScore += dotScore;
        sizeAmount++;
        score.text = "Score: " + scoreAmount.currentScore;
        score.transform.localScale = scaleChange;
        score.transform.DOScale(startingScale, 0.2f).ForceInit();
        size.text = sizeAmount.ToString();
        size.transform.localScale = scaleChange;
        size.transform.DOScale(startingScale, 0.3f).ForceInit();
    }

    IEnumerator GrowthScore()
    {
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
            updateScoreAndSize();
        }
    }
}
