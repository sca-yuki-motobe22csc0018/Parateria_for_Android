using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rename : MonoBehaviour
{
    public InputField inputField;  // ���O����͂���t�B�[���h
    public Text text;  // ���O��\������e�L�X�g
    public GameObject confirmPanel;  // YES/NO �{�^��������m�F�p�l��
    public GameObject rankingPanel;  // �����L���O��\������p�l��
    public GameObject nextPanel;  // YES ���������Ƃ��ɕ\������ʃp�l��
    public GameObject parentPanel;  // �e�p�l���i��: Panel_NameSpace�j
    public GameObject imageDisplay;  // ���O��\������ Image_Display �p�l��
    public GameObject imageWaku;  // ���O�\���p�� Image_waku �p�l��

    private bool isInputFinished = false;  // ���͂������������ǂ����𔻒肷��t���O

    void Start()
    {
        confirmPanel.SetActive(false);  // �ŏ��͔�\��
        nextPanel.SetActive(false);  // YES �������܂Ŕ�\��
        rankingPanel.SetActive(false);  // �����L���O�p�l������\��
        imageDisplay.SetActive(false);  // ���O��\������p�l�����ŏ��͔�\��
        imageWaku.SetActive(false);  // Image_waku �p�l�����ŏ��͔�\��
        parentPanel.SetActive(true);  // �e�p�l���͍ŏ��ɕ\��
        inputField.onEndEdit.AddListener(OnInputEndEdit);  // ���͂������������̃��X�i�[��ǉ�
    }

    // ���͂��I�������Ƃ��̏���
    private void OnInputEndEdit(string text)
    {
        isInputFinished = true;  // ���͂����������t���O�𗧂Ă�
    }

    // ���O���͌�Ɋm�F�p�l����\��
    public void OnSubmit()
    {
        if (!isInputFinished) return;  // ���͂��������Ă��Ȃ��ꍇ�͏������Ȃ�
        text.text = inputField.text;  // ���͓��e��\��
        StartCoroutine(ShowConfirmPanel());  // �m�F�p�l����\������R���[�`�����J�n
        imageDisplay.SetActive(true);  // ���O��\������ Image_Display �p�l����\��
        imageWaku.SetActive(true);  // ���O�\���p�� Image_waku �p�l����\��
    }

    // �R���[�`���Ń{�^���������ꂽ��A�����ҋ@���Ă���m�F�p�l����\��
    private IEnumerator ShowConfirmPanel()
    {
        yield return new WaitForSeconds(0.5f);  // 0.5�b�ҋ@�i�K�v�ɉ����Ē����j
        confirmPanel.SetActive(true);  // �m�F�p�l����\��
    }

    // NO ���������ꍇ �� ���͉�ʂɖ߂�
    public void OnNoButtonClick()
    {
        inputField.text = "";  // ���̓t�B�[���h�����Z�b�g
        confirmPanel.SetActive(false);  // �m�F�p�l�����\��
        imageDisplay.SetActive(false);  // Image_Display �p�l�����\��
        imageWaku.SetActive(false);  // Image_waku �p�l������\��
        isInputFinished = false;  // ���͊����t���O�����Z�b�g
    }

    // YES ���������ꍇ �� �����L���O�p�l����\��
    public void OnYesButtonClick()
    {
        confirmPanel.SetActive(false);  // �m�F�p�l�����\��
        nextPanel.SetActive(false);  // ���̃p�l�����\������Ă������\��
        parentPanel.SetActive(false);  // �e�p�l���iPanel_NameSpace�Ȃǁj���\��
        rankingPanel.SetActive(true);  // �����L���O�p�l����\��
    }
}