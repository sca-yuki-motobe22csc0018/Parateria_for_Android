using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
public class CSVLording : MonoBehaviour
{
    [SerializeField] private Transform rankingPanel; // ランキングを表示する親オブジェクト（空オブジェクト "rank" の親）
    [SerializeField] private GameObject rank1; // rank1 オブジェクト
    [SerializeField] private GameObject rank2; // rank2 オブジェクト
    [SerializeField] private GameObject rank3; // rank3 オブジェクト
    [SerializeField] private GameObject rank4; // rank4 オブジェクト
    [SerializeField] private GameObject rank5; // rank5 オブジェクト
    [SerializeField] private GameObject rank6; // rank6 オブジェクト
    [SerializeField] private GameObject rank7; // rank7 オブジェクト
    [SerializeField] private GameObject rank8; // rank8 オブジェクト
    [SerializeField] private GameObject rank9; // rank9 オブジェクト
    [SerializeField] private GameObject rank10; // rank10 オブジェクト

    private List<(string name, int score)> rankingData = new List<(string, int)>(); // 名前とスコアのリスト

    void Start()
    {
        LoadCSV();
        SortRanking();
        DisplayRanking();
    }

    // **CSVを読み込む（B2セルから取得）**
    private void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("ranking"); // Resources フォルダの CSV をロード

        if (csvFile == null)
        {
            Debug.LogError("CSVファイルが Resources に見つかりません！ ファイル名を確認してください。");
            return;
        }

        StringReader reader = new StringReader(csvFile.text);
        bool isFirstLine = true; // 1行目をスキップするフラグ

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込む
            if (isFirstLine)
            {
                isFirstLine = false; // 1行目（ヘッダー）はスキップ
                continue;
            }

            string[] data = line.Split(','); // カンマ区切りで分割
            if (data.Length >= 2 && int.TryParse(data[1], out int score)) // B列（2列目）のスコアを取得
            {
                rankingData.Add((data[0], score)); // 名前とスコアをリストに追加
            }
        }
    }

    // **スコア順にソート**
    private void SortRanking()
    {
        rankingData = rankingData.OrderByDescending(entry => entry.score).ToList();
    }

    // **各行に対応する "rank" を個別に表示**
    private void DisplayRanking()
    {
        // 1位〜10位の順位を順位オブジェクトにセット
        if (rankingData.Count > 0)
        {
            SetRankData(rank1, rankingData[0]);
        }
        if (rankingData.Count > 1)
        {
            SetRankData(rank2, rankingData[1]);
        }
        if (rankingData.Count > 2)
        {
            SetRankData(rank3, rankingData[2]);
        }
        if (rankingData.Count > 3)
        {
            SetRankData(rank4, rankingData[3]);
        }
        if (rankingData.Count > 4)
        {
            SetRankData(rank5, rankingData[4]);
        }
        if (rankingData.Count > 5)
        {
            SetRankData(rank6, rankingData[5]);
        }
        if (rankingData.Count > 6)
        {
            SetRankData(rank7, rankingData[6]);
        }
        if (rankingData.Count > 7)
        {
            SetRankData(rank8, rankingData[7]);
        }
        if (rankingData.Count > 8)
        {
            SetRankData(rank9, rankingData[8]);
        }
        if (rankingData.Count > 9)
        {
            SetRankData(rank10, rankingData[9]);
        }
    }

    // **個別のrankに名前とスコアをセットする関数**
    private void SetRankData(GameObject rankObject, (string name, int score) data)
    {
        Text nameText = rankObject.transform.Find("NameText").GetComponent<Text>();  // 名前テキスト
        Text scoreText = rankObject.transform.Find("ScoreText").GetComponent<Text>(); // スコアテキスト

        // **CSVのデータをセット**
        nameText.text = data.name;
        scoreText.text = $"{data.score}";
    }
}
