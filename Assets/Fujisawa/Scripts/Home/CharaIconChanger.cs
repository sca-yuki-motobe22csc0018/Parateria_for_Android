using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharaIconChanger : MonoBehaviour
{
    Image charaIcon;
    [SerializeField] Sprite[] charaSprites;
    [SerializeField] GameObject charaBaseImgObj;
    Image charaBaseImg;
    [SerializeField] Color[] charaColors;
    [SerializeField] Image namePlateImg;
    [SerializeField] Sprite[] namePlateSprites;
    [SerializeField] RectTransform charaIconObjRect;

    private void Awake()
    {
        charaIcon = GetComponent<Image>();
        charaBaseImg = charaBaseImgObj.GetComponent<Image>();
    }

    public IEnumerator IconChange(int charaNum, bool dir)
    {
        float animTime = 0.5f;
        float d = dir ? 1f : -1f;
        float addAngle = 45f;
        float namePlateRectY = namePlateImg.rectTransform.anchoredPosition.y;
        namePlateImg.rectTransform.DOAnchorPosY(600f,0.2f);
        yield return new WaitForSeconds(0.2f);
        namePlateImg.sprite = namePlateSprites[charaNum];
        charaBaseImgObj.transform.DOLocalRotate(new Vector3(0, 0, (720f + addAngle) * d), animTime, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(animTime / 2);
        charaIcon.sprite = charaSprites[charaNum];
        charaBaseImg.color = charaColors[charaNum];
        yield return new WaitForSeconds(animTime / 2);
        charaBaseImgObj.transform.DORotate(new Vector3(0, 0, (addAngle + 5) * d), animTime / 2);
        yield return new WaitForSeconds(animTime / 2);
        charaBaseImgObj.transform.DORotate(new Vector3(0, 0, 0), animTime / 4);
        namePlateImg.rectTransform.DOAnchorPosY(namePlateRectY, animTime / 4);
        yield return new WaitForSeconds(animTime / 4);
        charaIconObjRect.DOAnchorPosY(charaIconObjRect.anchoredPosition.y - 10f,0.15f)
                        .OnComplete(()=>{
                            charaIconObjRect.DOAnchorPosY(charaIconObjRect.anchoredPosition.y + 10f, 0f);
                        });
    }
}
