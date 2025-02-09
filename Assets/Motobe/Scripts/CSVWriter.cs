using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    void Start()
    {
        // Androidでも書き込み可能なフォルダのパスを取得
        string filePath = Path.Combine(Application.persistentDataPath, "saved_data.csv");

        // CSVデータを作成
        string[] lines = { "名前,スコア", "プレイヤー1,100", "プレイヤー2,200" };

        // ファイルに書き込む
        File.WriteAllLines(filePath, lines);
        Debug.Log("CSVを保存しました: " + filePath);
    }
}