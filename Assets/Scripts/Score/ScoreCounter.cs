using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] scoreDigits;
    [SerializeField]
    private float rotationTime;
    [SerializeField]
    public static int nowScore;
    private float scoreCounter;
    private int previousScore;
    public SpriteRenderer DamageEffect;
    public int SetScore { set { nowScore = value; } }
    float stageTimer;
    public int plusScoreDefault;
    public static int plusScore;
    public int PlusCount;
    private int PlusCounter;
    public int PlusTime;
    private bool end;
    // Start is called before the first frame update
    void Start()
    {
        end = false;
        nowScore = 0;
        PlusCounter = 0;
        stageTimer = 0;
        plusScore = plusScoreDefault;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!GameController.gameEnd)
        {
            stageTimer += Time.deltaTime;
            if (stageTimer > PlusTime)
            {
                if (PlusCounter < PlusCount)
                {
                    plusScore *= 2;
                    PlusCounter++;
                }
                stageTimer = 0;
            }
            if (nowScore < 999999999)
            {
                nowScore += (int)(plusScore * Time.deltaTime);
            }
            if (nowScore > 999999999)
            {
                nowScore = 999999999;
            }
            if (nowScore == (int)scoreCounter)
            {
                previousScore = nowScore;
            }
            else
            {
                int difference = nowScore - previousScore;
                scoreCounter += difference * Time.deltaTime / rotationTime;

                if (difference > 0)
                {
                    if (scoreCounter > nowScore)
                    {
                        scoreCounter = nowScore;
                    }
                }
                else
                {
                    if (scoreCounter < nowScore)
                    {
                        scoreCounter = nowScore;
                    }
                }

                if (nowScore == scoreCounter)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        int a = (nowScore / (int)Mathf.Pow(10, i)) % 10;
                        scoreDigits[i].anchoredPosition = new Vector2(0, a * 100);
                    }
                    if (scoreCounter < 100000000)
                    {
                        scoreDigits[8].anchoredPosition = new Vector2(0, -200);
                    }
                    if (scoreCounter < 10000000)
                    {
                        scoreDigits[7].anchoredPosition = new Vector2(0, -200);
                    }
                    if (scoreCounter < 1000000)
                    {
                        scoreDigits[6].anchoredPosition = new Vector2(0, -200);
                    }
                    if (scoreCounter < 100000)
                    {
                        scoreDigits[5].anchoredPosition = new Vector2(0, -200);
                    }
                    if (scoreCounter < 10000)
                    {
                        scoreDigits[4].anchoredPosition = new Vector2(0, -200);
                    }
                    if (scoreCounter < 1000)
                    {
                        scoreDigits[3].anchoredPosition = new Vector2(0, -200);
                    }
                    if (scoreCounter < 100)
                    {
                        scoreDigits[2].anchoredPosition = new Vector2(0, -200);
                    }
                    if (scoreCounter < 10)
                    {
                        scoreDigits[1].anchoredPosition = new Vector2(0, -200);
                    }
                    return;
                }
                for (int i = 0; i < 9; i++)
                {
                    scoreDigits[i].anchoredPosition = new Vector2(0, (scoreCounter / (int)Mathf.Pow(10, i)) % 10 * 100);
                }
                if (scoreCounter < 100000000)
                {
                    scoreDigits[8].anchoredPosition = new Vector2(0, -200);
                }
                if (scoreCounter < 10000000)
                {
                    scoreDigits[7].anchoredPosition = new Vector2(0, -200);
                }
                if (scoreCounter < 1000000)
                {
                    scoreDigits[6].anchoredPosition = new Vector2(0, -200);
                }
                if (scoreCounter < 100000)
                {
                    scoreDigits[5].anchoredPosition = new Vector2(0, -200);
                }
                if (scoreCounter < 10000)
                {
                    scoreDigits[4].anchoredPosition = new Vector2(0, -200);
                }
                if (scoreCounter < 1000)
                {
                    scoreDigits[3].anchoredPosition = new Vector2(0, -200);
                }
                if (scoreCounter < 100)
                {
                    scoreDigits[2].anchoredPosition = new Vector2(0, -200);
                }
                if (scoreCounter < 10)
                {
                    scoreDigits[1].anchoredPosition = new Vector2(0, -200);
                }
            }
        }
        else
        {
            if (!end)
            {
                Dead();
                end = true;
            }
        }
    }

    void Dead()
    {
        Locator<PlayerData>.Instance.SetScore(nowScore);
        var sequence = DOTween.Sequence();
        sequence.Append(DamageEffect.DOFade(0, 0.5f));
        sequence.Append(DamageEffect.DOFade(1, 1.0f));
        sequence.AppendCallback(() => SceneChange());
    }

    void SceneChange()
    {
        SceneManager.LoadScene("Ranking");
    }
}
