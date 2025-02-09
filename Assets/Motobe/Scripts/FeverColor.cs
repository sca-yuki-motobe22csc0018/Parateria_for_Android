using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverColor : MonoBehaviour
{
    public  SpriteRenderer mr;
    byte A = 0;
    byte R = 255;
    byte G = 0;
    byte B = 0;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        A = 255;
        mr.material.color = new Color32(255, 255, 255, 255);
        mr.material.color = mr.material.color - new Color32(0, 255, 255, 155);
    }

    // Update is called once per frame
    void Update()
    {
        mr.material.color = new Color32(R, G, B, A);
        if (R == 255)
        {
            if (G < 255)
            {
                G += 1;
            }
        }
        if (G == 255)
        {
            if (R > 0)
            {
                R -= 1;
            }
        }
        if (R == 0)
        {
            if (B < 255)
            {
                B += 1;
            }
        }
        if (B == 255)
        {
            if (G > 0)
            {
                G -= 1;
            }
        }
        if (G == 0)
        {
            if (R < 255)
            {
                R += 1;
            }
        }
        if (R == 255)
        {
            if (B > 0)
            {
                B -= 1;
            }
        }
    }
}
