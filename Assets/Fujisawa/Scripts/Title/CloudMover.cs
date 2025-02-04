using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudMover : MonoBehaviour
{
    RectTransform myRect;
    [SerializeField] RectTransform rect;
    public float moveSpeed;
    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        myRect.anchoredPosition -= new Vector2(1, 0) * moveSpeed;
        if(myRect.anchoredPosition.x <= 0)
        {
            rect.anchoredPosition = new Vector2(1920f + myRect.anchoredPosition.x, 0);
        }
    }
}
