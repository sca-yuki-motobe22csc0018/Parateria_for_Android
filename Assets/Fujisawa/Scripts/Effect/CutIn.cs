using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEditor;
using System;
using System.Xml.Serialization;

public class CutIn : MonoBehaviour
{
    [SerializeField] Image charaImg;
    [SerializeField] Image charaShadow;
    [SerializeField] Sprite[] charaSprites;
    [SerializeField] Image cutInBase;
    [SerializeField] Color[] charaColor;
    [SerializeField] Image[] cutInFades;
    [SerializeField] Text[] feverTexts;
    [SerializeField] int num;

    private void Awake()
    {
        Reset();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
            num = (num + 1) % charaSprites.Length;
            StartCoroutine(CutInAnim(num));
        }
    }
#endif

    private void Reset()
    {
        charaImg.rectTransform.anchoredPosition = new Vector2(1300f, charaImg.rectTransform.anchoredPosition.y);
        charaShadow.rectTransform.anchoredPosition = new Vector2(1300f, charaImg.rectTransform.anchoredPosition.y);
        for (int i = 0; i < feverTexts.Length; i++)
        {
            feverTexts[i].rectTransform.anchoredPosition = Vector2.zero;
            cutInFades[i].rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public IEnumerator CutInAnim(int _charaNum)
    {
        charaImg.sprite = charaSprites[_charaNum];
        charaShadow.sprite = charaSprites[_charaNum];
        cutInBase.color = charaColor[_charaNum];

        const float fadeAnim = 0.1f;
        cutInFades[0].rectTransform.DOAnchorPosY(300, fadeAnim);
        cutInFades[1].rectTransform.DOAnchorPosY(-300, fadeAnim);
        yield return new WaitForSeconds(fadeAnim);
        const float animTime = 0.1f;
        feverTexts[0].rectTransform.DOAnchorPosX(2000f, animTime);
        feverTexts[1].rectTransform.DOAnchorPosX(-2000f, animTime);

        charaImg.rectTransform.DOAnchorPosX(0, animTime);
        charaShadow.rectTransform.DOAnchorPosX(-100f, animTime);
        yield return new WaitForSeconds(1f);

        cutInFades[0].rectTransform.DOAnchorPosY(0, fadeAnim);
        cutInFades[1].rectTransform.DOAnchorPosY(0, fadeAnim);
        feverTexts[0].rectTransform.DOAnchorPosX(0f, animTime);
        feverTexts[1].rectTransform.DOAnchorPosX(0f, animTime);
    }
}
