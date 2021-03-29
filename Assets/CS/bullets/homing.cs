using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homing : BasicBullet
{
    public GameObject _playerMannager;
    PlayerManagger _playerMannagersqript;
    public float s;
    public GameObject _target;
    Enemy _sqript;

    // Start is called before the first frame update
    void Awake()
    {
        _playerMannager = GameObject.Find("PlayerManagger");
        _playerMannagersqript = _playerMannager.GetComponent<PlayerManagger>();
        _target = _playerMannagersqript._target;
        _sqript = _target.GetComponent<Enemy>();
        base.Awake();
        name = "homing";
    }

    public override void SpeedCalculation() // ターゲットに向かって追尾させる
    {
        //目標が消える時に先に消える
        if ((_sqript.breakable & 0b_001) == 0b_001)
            Destroy(this.gameObject);

        Vector3 tar = _target.transform.position;
        tar -= transform.position;
        tar = tar.normalized * s;
        Speed = tar;
    }

    public override void DirCalculation() //おかしくなるから保留
    {
    }
}
