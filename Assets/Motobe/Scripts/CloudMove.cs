using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speedmax;
    public float speedmin;
    public float speednormal;
    private float speed;
    float MaxX = -75;
    
    public GameObject[] cloud;

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < 9; i++)
        {
            cloud[i].SetActive(false);
        }
        int rand = Random.Range(0, 12);
        
        if (rand < 3)
        {
            speed = speedmax;
        }else if (rand < 6)
        {
            speed = speednormal;
        }
        else if (rand < 9)
        {
            speed=speedmin;
        }
        else
        {
            rand -= 9;
            speed = speedmax;
        }
        cloud[rand].SetActive(true);
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
