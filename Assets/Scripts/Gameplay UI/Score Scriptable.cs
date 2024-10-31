using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using DG.Tweening;
using TMPro;

[CreateAssetMenu(menuName = "The Score Amount")]
public class ScoreScriptable : ScriptableObject
{
    public int currentScore;
    public int currentSize;
}
