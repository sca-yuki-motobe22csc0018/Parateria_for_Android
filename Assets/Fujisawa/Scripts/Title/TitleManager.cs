using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Image TitleLogo;
    RectTransform TitleLogoRect;
    [SerializeField] CloudMover[] cloudMovers;
    [SerializeField] float cloudMoveSpeed;
    [SerializeField] Text startText;

    private void Awake()
    {
        TitleLogoRect = TitleLogo.GetComponent<RectTransform>();
        foreach (var mover in cloudMovers)
        {
            if (mover != null) mover.MoveSpeed = cloudMoveSpeed;
        }
    }

    private void Start()
    {
        StartCoroutine(BrinkText());
    }

#if UNITY_EDITOR
    //デバッグ用
    private void Update()
    {
        foreach (var mover in cloudMovers)
        {
            if (mover != null) mover.MoveSpeed = cloudMoveSpeed;
        }
    }
#endif

    bool isSceneChange = false;
    IEnumerator BrinkText()
    {
        isSceneChange = false;
        Color color = startText.color;
        while (!isSceneChange)
        {
            yield return null;
            startText.color = GetAlphaColor(startText.color);
        }

        startText.color = color;
        time = 0;
        speed *= 10;
        brinkTime = speed;
        while (time < brinkTime)
        {
            yield return null;
            startText.color = GetAlphaColor(startText.color);
        }
        startText.color = color;
        SceneManager.LoadScene("Home");
    }

    float time = 0;
    [SerializeField]
    float speed;
    [SerializeField]
    float sinLim;
    float brinkTime;
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * speed;
        color.a = Mathf.Abs(Mathf.Sin(time * Mathf.PI / 2)) * (1f - sinLim) + sinLim;
        return color;
    }

    public void GoHomeScene()
    {
        isSceneChange = true;
    }
}
