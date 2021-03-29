using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// position += speed　のため　位置を変えるために　DP/DT が必要

public class BasicBullet : MonoBehaviour
{
    public string name; //どの弾か知る必要かある時設定
    public GameObject _player;
    playerController _script;
    // direction,speedabs,
    private Vector3 position;　//現在地
    public Vector3 Position
    {
        get { return position; }
    }
    private Vector3 startPos; //射出位置
    public Vector3 StartPos
    {
        get { return startPos; }
    }
    private float direction; //deg2radする
    public float Direction
    {
        get { return direction + halfPI; }
        set { direction = (float)value * Mathf.Deg2Rad; }
    }
    public float DirDeg
    {
        get { return direction * Mathf.Rad2Deg; }
    }
    private float speedAbs = 0.1f;　//速度
    public float SpeedAbs
    {
        get { return speedAbs; }
        set { speedAbs = (float)value; }
    }
    private Vector3 speed; //pos += するための
    private Vector3 sp_1; //1フレーム前の正規SPVec
    public Vector3 norSp_1
    {
        set { sp_1 = Vector3.Normalize(value); }
    }
    public Vector3 Speed
    {
        get { return speed; }
        set { speed = value; }
    }


    // Start is called before the first frame update
    public virtual void Awake()
    {
        name = "";
        _player = GameObject.Find("player");
        _script = _player.GetComponent<playerController>();
        Direction = _script.Dir;
        
        startPos = transform.position;
        position = startPos;
        SpeedCalculation();
        transform.Rotate(new Vector3(0, 0, direction * Mathf.Rad2Deg));
        OnShoot();
    }


    // Update is called once per frame
    public virtual void Update()
    {
        Move();
        transform.position = position;
        DirCalculation();
        objDestroy();
    }

    public virtual void Move()
    {
        SpeedCalculation();
        position += speed;
    }

    public virtual void SpeedCalculation()
    {
        norSp_1 = Speed;
        speed.x = speedAbs * Mathf.Cos(Direction);
        speed.y = speedAbs * Mathf.Sin(Direction);
    }

    // １フレーム前とSpeedが違うなら、向きを変える
    // speedの１フレーム前と内積をとって　arccos(dot)
    // ここに時間がかかった。
    // 動きに合わせて向きが変わるから継承先で変える必要がない
    public virtual void DirCalculation()
    {
        if (sp_1 != Vector3.Normalize(speed))
        {
            Vector3 nsp = Vector3.Normalize(Speed);
            float DotSP = Vector3.Dot(sp_1, nsp);
            float dd_dt = Mathf.Acos(DotSP) * Mathf.Rad2Deg;
            if ((sp_1.x < nsp.x && sp_1.y > 0 && nsp.y > 0) || (sp_1.x > nsp.x && sp_1.y < 0 && nsp.y < 0))　//正規化された sp(t).x>sp(t-1f).x の時回転角は -
            { dd_dt *= -1; }
            transform.Rotate(new Vector3(0, 0, dd_dt));
        }
    }

    public virtual void objDestroy()
    {
        float x, y;
        x = transform.position.x;
        y = transform.position.y;
        x *= x;
        y *= y;

        if (x + y > 100)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void OnHit(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player" && collider.gameObject.tag != "Bullet")
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        OnHit(collider);
    }

    public virtual void OnShoot()
    { }

    public const float halfPI = Mathf.PI / 2;

}
