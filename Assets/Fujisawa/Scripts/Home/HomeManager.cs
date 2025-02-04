using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    void Awake()
    {
        selectNum = 0;
        charaFades = new CharaFade[charas.Length];
        charaRects = new RectTransform[charas.Length];
        anchoredPoses = new Vector2[charas.Length];
        for (int i = 0;i < charaFades.Length;i++)
        {
            charaFades[i] = charas[i].GetComponent<CharaFade>();
            charaRects[i] = charas[i].GetComponent<RectTransform>();
            anchoredPoses[i] = charaRects[i].anchoredPosition;
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
        Debug.Log(inputPos.x);
        Debug.Log(outputPos.x);
        float dirLength = Mathf.Abs(inputPos.x - outputPos.x);
        if (dirLength > 200)
        {
            if(inputPos.x > outputPos.x)
            {
                //‰E‘¤‚ÌƒLƒƒƒ‰‚ª‘O‚É—ˆ‚é
                StartCoroutine(CharaMoveAnim(_charaNum, true));
            }
            else
            {
                StartCoroutine(CharaMoveAnim(_charaNum, false));
            }
        }
        else
        {
            charaRects[selectNum].anchoredPosition = anchoredPoses[0];
        }
    }

    IEnumerator CharaMoveAnim(int _charaNum, bool dir)
    {
        int add = dir ? 1 : charas.Length - 1;
        for (int i = 0; i < charas.Length ; i++)
        {
            int num = (_charaNum + i) % charas.Length;
            charaRects[num].DOAnchorPos(anchoredPoses[(num + add) % charas.Length], 0.3f);
        }

        charaFades[_charaNum].FadeOut(0.3f);
        charaRects[_charaNum].DOScale(Vector2.one * backCharaSize, 0.3f);


        int top = dir ? charas.Length - 1 : 1;
        selectNum = (selectNum + top) % charas.Length;
        Debug.Log(selectNum);
        charaFades[selectNum].FadeIn(0.3f);
        charaRects[selectNum].DOScale(Vector2.one, 0.3f);
        inputPos = Vector2.zero;
        yield return new WaitForSeconds(0.3f);
    }

    public void SceneChange(string _sceneName)
    {
        StartCoroutine(SceneChangeAnim(_sceneName));
    }

    IEnumerator SceneChangeAnim(string _sceneName)
    {
        yield return null;
        SceneManager.LoadScene(_sceneName);
    }
}
