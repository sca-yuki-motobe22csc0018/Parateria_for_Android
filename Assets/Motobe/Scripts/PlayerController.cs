using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Rigidbody
    private Rigidbody2D rb;

    //�ʒu�֌W
    public float DefaultPosition;
    public float PlayerSpeed;
    public float EndPositionY;
    //�f�o�b�N�p
    public float StartPositionY;

    //�W�����v�֘A
    public float JumpForce;
    public int MaxJumpCount;
    private int thisJumpCount;
    private bool onWall;

    //�����ڊ֘A
    public GameObject PlayerSkin;
    public float RotaSpeed;
    private bool Rota;

    // Start is called before the first frame update
    void Start()
    {
        onWall = false;
        Rota=true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && thisJumpCount < MaxJumpCount)
        {
            rb.velocity = new Vector3(0, JumpForce, 0);
            thisJumpCount++;
            Rota = true;
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
        GameObject Stage_prefab = Resources.Load<GameObject>("Line");
        GameObject Stage = Instantiate(Stage_prefab, this.transform.position, Quaternion.identity);
        return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stage")&&!onWall)
        {
            thisJumpCount = 0;
            PlayerSkin.transform.rotation = Quaternion.identity;
            Rota = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            onWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stage") && !onWall)
        {
            thisJumpCount = 0;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            onWall = false;
        }
    }
}
