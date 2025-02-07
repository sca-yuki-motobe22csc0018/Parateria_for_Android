using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class PlayerData : MonoBehaviour
{
    public int charaNumber { get; private set;}
    public int score { get; private set; }

    const int rankings = 10;

    private void OnEnable() => Locator<PlayerData>.Bind(this);
    private void OnDisable() => Locator<PlayerData>.Unbind(this);

    void Awake()
    {

    }

    
    public void SetCharaNumber(int _num)
    {
        charaNumber = _num;
    }

    public void SetScore(int _score)
    {
        score = _score;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
