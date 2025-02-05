using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputfield : MonoBehaviour
{
    public InputField inputField;
    public Text text;
    public Image targetImage;  // 表示させたい Image コンポーネント

    void Start()
    {
        text.text = "";  // 初期状態を空に設定
        targetImage.gameObject.SetActive(false);  // 最初は非表示
    }

    public void OnButtonClick()
    {
        text.text = inputField.text;  // 入力フィールドの内容をテキストに表示
        targetImage.gameObject.SetActive(true);  // Image を表示
    }
}