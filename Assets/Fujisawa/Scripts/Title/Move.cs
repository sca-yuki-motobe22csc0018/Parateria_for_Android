using UnityEngine;

public class Move : MonoBehaviour
{
    RectTransform rectTransform;
    float time;
    float y = 0;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveDistance;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        y = Mathf.Abs(Mathf.Cos(time * moveSpeed)) * moveDistance;
        rectTransform.localPosition = new Vector2(rectTransform.localPosition.x, y);
    }

    float easeInOutQuart(float x)
    {
        return x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
    }

    float easeOutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }
}
