using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 蜂の巣
// HP管理と　色の変更(ダメージを受けた時)
public class hive : MonoBehaviour
{
    public int hiveHp = 10;
    public int hiveMaxHp;
    public float hiveHpPer => (float)hiveHp / (float)hiveMaxHp;
    private Color32 color = new Color(255, 255, 0, 255);
    // Start is called before the first frame update
    void Start()
    {
        hiveMaxHp = hiveHp;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = color;
        color = new Color(255, 255, 0, 255);
    }

    public void OnHit(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            if (collider.gameObject.GetComponent<Enemy>().name == "Bullet")
            {
                hiveHp--;
                collider.gameObject.GetComponent<Enemy>().breakable++;
                color = new Color32(255, 0, 0, 255);
            }
            if (collider.gameObject.GetComponent<Enemy>().name == "kuma")
            {
                hiveHp = 0;
                collider.gameObject.GetComponent<Enemy>().breakable++;
                color = new Color32(255, 0, 0, 255);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        OnHit(collider);
    }

}
