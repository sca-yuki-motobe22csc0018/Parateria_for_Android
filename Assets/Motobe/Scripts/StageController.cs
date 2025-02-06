using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class StageController : MonoBehaviour
{
    float speed;
    GameController controller;
    public int stageNum;
    public GameObject[] enemy;
    public GameObject[] heal;

    private void Start()
    {
        controller = FindObjectOfType<GameController>();
        if (stageNum == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                enemy[i].SetActive(false);
            }
            int rand = Random.Range(0,4);
            if (rand != 3)
            {
                enemy[rand].SetActive(true);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            heal[i].SetActive(false);
        }
        int randH = Random.Range(0, 30);
        if (randH <3)
        {
            heal[randH].SetActive(true);
        }
    }
    void Update()
    {
        speed = controller.StageSpeed;
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }
}
