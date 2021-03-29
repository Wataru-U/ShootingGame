using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string name; //エネミータグの中で何か見分けるため
    public GameObject _enemyManagger;
    public GameObject _player;
    public EnemyManagger _script;

    private Vector3 pos => transform.position;
    private float maxHP;

    private float hp = 70;
    public float HP
    {
        get { return hp; }
        set { hp = (float)value; }
    }
    public float hpPer => hp / maxHP;

    public Vector3 Speed { get; set; }
    private Color32 color = new Color32(255, 255, 255, 255);
    public int stastusFlag;

    public int breakable = 0;

    public virtual void Awake()
    {
        breakable = 0;
        _player = GameObject.Find("蜂の巣");
        Vector3 playerPos = _player.transform.position;
        Speed = (playerPos - pos).normalized / 10;
        maxHP = hp;
    }



    // Start is called before the first frame update
    public virtual void Start()
    {
        _enemyManagger = GameObject.Find("EnemiesManagger");
        _script = _enemyManagger.GetComponent<EnemyManagger>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Move();
        objDestroy();
        StatusChenged();
    }

    public virtual void Move()
    {
        transform.position += Speed;
    }

    public virtual void objDestroy()
    {
        //壊して良くなったら少しだけ待つ
        //その間にホーミングを壊す
        //そうしないと行き場をなくしたホーミングがエラーを吐く。
        Des();
        if ((breakable & 0b_1) == 0)
        {
            if (hp <= 0)
            {
                _script.brokenEnemyCount += 2;// カウント＋1
                breakable |= 0b_01;
            }
            if (pos.x * pos.x + pos.y * pos.y > 100)
                breakable |= 0b_01;
        }
    }
    public virtual void Des()
    {
        if ((breakable & 0b_101) == 0b_101)
        {
            Debug.Log(breakable);
            Destroy(this.gameObject);
            _script.brokenEnemyCount++; // 消えたことを報告
        }
    }
    public virtual void OnHit(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            hp--;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        OnHit(collider);
    }

    public virtual void StatusChenged() // プレイヤーと同じ
    {
        if ((stastusFlag & 0b_001) == 0b_001)
        {
            GetComponent<Renderer>().material.color = color;
            stastusFlag--;
            stastusFlag <<= 1;
        }
        if (hpPer > 0.5f)
        { stastusFlag |= 0b_010; }
        if (hpPer < 0.5f && hpPer > 0.25f)
        {
            color = new Color32(255, 255, 0, 255);
            if ((stastusFlag & 0b_100) == 0b_000)
            { stastusFlag |= 0b_001; }
        }
        if (hpPer < 0.25f)
        {
            color = new Color32(255, 0, 0, 255);
            if ((stastusFlag & 0b_1_000) == 0b_0_000)
            { stastusFlag |= 0b_001; }
        }
    }
}
