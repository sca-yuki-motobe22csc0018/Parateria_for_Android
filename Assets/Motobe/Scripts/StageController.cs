using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public float speed;
    public float spawnPointX;
    public float spawnPointY;
    public float spawnPointZ;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }

    private void Stage00(float x, float y, float z)
    {
        GameObject Stage_prefab = Resources.Load<GameObject>("Stage00");
        GameObject Stage = Instantiate(Stage_prefab, new Vector3(x, y, z), Quaternion.identity);
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyObject"))
        {
            Stage00(spawnPointX, spawnPointY, spawnPointZ);
            Destroy(this.gameObject);
        }
    }
}
