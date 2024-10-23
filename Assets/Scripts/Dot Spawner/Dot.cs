using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    [SerializeField] SpriteRenderer dot;
    [SerializeField] Dotcolors colors;
    Color rngColor;
    int num;
    // Start is called before the first frame update
    void OnEnable()
    {
        num = UnityEngine.Random.Range(0, colors.colors.Count);
        rngColor = colors.colors[num];
        rngColor.a = 1;
        dot.color = rngColor;
    }
}
