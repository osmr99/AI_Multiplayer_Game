using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    [SerializeField] SpriteRenderer dot;
    [SerializeField] Dotcolors colors;
    [SerializeField] float speed = 1.0f;
    public Transform targetPlayer;
    Color rngColor;
    int num;
    // Start is called before the first frame update
    void OnEnable()
    {
        num = Random.Range(0, colors.colors.Count);
        rngColor = colors.colors[num];
        rngColor.a = 1;
        dot.color = rngColor;
    }

    private void Update()
    {
        if(targetPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime) ;
        }
    }
}
