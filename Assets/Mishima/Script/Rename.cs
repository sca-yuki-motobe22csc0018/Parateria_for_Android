using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rename : MonoBehaviour
{
    public InputField inputField;  // 名前を入力するフィールド
    public Text text;  // 名前を表示するテキスト
    public GameObject confirmPanel;  // YES/NO ボタンがある確認パネル
    public GameObject rankingPanel;  // ランキングを表示するパネル
    public GameObject nextPanel;  // YES を押したときに表示する別パネル
    public GameObject parentPanel;  // 親パネル（例: Panel_NameSpace）
    public GameObject imageDisplay;  // 名前を表示する Image_Display パネル
    public GameObject imageWaku;  // 名前表示用の Image_waku パネル

    private bool isInputFinished = false;  // 入力が完了したかどうかを判定するフラグ

    void Start()
    {
        confirmPanel.SetActive(false);  // 最初は非表示
        nextPanel.SetActive(false);  // YES を押すまで非表示
        rankingPanel.SetActive(false);  // ランキングパネルも非表示
        imageDisplay.SetActive(false);  // 名前を表示するパネルも最初は非表示
        imageWaku.SetActive(false);  // Image_waku パネルも最初は非表示
        parentPanel.SetActive(true);  // 親パネルは最初に表示
        inputField.onEndEdit.AddListener(OnInputEndEdit);  // 入力が完了した時のリスナーを追加
    }

    // 入力が終了したときの処理
    private void OnInputEndEdit(string text)
    {
        isInputFinished = true;  // 入力が完了したフラグを立てる
    }

    // 名前入力後に確認パネルを表示
    public void OnSubmit()
    {
        if (!isInputFinished) return;  // 入力が完了していない場合は処理しない
        text.text = inputField.text;  // 入力内容を表示
        StartCoroutine(ShowConfirmPanel());  // 確認パネルを表示するコルーチンを開始
        imageDisplay.SetActive(true);  // 名前を表示する Image_Display パネルを表示
        imageWaku.SetActive(true);  // 名前表示用の Image_waku パネルを表示
    }

    // コルーチンでボタンが押された後、少し待機してから確認パネルを表示
    private IEnumerator ShowConfirmPanel()
    {
        yield return new WaitForSeconds(0.5f);  // 0.5秒待機（必要に応じて調整）
        confirmPanel.SetActive(true);  // 確認パネルを表示
    }

    // NO を押した場合 → 入力画面に戻る
    public void OnNoButtonClick()
    {
        inputField.text = "";  // 入力フィールドをリセット
        confirmPanel.SetActive(false);  // 確認パネルを非表示
        imageDisplay.SetActive(false);  // Image_Display パネルを非表示
        imageWaku.SetActive(false);  // Image_waku パネルも非表示
        isInputFinished = false;  // 入力完了フラグをリセット
    }

    // YES を押した場合 → ランキングパネルを表示
    public void OnYesButtonClick()
    {
        confirmPanel.SetActive(false);  // 確認パネルを非表示
        nextPanel.SetActive(false);  // 他のパネルが表示されていたら非表示
        parentPanel.SetActive(false);  // 親パネル（Panel_NameSpaceなど）を非表示
        rankingPanel.SetActive(true);  // ランキングパネルを表示
    }
}