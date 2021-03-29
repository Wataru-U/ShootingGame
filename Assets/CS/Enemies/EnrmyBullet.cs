using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnrmyBullet : Enemy
{
    public GameObject _ui;
    Score _scoreSqript;
    public override void Awake()
    {
        name = "Bullet";
        _ui = GameObject.Find("UI");
        _scoreSqript = _ui.GetComponent<Score>();
        HP = 1;
        base.Awake();
    }

    public override void OnHit(Collider2D collider)
    {
        base.OnHit(collider);
        if (collider.gameObject.tag == "Hive")
        {
            //壊れると宣言
            base.breakable |= 0b_01;
            Debug.Log(base.breakable);
        }
    }

    public override void Des()
    {
        if ((base.breakable & 0b_001) == 0b_001)
        {
            if (HP <= 0)
            { _scoreSqript.score += 1; }
            Destroy(this.gameObject);
            _script.brokenEnemyCount++; // 消えたことを報告
        }
    }
}
