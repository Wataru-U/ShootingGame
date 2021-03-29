using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回りながら進む

// Positon(t) = Rotate(Dir) * (S(t) + r( up + cir(t,Dir)))
// P'(t) = Rotate(Dir) * (S'(t) + r/T(cir'(t,Dir))) dt

public class mawaru : BasicBullet
{
    public float s;
    public float r;
    public float T;
    // Awake is called before the first frame update
    void Awake()
    {
        SpeedAbs = s;
        base.Awake();
    }

    public override void SpeedCalculation()
    {
        base.SpeedCalculation();
        Vector3 DTDP = new Vector3(0, 0, 0);
        Vector3 d = new Vector3(Mathf.Cos(Direction), Mathf.Cos(Direction), 0);
        float Theta = Time.time / T - Direction;
        DTDP.x = Mathf.Cos(Theta);
        DTDP.y = Mathf.Sin(Theta);
        DTDP *= r / T;
        Speed = Speed + DTDP;
    }

    // Update is called once per frame


}
