using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// 名前直すのがめんどくさいのでそのまま

public class Targer : MonoBehaviour
{
    public GameObject _playerMannager;
    PlayerManagger _playerMannagerSqript;
    public GameObject _bulletManagerObj;
    BulletManagger _BMSqript;
    // Start is called before the first frame update
    void Start()
    {
        _playerMannager = transform.parent.gameObject; ;
        _playerMannagerSqript = _playerMannager.GetComponent<PlayerManagger>();
        _BMSqript = _bulletManagerObj.GetComponent<BulletManagger>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 depth = new Vector3(0, 0, 0.5f);
        transform.position = _playerMannagerSqript._target.transform.position + depth;
        ChangeColor();
    }

    void ChangeColor()
    {
        Color32 color = new Color32(0, 240, 0, 255);
        if (_playerMannagerSqript.Lock == 0b_01)
        {
            color = new Color32(30, 30, 255, 255);
        }
        GetComponent<Renderer>().material.color = color;
    }
}
