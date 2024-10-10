using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StageController : MonoBehaviour
{
    float speed;
    GameController controller;

    private void Start()
    {
        controller = FindObjectOfType<GameController>();
        speed = controller.StageSpeed;
    }
    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }

    
}
