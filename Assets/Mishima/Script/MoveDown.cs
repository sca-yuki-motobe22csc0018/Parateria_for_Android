using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
            if(transform.position.y > stopY) AnimationSkip();
            //�A�j���[�V�����I�����ɌĂ�
            //Locator<PlayerData>.Instance.ResetScore();
        }
    }

    public void AnimationSkip()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,2f);
        Locator<PlayerData>.Instance.ResetScore();
        StartCoroutine(SceneChangeAnim());
    }

    IEnumerator SceneChangeAnim()
    {
        yield return new WaitForSeconds(0.2f);
        //�K�v����Ńt�F�[�h���ǉ�
        SceneManager.LoadScene("Home");
    }
}