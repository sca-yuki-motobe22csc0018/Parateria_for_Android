using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    void Start()
    {
        // Android�ł��������݉\�ȃt�H���_�̃p�X���擾
        string filePath = Path.Combine(Application.persistentDataPath, "saved_data.csv");

        // CSV�f�[�^���쐬
        string[] lines = { "���O,�X�R�A", "�v���C���[1,100", "�v���C���[2,200" };

        // �t�@�C���ɏ�������
        File.WriteAllLines(filePath, lines);
        Debug.Log("CSV��ۑ����܂���: " + filePath);
    }
}