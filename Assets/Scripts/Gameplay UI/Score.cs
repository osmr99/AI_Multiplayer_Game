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
    [SerializeField] TMP_Text playerText;
    [SerializeField] Dotcolors textColor;
    float startingFontSize = 35;
    float startingScale = 1;
    int dotScore = 50;
    public ScoreScriptable stats;
    [SerializeField] PowerupUI powerupUI;
    Vector3 scaleChange;
    SceneHandler2 sceneHandler;

    // Start is called before the first frame update
    void Start()
    {
        score.fontSize = startingFontSize;
        scaleChange = new Vector3(1.075f, 1.075f, 1.075f);
        score.color = textColor.colors[0];
        size.color = textColor.colors[0];
        playerText.color = textColor.colors[0];
        sceneHandler = FindAnyObjectByType<SceneHandler2>();
    }

    void Update()
    {
        //if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            //StartCoroutine(GrowthScore());
    }

    public void updateScoreAndSize()
    {
        if(powerupUI.allowPowerup)
        {
            stats.currentScore += dotScore;
            stats.currentSize++;
            score.text = "Score: " + stats.currentScore;
            score.transform.localScale = scaleChange;
            score.transform.DOScale(startingScale, 0.2f).ForceInit();
            size.text = stats.currentSize.ToString();
            size.transform.localScale = scaleChange;
            size.transform.DOScale(startingScale, 0.3f).ForceInit();
        }
    }

    public void SetColor(Color matchColor)
    {
        score.color = matchColor;
        size.color = matchColor;
    }

    IEnumerator GrowthScore()
    {
        for(int i = 0; i < 25; i++)
        {
            if (powerupUI.allowPowerup)
            {
                yield return new WaitForSeconds(0.2f);
                updateScoreAndSize();
            }
            else
                break;
        }
    }

    public void StartGrowth()
    {
        StartCoroutine(GrowthScore());
    }

    public void ResetUI()
    {
        score.transform.DOShakePosition(0.35f, 10, 50, 50);
        StartCoroutine(RespawningTimerUI());
    }

    IEnumerator RespawningTimerUI()
    {
        score.text = "";
        size.text = "";
        float timeRemaining = 5.0f;
        for (int i = 0; i <= 50; i++)
        {
            score.text = "Respawning in\n" + timeRemaining.ToString("F1") + "s";
            timeRemaining -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        score.text = "Score: 0";
        size.text = "0";
        yield return new WaitForSeconds(0.03f);
    }
}
