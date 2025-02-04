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
    public Button button;

    //見た目関連
    public GameObject PlayerSkin;
    public float RotaSpeed;
    private bool Rota;
    public string LinePrefab;

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

    //フィーバー関連
    private bool fever;
    private int feverCount;
    public int feverMax;
    //public GameObject[] feverCountObj;

    // Start is called before the first frame update
    void Start()
    {
        fever = false;
        feverCount = 0;
        onWall = false;
        Rota=true;
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
        for (int i=0;i<thisHP;i++)
        {
            HPObject[i].SetActive(true);
        }
        button.onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Update()
    {
        if (Jump)
        {
            JumpCoolTimer += Time.deltaTime;
            if (JumpCoolTimer>JumpCoolTime)
            {
                JumpCoolTimer = 0;
                Jump = false;
                DamageTrigger=true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpAction();
        }
        if (feverCount>=feverMax)
        {
            fever = true;
        }
        if (fever)
        {
            //fever = false;
            //feverCount = 0;
        }
        if (this.transform.position.x < DefaultPosition)
        {
            this.transform.position += new Vector3(PlayerSpeed * Time.deltaTime, 0, 0);
        }
        if (this.transform.position.x > DefaultPosition)
        {
            this.transform.position -= new Vector3(PlayerSpeed * Time.deltaTime, 0, 0);
        }
        if (Rota)
        {
            PlayerSkin.transform.Rotate(0,0,-RotaSpeed/Time.timeScale*Time.deltaTime);
        }
        if (this.transform.position.y < EndPositionY)
        {
            this.transform.position+=new Vector3(0,StartPositionY,0);
        }
        Line();

    }

    private void Line()
    {
        GameObject Stage_prefab = Resources.Load<GameObject>(LinePrefab);
        GameObject Stage = Instantiate(Stage_prefab, this.transform.position, Quaternion.identity);
        return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(StageTag)&&!onWall)
        {
            thisJumpCount = 0;
            PlayerSkin.transform.rotation = Quaternion.identity;
            Rota = false;
            Jump = false;
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

                    ScoreCounter.nowScore += 50 * ScoreCounter.plusScore;
                }
                Debug.Log(feverCount);
            }
            else
            {
                Debug.Log("ギリ");
                if (ScoreCounter.nowScore < 999999999)
                {

                    ScoreCounter.nowScore += 10 * ScoreCounter.plusScore;
                }
            }
            DamageTrigger = false;
            Destroy(collision.gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
                Damage();
                Destroy(collision.gameObject);
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
            rb.velocity = new Vector3(0, JumpForce, 0);
            thisJumpCount++;
            Rota = true;
            Jump = true;
        }
    }

    void Damage()
    {
        thisHP--;
        if (thisHP > 0)
        {
            HPObject[thisHP].SetActive(false);
        }
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
        }
    }

    public void Click()
    {
        JumpAction();
    }
}
