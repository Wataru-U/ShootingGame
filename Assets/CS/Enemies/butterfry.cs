using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butterfry : Enemy
{
    public GameObject _ui;
    Score _scoreSqript;
    float deg = 0;
    Vector3 s;
    // Start is called before the first frame update
    public override void Awake()
    {
        _ui = GameObject.Find("UI");
        _scoreSqript = _ui.GetComponent<Score>();
        HP = 5;
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void Move()
    {

        s.y = Mathf.Cos(Mathf.Deg2Rad * deg) * 0.04f;
        s.y += Mathf.Cos(Mathf.Deg2Rad * deg * 10) * 0.01f;
        s.x = -0.01f;
        Speed = s;
        base.Move();
        deg++;

    }
    public override void StatusChenged() { }

    public override void Des()
    {
        if (breakable > 2)
        {
            if (HP <= 0)
                _scoreSqript.score += 100;
            Destroy(this.gameObject);
            _script.brokenEnemyCount++; // 消えたことを報告
        }
    }
}
