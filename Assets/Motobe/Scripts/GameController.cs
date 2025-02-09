using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    float feverTimer;
    float startX = 30;
    float MaxY = 11;
    float minY = 0;
    float FMaxY = 9;
    float FminY = -8;
    public static bool gameEnd;
    public Material sharedMat;
    byte A = 0;
    byte R = 255;
    byte G = 0;
    byte B = 0;
    Color newColor; // Ý’è‚µ‚½‚¢F
    // public GameObject JumpButton;
    // Start is called before the first frame update
    void Start()
    {
        PlusTimer = 0;
        PlusCounter = 0;
        cloudTimer = 0;
        feverTimer = 0;
        gameEnd = false;
        if (PlayerController.charaNum == 2)
        {
            StageSpeed -= 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd)
        {
            cloudTimer += Time.deltaTime;
            if (cloudTimer > 2.0f)
            {
                Cloud();
                cloudTimer = 0;
            }
            if (PlayerController.fever)
            {
                feverTimer += Time.deltaTime;
                if (feverTimer > 0.25f)
                {
                    Fever();
                    feverTimer = 0;
                }
            }

            if (PlusCounter < PlusCount)
            {
                PlusTimer += Time.deltaTime;
                if (PlusTimer > PlusTime * PlusCounter)
                {
                    PlusTimer = 0;
                    GameSpeed += PlusGameSpeed;
                    StageSpeed += PlusSpeed;
                    PlusCounter++;
                }
            }
            Time.timeScale = GameSpeed;
        }
        
    }
    private void Cloud()
    {
        GameObject Cloud_prefab = Resources.Load<GameObject>("Cloud");
        float randY = Random.Range(minY, MaxY);
        GameObject Cloud = Instantiate(Cloud_prefab, new Vector3(startX, randY, 0), Quaternion.identity);
        Debug.Log(Cloud);
    }
    private void Fever()
    {
        GameObject Fever_prefab = Resources.Load<GameObject>("Fever");
        float randY = Random.Range(FminY, FMaxY);
        GameObject Fever = Instantiate(Fever_prefab, new Vector3(startX, randY, 0), Quaternion.identity);
        Debug.Log(Fever);
    }
}
