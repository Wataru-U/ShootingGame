using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Awakeposから　半径を大きくしながら回転する

//Position(t) = r(t)Cos(theata(t))
//P.x'(t) = r'(t)Cos(Theta(t)) + r(t)sin(Theta(t))/T dt
//P.y'(t) =      Sin           -     cos
public class kaiten : BasicBullet
{
    public float T; //周期
    public float r; //半径の変化率
    float time;
    int Dir;
    // Awake is called before the first frame update
    void Awake()
    {
        base.Awake();
        time = 0;
        Dir = Input.GetButton("R2") == false ? -1 : 1;
    }

    // Update is called once per frame
    public override void SpeedCalculation()
    {
        norSp_1 = Speed;
        time += 0.01f;
        float t = time / T;
        float Theta = (t + DirDeg) * Dir;

        Vector3 DTDP = new Vector3(0, 0, 0);
        DTDP.x = Mathf.Cos(Theta) + t * Mathf.Sin(Theta);
        DTDP.y = Mathf.Sin(Theta) - t * Mathf.Cos(Theta);
        DTDP *= r;
        Speed = DTDP;
    }
}
