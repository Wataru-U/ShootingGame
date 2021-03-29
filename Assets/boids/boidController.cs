using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidController : MonoBehaviour
{
    GameObject leader;
    Vector3 vel;
    Vector3 acc;
    float followSpeed;
    public float maxSpeed;

    QueueVector3 _queue;
    // Start is called before the first frame update
    void Start()
    {
        leader = GameObject.Find("leader");
        followSpeed = Random.Range(10, 15);
        maxSpeed = Random.Range(7, 10);

        transform.position = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = leader.transform.position - transform.position;
        dir = dir.normalized;
        acc = dir * followSpeed;


        _queue = new QueueVector3(100, transform.position);

        var boids = GameObject.FindGameObjectsWithTag("boid");
        for (int i = 0; i < boids.Length; i++)
            if (boids[i].transform.position != transform.position)
                _queue.Add(boids[i].transform.position);


        //ここから
        for (int i = 0; i < boids.Length; i++)
        {
            Vector2 pullDis = _queue.PullDis();
            Vector3 a = boids[(int)pullDis.y].transform.position - transform.position;
            // 結合
            if (pullDis.x > 2)
            {
                if (i == 0) //１番近いのでroot2より大きく離れていたらそれに近づく
                    acc += a;
                break;
            }
            //分離
            if (pullDis.x < 2)
            {
                acc += -a.normalized / pullDis.x;
            }
        }
        //自分以外の重心と離れていたら近づく　結合
        if (_queue.AverageDis > 16)
            acc += (_queue.Average - transform.position) / 10;



        vel += acc * Time.deltaTime;
        //Debug.DrawLine(transform.position, transform.position + acc / 8, Color.cyan);
        //Debug.DrawLine(transform.position, transform.position + vel / 8, Color.green);

        float speed = Mathf.Clamp(vel.magnitude, 0, maxSpeed);
        vel = vel.normalized * speed;
        transform.position += vel * Time.deltaTime;
    }
}
