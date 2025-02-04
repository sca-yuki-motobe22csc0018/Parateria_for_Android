using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // �ړ����x
    [SerializeField] private float stopY = -2f; // ��~����Y���W

    void Update()
    {
        // �w��̈ʒu�ɓ��B����܂ňړ�
        if (transform.position.y > stopY)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}