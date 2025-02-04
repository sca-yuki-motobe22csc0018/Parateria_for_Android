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

    private void Awake()
    {
        TitleLogoRect = TitleLogo.GetComponent<RectTransform>();
        foreach (var mover in cloudMovers)
        {
            if (mover != null) mover.moveSpeed = cloudMoveSpeed;
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        foreach (var mover in cloudMovers)
        {
            if (mover != null) mover.moveSpeed = cloudMoveSpeed;
        }
    }
#endif

    public void GoHomeScene()
    {
        SceneManager.LoadScene("Home");
    }
}
