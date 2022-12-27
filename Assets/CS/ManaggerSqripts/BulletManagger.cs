using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//どの弾を撃つか決めるところ
public class BulletManagger : MonoBehaviour
{
    private int num;
    public int bulletNum;
    public GameObject massugu;
    public GameObject mawaru;
    public GameObject homing;
    public GameObject[] Bullets;
    public GameObject UseBullet;
    public GameObject BulletList;
    BulletList _BulletListSqript;
    private int interval;
    private int changeInterval;
    private bool isSetedBullets;
    // Start is called before the first frame update
    void Awake()
    {
        _BulletListSqript = BulletList.GetComponent<BulletList>();
        num = 3;
        bulletNum = 0;
        Bullets = new GameObject[num];
        isSetedBullets = false;
    }

    void Start()
    {
        SetBullets();
    }

    // Update is called once per frame
    void Update()
    {
        SetBullets();
        ChangeBullet();
    }

    private void SetBullets(){
        if(!isSetedBullets){
            Bullets = new GameObject[num];
            Bullets[0] = massugu;
            Bullets[1] = homing;
            Bullets[2] = mawaru;
            UseBullet = Bullets[0];
            isSetedBullets = true;
        }
    }

    public void ChangeBullet()
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
        // 複数立ってる　＝＝　入れ替わった瞬間
        for (int i = 0; i < 3; i++)
            if ((interval & (1 << i)) == 1 << i)
                c++;

        // 絵を回転する間は変えない
        if (c >= 2 && changeInterval == _BulletListSqript.frame)
        {
            changeInterval--;
            interval &= (value == 0) ? 0b_001
                    : (value == 1) ? 0b_010
                    : 0b_100;　//ビットを１につに直す
        }
        else if (changeInterval != _BulletListSqript.frame && changeInterval != 0)
        {
            changeInterval--;
        }
        else
        {
            changeInterval = _BulletListSqript.frame;
        }


        //複数ビットが立っている時変更
        //複数ビットが立っている時　＝＝　入力が変わった時
        if (changeInterval >= _BulletListSqript.frame - 1)
        {
            if ((c >= 2 && value == 1))
            {
                bulletNum++;
                if (bulletNum >= num)
                    bulletNum = 0;
                UseBullet = Bullets[bulletNum];
            }
            if ((c >= 2 && value == -1))
            {
                bulletNum--;
                if (bulletNum < 0)
                    bulletNum = num - 1;
                UseBullet = Bullets[bulletNum];
            }
        }

    }
}
