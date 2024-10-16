using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button button;
    [SerializeField] TMP_Text text;
    [SerializeField] float addedScale;
    [SerializeField] float time;
    [SerializeField] int i;
    [SerializeField] int x;
    [SerializeField] int z;
    float originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale + addedScale, time);
        //Debug.Log("hi");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, time);
        //Debug.Log("bye");
    }

    public void Click()
    {
        transform.DOShakePosition(0.5f, i, x, z);
    }

}
