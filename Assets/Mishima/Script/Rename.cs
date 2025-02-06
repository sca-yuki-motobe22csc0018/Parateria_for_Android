using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Rename : MonoBehaviour
{
    public InputField inputField;
    public Text text;
    public GameObject confirmPanel;
    public GameObject rankingPanel;
    public GameObject nextPanel;
    public GameObject parentPanel;
    public GameObject imageDisplay;

    private bool isInputFinished = false;
    private string filePath;  // CSV�̕ۑ���

    void Start()
    {
        confirmPanel.SetActive(false);
        nextPanel.SetActive(false);
        rankingPanel.SetActive(false);
        imageDisplay.SetActive(false);
        parentPanel.SetActive(true);
        inputField.onEndEdit.AddListener(OnInputEndEdit);

        // �ۑ��������i�������݉\�ȃp�X�j
        filePath = Path.Combine(Application.persistentDataPath, "ranking.csv");

        // CSV�t�@�C�����Ȃ���΃w�b�_�[���쐬
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "���O,�X�R�A\n");
        }
    }

    private void OnInputEndEdit(string text)
    {
        isInputFinished = true;
    }

    public void OnSubmit()
    {
        if (!isInputFinished) return;
        text.text = inputField.text;
        StartCoroutine(ShowConfirmPanel());
        imageDisplay.SetActive(true);
    }

    private IEnumerator ShowConfirmPanel()
    {
        yield return new WaitForSeconds(0.5f);
        confirmPanel.SetActive(true);
    }

    public void OnNoButtonClick()
    {
        inputField.text = "";
        confirmPanel.SetActive(false);
        imageDisplay.SetActive(false);
        isInputFinished = false;
    }

    public void OnYesButtonClick()
    {
        confirmPanel.SetActive(false);
        nextPanel.SetActive(false);
        parentPanel.SetActive(false);
        rankingPanel.SetActive(true);

        // CSV�ɖ��O��ۑ�
        SaveToCSV(inputField.text);
    }

    private void SaveToCSV(string playerName)
    {
        // ���̃X�R�A�� 0 �Ƃ��ĕۑ�
        string newEntry = $"{playerName},0\n";
        File.AppendAllText(filePath, newEntry);
    }
}