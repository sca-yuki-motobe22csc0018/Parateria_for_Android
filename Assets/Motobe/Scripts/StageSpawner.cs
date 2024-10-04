using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private int spawnRandom;
    public int[] stageProbability;
    public int stageAmount;
    public float spawnPointX;
    public float spawnPointY;
    public float spawnPointZ;
    void Start()
    {
        for (int i = 0; i < stageAmount; i++)
        {
            spawnRandom += stageProbability[i];
        }
    }
    private void Stage(float x, float y, float z, int num)
    {
        GameObject Stage_prefab = Resources.Load<GameObject>("Stage0" + num);
        GameObject Stage = Instantiate(Stage_prefab, new Vector3(x, y, z), Quaternion.identity);
        return;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            int Rand = Random.Range(0, spawnRandom);
            for (int i = 0; i < stageAmount; i++)
            {
                Rand -= stageProbability[i];
                if (Rand < 0)
                {
                    Stage(spawnPointX, spawnPointY, spawnPointZ, i);
                    break;
                }
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
