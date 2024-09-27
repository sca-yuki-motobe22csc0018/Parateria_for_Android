using UnityEngine;

public class Move : MonoBehaviour
{
    RectTransform rectTransform;
    float time;
    float y = 0;
    float startY;
    float startX;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveDistance;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.localPosition.y;
        startX = rectTransform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        y = Mathf.Sin(time * moveSpeed) * moveDistance;
        rectTransform.localPosition = new Vector2(startX, y + startY);
    }
}
