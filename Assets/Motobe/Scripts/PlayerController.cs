using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    //Rigidbody
    private Rigidbody2D rb;

    //位置関係
    public float DefaultPosition;
    public float PlayerSpeed;
    public float EndPositionY;
    //デバック用
    public float StartPositionY;

    //ジャンプ関連
    public float JumpForce;
    public int MaxJumpCount;
    private int thisJumpCount;
    private bool onWall;
    public string StageTag;
    public string WallTag;
    public string GiriJumpTag;
    public string GiriGiriJumpTag;
    private bool Jump;
    private bool GiriGiri;
    private float JumpCoolTime = 0.1f;
    private float JumpCoolTimer;
    public GameObject JumpButton;
    public int[] GiriScoreSet;
    private int GiriScore;

    //見た目関連
    public GameObject[] PlayerSkin;
    public GameObject[] PlayerIcon;
    public int num;
    public static int charaNum;
    public SpriteRenderer[] evaluation;
    public static int st;
    //public string LinePrefab;

    //敵との判定等
    public string EnemyTag;
    private bool DamageTrigger;
    public SpriteRenderer DamageEffectPlayer;
    public SpriteRenderer DamageEffect;
    public float DamageTime;
    public float DamageColor;

    //HP関連
    private int thisHP;
    public int StartHP;
    public int MAXHP;
    public GameObject[] HPObject;
    public string HealTag;
    public int[] HealScoreSet;
    private int HealScore;

    //フィーバー関連
    public static bool fever;
    private int feverCount;
    public int feverMax;
    float feverTimer;
    public GameObject feverBack;

    public AudioClip Nice;
    public AudioClip Excellent;
    public AudioClip HealSound;
    AudioSource audioSource;


    public enum State
    {
        run=1,
        jump=2,
        die=3
    }
    public State state;
    //public GameObject[] feverCountObj;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        feverBack.SetActive(false);
        feverTimer = 0;
        state = State.run;
        charaNum = Locator<PlayerData>.Instance.charaNumber;
        fever = false;
        feverCount = 0;
        onWall = false;
        rb = GetComponent<Rigidbody2D>();
        Jump = false;
        GiriGiri = false;
        JumpCoolTimer = 0;
        DamageTrigger = true;
        thisHP=StartHP;
        for (int i = 0; i < MAXHP; i++)
        {
            HPObject[i].SetActive(false);
        }

        for (int i = 0; i < 3; i++)
        {
            PlayerSkin[i].SetActive(false);
            PlayerIcon[i].SetActive(false);
        }
        PlayerSkin[charaNum].SetActive(true);
        PlayerIcon[charaNum].SetActive(true);
        GiriScore = GiriScoreSet[charaNum];
        HealScore = HealScoreSet[charaNum];
        if (charaNum==2)
        {
            MaxJumpCount++;
        }

        for (int i=0;i<thisHP;i++)
        {
            HPObject[i].SetActive(true);
        }
        for(int i=0; i < 2; i++)
        {
            var eva = DOTween.Sequence();
            eva.Append(evaluation[i].DOFade(0, 0));
        }
        var sequence = DOTween.Sequence();
        sequence.Append(DamageEffect.DOFade(1, 0));
        sequence.Append(DamageEffect.DOFade(0, DamageTime*8));
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.run)
        {
            st = 1;
        }
        if (state == State.jump)
        {
            st = 2;
        }
        if (state == State.die)
        {
            st = 3;
        }
        if (!GameController.gameEnd)
        { 
            if (Jump)
            {
                JumpCoolTimer += Time.deltaTime;
                if (JumpCoolTimer > JumpCoolTime)
                {
                    JumpCoolTimer = 0;
                    Jump = false;
                    DamageTrigger = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpAction();
            }
            if (feverCount >= feverMax)
            {
                fever = true;
            }
            if (fever)
            {
                feverTimer += Time.deltaTime;
                feverBack.SetActive(true);
            }
            if (feverTimer > 15)
            {
                fever = false;
                feverBack.SetActive(false);
                feverTimer = 0;
                feverCount = 0;
            }
            if (this.transform.position.x < DefaultPosition)
            {
                this.transform.position += new Vector3(PlayerSpeed * Time.deltaTime, 0, 0);
            }
            if (this.transform.position.x > DefaultPosition)
            {
                this.transform.position -= new Vector3(PlayerSpeed * Time.deltaTime, 0, 0);
            }
            if (this.transform.position.y < EndPositionY)
            {
                for (int i = 0; i < MAXHP; i++)
                {
                    HPObject[i].SetActive(false);
                }
                thisHP = 0;
                Damage();
            }
            if (thisHP <= 0)
            {
                GameController.gameEnd = true;
                state = State.die;
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(StageTag)&&!onWall)
        {
            thisJumpCount = 0;
            state = State.run;
            Jump = false;
        }
        if (collision.gameObject.CompareTag(StageTag) && onWall)
        {
            for (int i = 0; i < MAXHP; i++)
            {
                HPObject[i].SetActive(false);
            }
            thisHP = 0;
            Damage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GiriJumpTag) && Jump)
        {
            if (GiriGiri)
            {
                if (!fever)
                {
                    feverCount++;
                }
                if (ScoreCounter.nowScore<999999999)
                {
                    ScoreCounter.nowScore += GiriScore * ScoreCounter.plusScore;
                    if (fever&&charaNum==0)
                    {
                        ScoreCounter.nowScore += GiriScore * ScoreCounter.plusScore;
                    }
                }
                var eva = DOTween.Sequence();
                eva.Append(evaluation[1].DOFade(1, 0.2f));
                eva.Append(evaluation[1].DOFade(1, 0.25f));
                eva.Append(evaluation[1].DOFade(0, 0.2f));
                audioSource.PlayOneShot(Excellent);
            }
            else
            {
                if (ScoreCounter.nowScore < 999999999)
                {
                    ScoreCounter.nowScore += 4 / GiriScore * ScoreCounter.plusScore;
                    if (fever && charaNum == 0)
                    {
                        ScoreCounter.nowScore += GiriScore * ScoreCounter.plusScore;
                    }
                }
                var eva = DOTween.Sequence();
                eva.Append(evaluation[0].DOFade(1, 0.2f));
                eva.Append(evaluation[0].DOFade(1, 0.25f));
                eva.Append(evaluation[0].DOFade(0, 0.2f));
                audioSource.PlayOneShot(Nice);
            }
            DamageTrigger = false;
            Destroy(collision.gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(HealTag))
        {
            Heal();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag(GiriGiriJumpTag))
        {
            GiriGiri = true;
        }
        if (collision.gameObject.CompareTag(WallTag))
        {
            onWall = true;
        }
        if (collision.gameObject.CompareTag(EnemyTag))
        {
            if (DamageTrigger)
            {
                if(fever)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    Damage();
                    Destroy(collision.gameObject);
                }
                
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(StageTag) && !onWall)
        {
            thisJumpCount = 0;
        }
        if (collision.gameObject.CompareTag(WallTag))
        {
            onWall = false;
        }
        if (collision.gameObject.CompareTag(GiriGiriJumpTag))
        {
            GiriGiri = false;
        }
    }
    public void JumpAction()
    {
        if (thisJumpCount < MaxJumpCount)
        {
            if (!GameController.gameEnd)
            {
                rb.velocity = new Vector3(0, JumpForce, 0);
                thisJumpCount++;
                Jump = true;
                state = State.jump;
            }
        }
    }

    void Damage()
    {
        thisHP--;
        if (thisHP >= 0)
        {
            HPObject[thisHP].SetActive(false);
        }

        Handheld.Vibrate();
        var sequence = DOTween.Sequence();
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(0, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(0, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffectPlayer.DOFade(0, DamageTime));
    }

    void Heal()
    {
        if (thisHP < MAXHP)
        {
            HPObject[thisHP].SetActive(true);
            thisHP++;
            
            if (charaNum == 1&& thisHP < MAXHP)
            {
                HPObject[thisHP].SetActive(true);
                thisHP++;
            }
        }
        if (ScoreCounter.nowScore < 999999999)
        {
            ScoreCounter.nowScore += HealScore * ScoreCounter.plusScore;
            if (fever && charaNum == 1)
            {
                ScoreCounter.nowScore += HealScore * ScoreCounter.plusScore; 
                ScoreCounter.nowScore += HealScore * ScoreCounter.plusScore;
            }
        }
        audioSource.PlayOneShot(HealSound);
    }
}


