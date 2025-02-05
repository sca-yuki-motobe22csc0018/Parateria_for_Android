using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudMover : MonoBehaviour
{
    RectTransform myRect;
    [SerializeField] RectTransform rect;
    public float moveSpeed {  get; private set; }
    public float MoveSpeed { set {  moveSpeed = value; } }

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        myRect.anchoredPosition -= new Vector2(1f, 0) * moveSpeed * Time.deltaTime;
        if(myRect.anchoredPosition.x <= 0)
        {
            rect.anchoredPosition = new Vector2(1920f + myRect.anchoredPosition.x, 0);
        }
    }
}
