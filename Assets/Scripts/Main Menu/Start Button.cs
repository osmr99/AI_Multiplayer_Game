using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button button;
    //[SerializeField] TMP_Text text;
    [SerializeField] float addedScale;
    [SerializeField] float time;
    int strength = 10;
    int vibrato = 50;
    int randomness = 50;
    float originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale.x;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(button.enabled)
            transform.DOScale(originalScale + addedScale, time);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(button.enabled)
            transform.DOScale(originalScale, time);
    }

    public void Click()
    {
        transform.DOShakePosition(0.5f, strength, vibrato, randomness);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        button.enabled = false;
        yield return new WaitForSeconds(0.5f);
        SceneHandler2.LoadGame();
    }

}
