using UnityEngine;

[ExecuteAlways, RequireComponent(typeof(RectTransform))]
public class SafeAreaSample : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        var area = Screen.safeArea;
        var anchorMin = area.position;
        var anchorMax = area.position + area.size;
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
    }

    private void Update()
    {
        
    }
}
