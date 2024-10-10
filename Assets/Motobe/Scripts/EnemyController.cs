using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject GiriJumpPoint;
    private Vector3 GiriJumpPointPosition = new Vector3(-1.5f, 0, 0);
    GameController controller;
    float speed;
    public  float ThisSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        GiriJumpPoint.transform.parent = null;
        speed = controller.StageSpeed+ThisSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GiriJumpPoint!=null)
        {
            GiriJumpPoint.transform.position = this.transform.position + GiriJumpPointPosition;
        }
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }
}
