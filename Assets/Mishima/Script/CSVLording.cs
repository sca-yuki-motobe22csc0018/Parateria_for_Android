using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
public class CSVLording : MonoBehaviour
{
    [SerializeField] private GameObject[] rankObjects;  // 1〜10位のオブジェクト
    private List<(string name, int score)> rankingData = new List<(string, int)>();
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "ranking.csv");
        LoadCSV();
        SortRanking();
        DisplayRanking();
    }

    private void LoadCSV()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("CSVファイルが見つかりません！");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        bool isFirstLine = true;

        foreach (string line in lines)
        {
            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            }

            string[] data = line.Split(',');
            if (data.Length >= 2 && int.TryParse(data[1], out int score))
            {
                rankingData.Add((data[0], score));
            }
        }
    }

    private void SortRanking()
    {
        rankingData = rankingData.OrderByDescending(entry => entry.score).ToList();
    }

    private void DisplayRanking()
    {
        for (int i = 0; i < rankObjects.Length && i < rankingData.Count; i++)
        {
            SetRankData(rankObjects[i], rankingData[i]);
        }
    }

    private void SetRankData(GameObject rankObject, (string name, int score) data)
    {
        Text nameText = rankObject.transform.Find("NameText").GetComponent<Text>();
        Text scoreText = rankObject.transform.Find("ScoreText").GetComponent<Text>();

        nameText.text = data.name;
        scoreText.text = $"{data.score}";
    }
}