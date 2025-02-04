using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // 移動速度
    [SerializeField] private float stopY = -2f; // 停止するY座標

    void Update()
    {
        // 指定の位置に到達するまで移動
        if (transform.position.y > stopY)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}