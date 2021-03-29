using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asobikata : MonoBehaviour
{
    int input => Input.GetButtonDown("R2") == true ? 1
                : Input.GetKeyDown(KeyCode.A) == true ? 1
                : 0;

    // inputによって変わる
    // visibleのビットが立っているかどうかでも変わる
    int visible = 0;
    int page = 0b_01;
    int interval;
    int count = 17;
    int dir = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Show();
        ChangePage();
        Debug.Log(page);
    }

    void Show()
    {
        Vector3 pos = transform.position;
        if (input == 1)
        {
            visible = (visible & 0b_01) == 0b_01 ? visible & ~0b_01
                    : visible | 0b_01;
            if (visible == 0)
                page = 0b_01;
        }
        if ((visible & 0b_01) == 0b_01)
        {
            pos.z = 80;
        }
        else
        {
            pos.z = 90;
        }
        transform.position = pos;
    }
    void ChangePage()
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
            if ((page & 0b_01) != 0b_01 && value == -1)
            {
                page >>= 1;
                dir = -value;
            }
            if ((page & 0b_01 << 6) != 0b_01 << 6 && value == 1)
            {
                page <<= 1;
                dir = -value;
            }
        }
        if (count != 0 && dir != 0)
        {
            count--;
            Vector3 vel = new Vector3(1 * dir, 0, 0);
            transform.position += vel;
        }
        else
        {
            count = 17;
            dir = 0;
            float x = -17.33f * (flag(page, 7) - 3);
            Vector3 pos = transform.position;
            pos.x = x;
            transform.position = pos;
        }
    }

    int flag(int f, int num)
    {
        int result = 0;
        for (int i = 0; i < num; i++)
        {
            result = (f & 1 << i) == 1 << i ? i : 0;
            if (result != 0)
                break;
        }
        return result;
    }
}
