using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 発射時の角度に合わせて曲率が変わる
// speed.y >= 0

public class magaru : BasicBullet
{
    public float Dir;
    public float s;


    // Awake is called before the first frame update
    void Awake()
    {
        SpeedAbs = s;
        base.Awake();
        Dir = DirDeg;
    }
    void Update()
    {


        base.Update();
    }

    // Update is called once per frame


}
