using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class HomeManager : MonoBehaviour
{
    [SerializeField] GameObject[] charas;
    CharaFade[] charaFades;
    RectTransform[] charaRects;
    Vector2 inputPos;
    Vector2 outputPos;
    int selectNum;

    Vector2[] anchoredPoses;
    const float backCharaSize = 0.4f;
    const float animTime = 0.75f;

    [SerializeField] GameObject[] sortObjs;
    [SerializeField] CharaIconChanger charaIcon;

    [SerializeField] GameObject[] shadows;
    RectTransform[] shadowRects;
    Vector2[] shadowAnchoredPoses;

    void Awake()
    {
        selectNum = 0;
        charaFades = new CharaFade[charas.Length];
        charaRects = new RectTransform[charas.Length];
        anchoredPoses = new Vector2[charas.Length];

        shadowRects = new RectTransform[charas.Length];
        shadowAnchoredPoses = new Vector2[charas.Length];

        for (int i = 0;i < charaFades.Length;i++)
        {
            charaFades[i] = charas[i].GetComponent<CharaFade>();
            charaRects[i] = charas[i].GetComponent<RectTransform>();
            anchoredPoses[i] = charaRects[i].anchoredPosition;
            shadowRects[i] = shadows[i].GetComponent<RectTransform>();
            shadowAnchoredPoses[i] = shadowRects[i].anchoredPosition;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            inputPos = Input.mousePosition;
        }
#else
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                inputPos = touch.position;
            }
        }
        else
        {
            inputPos = Vector2.zero;
        }
#endif
    }

    public void OnDragChara(int _charaNum)
    {
        if (selectNum != _charaNum) return;
#if UNITY_EDITOR
        Debug.Log(inputPos.x);
        outputPos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            charaRects[selectNum].position = new Vector2(outputPos.x, charaRects[selectNum].position.y);
            shadowRects[selectNum].position = new Vector2(charaRects[selectNum].position.x, shadowRects[selectNum].position.y);
        }
#else
    if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            outputPos = touch.position;
            if (touch.phase == TouchPhase.Moved)
            {
                charaRects[selectNum].position = new Vector2(outputPos.x, charaRects[selectNum].position.y);
            }
        }
#endif
    }

    public void OnDropChara(int _charaNum)
    {
        float dirLength = Mathf.Abs(inputPos.x - outputPos.x);
        if (dirLength > 100)
        {
            Debug.Log("drop");
            if (inputPos.x > outputPos.x)
            {
                //右側のキャラが前に来る
                StartCoroutine(CharaMoveAnim(_charaNum, true));
            }
            else
            {
                StartCoroutine(CharaMoveAnim(_charaNum, false));
            }
        }
        else
        {
            Debug.Log("失敗");
            charaRects[selectNum].anchoredPosition = anchoredPoses[0];
        }
    }

    IEnumerator CharaMoveAnim(int _charaNum, bool dir)
    {
        int add = dir ? charas.Length - 1 : 1;
        int sortAdd = dir ? 1 : charas.Length - 1;
        int sortPos = 0;
        for (int i = 0; i < charas.Length ; i++)
        {
            int num = (_charaNum + add + i) % charas.Length;
            shadowRects[num].DOAnchorPos(shadowAnchoredPoses[i],animTime);
            shadowRects[num].parent = sortObjs[sortPos].transform;

            charaRects[num].DOAnchorPos(anchoredPoses[i], animTime);
            charaRects[num].parent = sortObjs[sortPos].transform;
            sortPos = (sortPos + sortAdd) % charas.Length;
            Debug.Log($"{num}が{i}地点へ");
        }

        charaFades[_charaNum].FadeOut(animTime/2);
        charaRects[_charaNum].DOScale(Vector2.one * backCharaSize, animTime);
        shadowRects[_charaNum].DOScale(Vector2.one * backCharaSize, animTime);
        charas[_charaNum].GetComponent<Image>().raycastTarget = false;

        selectNum = (selectNum + add) % charas.Length;
        charaFades[selectNum].FadeIn(animTime/2);
        charaRects[selectNum].DOScale(Vector2.one, animTime);
        shadowRects[selectNum].DOScale(Vector2.one, animTime);
        inputPos = Vector2.zero;
        StartCoroutine(charaIcon.IconChange(selectNum,dir));
        yield return new WaitForSeconds(animTime);
        charas[selectNum].GetComponent<Image>().raycastTarget = true;
    }

    public void SceneChange(string _sceneName)
    {
        StartCoroutine(SceneChangeAnim(_sceneName));
    }

    IEnumerator SceneChangeAnim(string _sceneName)
    {
        yield return null;
        Locator<PlayerData>.Instance.SetCharaNumber(selectNum);
        if(_sceneName == "Ranking") Locator<PlayerData>.Instance.TitleToRanking(true);
        else Locator<PlayerData>.Instance.TitleToRanking(false);
        SceneManager.LoadScene(_sceneName);
    }
}
