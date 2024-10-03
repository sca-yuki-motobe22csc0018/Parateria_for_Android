using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public float speed;
    /*
    public float defaultSpeed;

    public float plusSpeed;
    private float speed;
    private float timer;
    public float SpeedUpTime;
    */
    // Start is called before the first frame update
    void Start()
    {
        /*
        speed = defaultSpeed;
        timer = 0;
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        timer += Time.deltaTime;
        if (timer > SpeedUpTime)
        {
            timer = 0;
            speed += plusSpeed;
        }
        */
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }

    
}
