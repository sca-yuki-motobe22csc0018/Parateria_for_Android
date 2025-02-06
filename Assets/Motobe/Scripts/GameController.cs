using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float StageSpeed;
    public float GameSpeed;
    public float PlusSpeed;
    public float PlusGameSpeed;
    public float PlusTime;
    private float PlusTimer;
    public int PlusCount;
    private int PlusCounter;
    float cloudTimer;
    float startX = 30;
    float MaxY = 11;
    float minY = 0;
    public static bool gameEnd;
    // public GameObject JumpButton;
    // Start is called before the first frame update
    void Start()
    {
        PlusTimer = 0;
        PlusCounter = 0;
        cloudTimer = 0;
        gameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        cloudTimer += Time.deltaTime;
        if (cloudTimer > 3.0f)
        {
            Cloud();
            cloudTimer = 0;
        }
        if (PlusCounter < PlusCount)
        {
            PlusTimer += Time.deltaTime;
            if (PlusTimer > PlusTime*PlusCounter)
            {
                PlusTimer = 0;
                GameSpeed += PlusGameSpeed;
                StageSpeed += PlusSpeed;
                PlusCounter++;
            }
        }
        Time.timeScale=GameSpeed;
    }
    private void Cloud()
    {
        GameObject Cloud_prefab = Resources.Load<GameObject>("Cloud");
        float randY = Random.Range(minY, MaxY);
        GameObject Cloud = Instantiate(Cloud_prefab, new Vector3(startX, randY, 0), Quaternion.identity);
        Debug.Log(Cloud);
    }
}
