using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletList : MonoBehaviour
{
    int interval;
    bool Rotation = false;
    public int deg = 4;
    public int degIntevral;
    public int frame => 120 / deg;
    int dir;
    // Start is called before the first frame update
    void Start()
    {
        degIntevral = frame;
    }

    // Update is called once per frame
    void Update()
    {
        int value = (int)Input.GetAxisRaw("CrossAxis");
        // キーボード割り当て
        int keyArrow = (Input.GetKey(KeyCode.LeftArrow) == true) ? -1
                     : (Input.GetKey(KeyCode.RightArrow) == true) ? 1
                     : 0;
        value += keyArrow;
        // ここがないと押されている間ずっと変わる >> 目的のものにならない
        // 十字キーが押されていない時１つめ、　右で2つ目、　左で3つ目　のビットが立つ
        interval |= (value == 0) ? 0b_001
                    : (value == 1) ? 0b_010
                    : 0b_100;
        int c = 0;
        // 現在何個ビットが立っているか調べる
        for (int i = 0; i < 3; i++)
            if ((interval & (1 << i)) == 1 << i)
                c++;

        //bitを1つに直す
        interval &= (value == 0) ? 0b_001
                 : (value == 1) ? 0b_010
                 : 0b_100;
        if (c != 1 && value != 0)
        {
            Rotation = true;
            dir = value;
        }
        if (Rotation == true)
        {
            transform.Rotate(0, 0, dir * deg);
            degIntevral--;
        }
        if (degIntevral == 0)
        {
            degIntevral = frame;
            Rotation = false;
            dir = 0;
        }
    }
}
