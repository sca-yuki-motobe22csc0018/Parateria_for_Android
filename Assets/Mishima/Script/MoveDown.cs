using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // 移動速度
    [SerializeField] private float stopY = -2f; // 停止するY座標

    void Update()
    {
        // 指定の位置に到達するまで移動
        if (transform.position.y > stopY)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            if(transform.position.y > stopY) AnimationSkip();
            //アニメーション終了時に呼ぶ
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
        //必要次第でフェード等追加
        SceneManager.LoadScene("Home");
    }
}