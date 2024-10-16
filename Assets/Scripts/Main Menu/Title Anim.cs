using UnityEngine;
using TMPro;

public class TitleAnim : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] float maxSpacing;
    [SerializeField] float spacingRate;
    [SerializeField] float startingFontSize;
    [SerializeField] float fontSizeRate;
    [SerializeField] float fontSizeMultiplier;
    float fontSize;
    float bigFontSize;
    float spacing = 0;
    bool animOut = true;
    bool growing = true;

    // Start is called before the first frame update
    void Start()
    {
        title.characterSpacing = spacing;
        title.fontSize = fontSize;
        fontSize = startingFontSize;
        bigFontSize = fontSize * fontSizeMultiplier;
    }

    private void FixedUpdate()
    {
        SpacingAnim();
        FontSizeAnim();
    }

    void SpacingAnim()
    {
        title.characterSpacing = spacing;
        if (animOut && spacing < maxSpacing)
        {
            spacing += spacingRate;
            if (spacing >= maxSpacing)
                animOut = false;
        }
        if (!animOut && spacing > 0)
        {
            spacing -= spacingRate;
            if (spacing <= 0)
                animOut = true;
        }
    }

    void FontSizeAnim()
    {
        title.fontSize = fontSize;
        if(growing && fontSize < bigFontSize)
        {
            fontSize += fontSizeRate;
            if (fontSize >= bigFontSize)
                growing = false;
        }
        if(!growing && fontSize > startingFontSize)
        {
            fontSize -= fontSizeRate;
            if (fontSize <= startingFontSize)
                growing = true;
        }
    }
}
