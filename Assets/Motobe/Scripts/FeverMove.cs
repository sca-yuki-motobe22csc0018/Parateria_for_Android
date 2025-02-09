using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverMove : MonoBehaviour
{
    private float speed;
    float MaxX = -30;

    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(5, 8);
        speed=rand;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.gameEnd)
        {
            if (transform.position.x < MaxX)
            {
                Destroy(this.gameObject);
            }
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
}
