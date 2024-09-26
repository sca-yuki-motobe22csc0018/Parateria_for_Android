using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraResizer : MonoBehaviour
{
    [SerializeField] Vector2 targetResolution;
    [SerializeField] float targetOrthographicSize = 5;

    Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        ResizeCameraOrthographicSize();
    }

    void ResizeCameraOrthographicSize()
    {
        var currentResolution = new Vector2(Screen.width, Screen.height);
        var targetAspect = targetResolution.x / targetResolution.y;
        var currentAspect = currentResolution.x / currentResolution.y;

        if (currentAspect >= targetAspect)
        {
            camera.orthographicSize = targetOrthographicSize;
            return;
        }

        var orthoGraphicSize = targetOrthographicSize * (targetAspect / currentAspect);
        camera.orthographicSize = orthoGraphicSize;
    }
}
