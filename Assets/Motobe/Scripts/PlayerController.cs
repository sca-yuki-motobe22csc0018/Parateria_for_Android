using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    //Rigidbody
    private Rigidbody2D rb;

    //ˆÊ’uŠÖŒW
    public float DefaultPosition;
    public float PlayerSpeed;
    public float EndPositionY;
    //ƒfƒoƒbƒN—p
    public float StartPositionY;

    //ƒWƒƒƒ“ƒvŠÖ˜A
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

    //Œ©‚½–ÚŠÖ˜A
    public GameObject PlayerSkin;
    public float RotaSpeed;
    private bool Rota;
    public string LinePrefab;

    //“G‚Æ‚Ì”»’è“™
    public string EnemyTag;
    private bool DamageTrigger;
    public SpriteRenderer DamageEffect;
    public float DamageTime;
    public float DamageColor;

    // Start is called before the first frame update
    void Start()
    {
        onWall = false;
        Rota=true;
        rb = GetComponent<Rigidbody2D>();
        Jump = false;
        GiriGiri = false;
        JumpCoolTimer = 0;
        DamageTrigger = true;
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
            PlayerSkin.transform.Rotate(0,0,-RotaSpeed*Time.deltaTime);
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
                Debug.Log("ƒMƒŠƒMƒŠ");
            }
            else
            {
                Debug.Log("ƒMƒŠ");
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
        var sequence = DOTween.Sequence();
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
    }
}
