using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float StageSpeed;
    public float PlusSpeed;
    public float PlusTime;
    private float PlusTimer;
    public int PlusCount;
    private int PlusCounter;
    public GameObject JumpButton;
    // Start is called before the first frame update
    void Start()
    {
        PlusTimer = 0;
        PlusCounter = 0;
        JumpButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlusCounter < PlusCount)
        {
            PlusTimer += Time.deltaTime;
            if (PlusTimer > PlusTime)
            {
                PlusTimer = 0;
                StageSpeed += PlusSpeed;
                PlusCounter++;
            }
        }
    }
}
