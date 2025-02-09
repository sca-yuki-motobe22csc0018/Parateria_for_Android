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
            int feverRandMax;
            if (PlayerController.fever)
            {
                feverRandMax = 7;
            }
            else
            {
                feverRandMax = 30;
            }
            int rand = Random.Range(0,feverRandMax);
            if (rand < 1)
            {
                enemy[0].SetActive(true);
                enemy[1].SetActive(true);
                enemy[2].SetActive(true);
            }
            else 
            if (rand < 3)
            {
                enemy[1].SetActive(true);
                enemy[2].SetActive(true);
            }
            else 
            if (rand < 5)
            {
                enemy[0].SetActive(true);
                enemy[2].SetActive(true);
            }
            else if (rand < 7)
            {
                enemy[0].SetActive(true);
                enemy[1].SetActive(true);
            }
            else if (rand < 13)
            {
                enemy[0].SetActive(true);
            }
            else if (rand < 19)
            {
                enemy[1].SetActive(true);
            }
            else if(rand<25)
            {
                enemy[2].SetActive(true);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            heal[i].SetActive(false);
        }
        int feverRandMaxH;
        if (PlayerController.charaNum == 1&& PlayerController.fever)
        {
            feverRandMaxH = 3;
        }
        else
        {
            feverRandMaxH = 30;
        }
        
        int randH = Random.Range(0, feverRandMaxH);
        if (randH <3)
        {
            heal[randH].SetActive(true);
        }
    }
    void Update()
    {
        if (!GameController.gameEnd)
        {

            speed = controller.StageSpeed;
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
    }
}
