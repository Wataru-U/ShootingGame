using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class suzume : Enemy
{
    public GameObject _bullet;
    private Vector3 pos => transform.position;
    private QueueVector3 _queue;
    private Enemy _baseScript;
    Vector3 vel;
    Vector3 acc;
    float followSpeed = 1;
    public float maxSpeed = 1;

    private float timeleft;
    private float interval;
    // Start is called before the first frame update
    public override void Awake()
    {
        name = "suzume";
        HP = 20;
        base.Awake();
        interval = Random.Range(1.2f, 3.0f);
    }

    public override void Update()
    {
        // 個性に合わせた間隔で弾を発射
        timeleft -= Time.deltaTime;
        if (timeleft <= 0)
        {
            timeleft = interval;
            Instantiate(_bullet, transform.position - new Vector3(0.5f, 1, 0), Quaternion.identity, this.transform);
        }
        base.Update();

    }

    public override void Move()  //ボイドとほぼ同じ　leaderの代わりは(5,0,0)
    {
        //なるべく蜂の中央に寄せつつ、他のと近すぎると少し遠ざかるようにした。
        _queue = new QueueVector3(100, transform.position);

        //tagは1つしかないのでEnemyクラスごとにつけたnameをLinqを使って選定
        var boids = GameObject.FindGameObjectsWithTag("Enemy").
                    Where(x => x.GetComponent<Enemy>().name == "suzume").
                    ToArray();
        Speed = Vector3.zero;
        //1体の時は動かない
        if (boids.Length > 1)
        {
            Vector3 dir = new Vector3(5, 0, 0) - transform.position;
            //０ベクトルだと正規化できない
            dir = (dir == Vector3.zero) ? dir : dir.normalized;
            acc = dir * followSpeed;

            //優先とつきキューでソートしているがここでは最適じゃなかった
            //しかし扱うデータ数が少ないので無視した
            foreach (var item in boids)
                if (item.transform.position != transform.position)
                    _queue.Add(item.transform.position);

            for (int i = 0; i < boids.Length; i++)
            {
                Vector2 pullDis = _queue.PullDis();
                Vector3 a = boids[(int)pullDis.y].transform.position - transform.position;
                if (pullDis.x > 16)
                {
                    break;
                }
                //分離
                else
                {
                    // 数が少ない時は感覚を増やす
                    float correction = ((boids.Length < 10) ? 30 / boids.Length : 1);
                    // 距離が0だと割れずにエラーが出る
                    acc += -a.normalized / ((pullDis.x == 0) ? 1 : pullDis.x) * (correction);
                }
            }
            //自分以外の重心と離れていたら近づく　結合
            if (_queue.AverageDis > 16)
                acc += (_queue.Average - transform.position) / 10;

            vel += acc;
            Debug.DrawLine(transform.position, transform.position + acc / 8, Color.cyan);
            //Debug.DrawLine(transform.position, transform.position + vel / 8, Color.green);

            float speed = Mathf.Clamp(vel.magnitude, 0, maxSpeed);
            vel = (vel == Vector3.zero) ? vel : vel.normalized * speed;
            Speed = vel * Time.deltaTime;
        }
        base.Move();
    }
    public override void objDestroy()
    {
        if ((breakable & 0b_01) == 0 && HP <= 0)
        {
            base._script.suzumeCount++; // 消えたことを報告
        }
        base.objDestroy();
    }
    public override void StatusChenged()
    {
        Color32 col = new Color32();
        if ((base.stastusFlag & 0b_001) == 0b_001)
        {
            col = new Color32(255, 255, 100, 255);
            GetComponent<Renderer>().material.color = col;
            base.stastusFlag--;
            base.stastusFlag <<= 1;
        }
        if (hpPer > 0.5f)
        { base.stastusFlag |= 0b_010; }
        if (hpPer < 0.5f && hpPer > 0.25f)
        {
            col = new Color32(255, 255, 0, 255);
            GetComponent<Renderer>().material.color = col;
            if ((base.stastusFlag & 0b_100) == 0b_000)
            { base.stastusFlag |= 0b_001; }
        }
        if (hpPer < 0.25f)
        {
            col = new Color32(255, 0, 0, 255);
            GetComponent<Renderer>().material.color = col;
            if ((base.stastusFlag & 0b_1_000) == 0b_0_000)
            { base.stastusFlag |= 0b_001; }
        }

    }
}
