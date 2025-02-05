using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    public float speed;
    float MaxX=-75;
    float RespawnX=75;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < MaxX)
        {
            transform.position = new Vector3(RespawnX, -3.25f, 0);
        }
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
