using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuma : Enemy
{
    private Vector3 s;
    private int Flag = 0;
    public override void Awake()
    {
        name = "kuma";
        s.x = Random.Range(-0.1f, -0.05f);
        s.y = Random.Range(-0.1f, 0.1f);
        s.z = 0;
        HP = 10000;
        Flag = 0;
        base.Awake();
    }
    public override void Move()
    {
        Speed = s;
        if ((Flag & 0b_10) == 0b_10)
        {
            GameObject _player = GameObject.Find("蜂の巣");
            Vector3 playerPos = _player.transform.position;
            Speed = (playerPos - transform.position).normalized / 10;
            if (Dis(transform.position, _player.transform.position) < 1)
            {
                _player.GetComponent<hive>().hiveHp = 0;
            }
        }
        base.Move();


    }

    public override void OnHit(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Debug.Log("hit");
            Flag |= 0b_11;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        OnHit(collider);
    }

    private float Dis(Vector3 p, Vector3 q)
    {
        p.x -= q.x;
        p.y -= q.y;
        p.x *= p.x;
        p.y *= p.y;
        return p.x + p.y;
    }
}
