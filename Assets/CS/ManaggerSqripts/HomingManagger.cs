using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// ここでホーミング弾がどのオブジェクトを狙っているか確認する
// 壊れていいよって指令をここから出す
public class HomingManagger : MonoBehaviour
{
    GameObject _playerManagger;
    PlayerManagger _PMSqript;
    GameObject _PMtarget;
    // Start is called before the first frame update
    void Start()
    {
        _playerManagger = GameObject.Find("PlayerManagger");
        _PMSqript = _playerManagger.GetComponent<PlayerManagger>();
        _PMtarget = _PMSqript._target;
    }

    // Update is called once per frame
    void Update()
    {
        //whereでホーミングだけに絞って　　Selectでどのターゲットを所得
        var homingArray = GameObject.FindGameObjectsWithTag("Bullet")
                            .Where(x => x.GetComponent<BasicBullet>().name == "homing")
                            .Select(x => x.GetComponent<homing>()._target)
                            .ToArray();
        var enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

        //ゲーム中にホーミング弾がない時　＝＞　elseから
        if (homingArray.Length != 0)
        {

            int[] enemyBucket = new int[enemyArray.Length];
            foreach (var item in homingArray)
            {
                for (int i = 0; i < enemyArray.Length; i++)
                {
                    if (enemyArray[i] == item)
                    {
                        enemyBucket[i]++;
                        break;
                    }
                }
            }
            for (int i = 0; i < enemyArray.Length; i++)
            {
                var _sqript = enemyArray[i].GetComponent<Enemy>();
                //HPがぜろで狙っているオブジェクト(プレイヤー含む)がなければ壊れていいよ
                if ((_sqript.breakable & 0b_001) == 0b_001 && enemyBucket[i] == 0)
                {
                    bool b = true;
                    foreach (var item in _PMSqript.ememies)
                    {
                        if (item == enemyArray[i])
                            b = false;
                        break;
                    }
                    if (b == true)
                        _sqript.breakable |= (b == true) ? 0b_100 : 0;
                }
            }
        }
        else
        {
            for (int i = 0; i < enemyArray.Length; i++)
            {
                var _sqript = enemyArray[i].GetComponent<Enemy>();
                if ((_sqript.breakable & 0b_001) == 0b_001)
                    _sqript.breakable |= 0b_100;
            }
        }

    }
}
