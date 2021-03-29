using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// x成分にsin(t/T + pi/2)

// time = t;
// Positon.x(t) = s(t) * Cos(dir) - A Sin(dir)sin(t/T)
//        .y(t) = s(t) * Sin(dir) + A cos(dir)sin(t/T) 

// s'(t) = speedabs   //Position(t) == P(t)
// P.x'(t) = cos(dir)s'(t) - Asin(dir)/T cos(t/T)
// p.y'(t) = sin(dir)s'(t) + Acos(dir)/T cos(t/T)


public class yureru : BasicBullet
{
    // Awake is called before the first frame update
    public float s = 0.1f;
    public float sinpuku;
    public float T;
    Vector3 sin = new Vector3();

    void Awake()
    {
        SpeedAbs = s;
        base.Awake();
        transform.Rotate(new Vector3(0, 0, 45));
    }


    // Update is called once per frame

    public override void SpeedCalculation()
    {
        base.SpeedCalculation();
        float d = sinpuku * Mathf.Sin(Time.time / T + halfPI) / T;
        Vector3 DPDt = new Vector3(-Mathf.Sin(Direction) * d, Mathf.Cos(Direction) * d);
        Speed = Speed + DPDt;
    }

}
