using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPanel : MonoBehaviour
{
    // 決められたボタンが押された時
    int input => Input.GetButtonDown("R2") == true ? 1
                : Input.GetKeyDown(KeyCode.A) == true ? 1
                : 0;

    // inputによって変わる
    // visibleのビットが立っているかどうかでも変わる
    int visible = 0;

    float alpha = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (input == 1)
        {
            visible = (visible & 0b_01) == 0b_01 ? visible & ~0b_01
                    : visible | 0b_01;
        }
        if ((visible & 0b_01) == 0b_01)
        {
            alpha += 0.05f;
        }
        else
        {
            alpha -= 0.025f;
        }

        alpha = Mathf.Clamp(alpha, 0, 0.6f);
        Color col = this.GetComponent<Image>().color;
        col.a = alpha;
        this.GetComponent<Image>().color = col;

    }
}
