using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CharaFade : MonoBehaviour
{
    Image charaFade;
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        charaFade = transform.GetChild(0).GetComponent<Image>();
    }

    public void FadeIn(float fadeTime)
    {
        charaFade.DOFade(0f, fadeTime);
    }

    public void FadeOut(float fadeTime)
    {
        charaFade.DOFade(0.5f, fadeTime);
    }
}
