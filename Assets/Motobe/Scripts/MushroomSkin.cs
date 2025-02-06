using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSkin : MonoBehaviour
{
    public GameObject[] skin;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            skin[i].SetActive(false);
        }
        int rand = Random.Range(0, 4);
        skin[rand].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
