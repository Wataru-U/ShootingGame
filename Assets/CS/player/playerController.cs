using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 主に操作に関わること

public class playerController : MonoBehaviour
{
    public GameObject _bulletManagger;
    public GameObject _use;
    BulletManagger _bulletManaSqript;
    float interval;
    bool shot = false;

    public bool auto = true;

    public float LV => Mathf.Abs(Input.GetAxis("Vertical")) < 0.1f ? 0 : Input.GetAxis("Vertical") / 10;
    public float RH => Input.GetAxisRaw("RightStickHori") * (-1.5f);　//Rawだと整数値しかとらない
    public float Dir = 0;

    // Start is called before the first frame update
    void Start()
    {
        _bulletManaSqript = _bulletManagger.GetComponent<BulletManagger>();
        Dir = -90;
    }

    // Update is called once per frame
    void Update()
    {
        _use = _bulletManaSqript.UseBullet;
        transform.Translate(Vector3.down * LV);
        SetDir();
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * 0.2f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * 0.2f);
        }

        shot = false;
        if (Input.GetButton("L2") || auto)
        {
            interval -= Time.deltaTime;
            if (interval <= 0)
            {
                interval = 0.2f;
                shot = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || shot == true)
        {
            Instantiate(_use, transform.position, Quaternion.identity);
        }
    }


    void SetDir()
    {

        if (Input.GetKey(KeyCode.W))
            Dir += 2;
        if (Input.GetKey(KeyCode.S))
            Dir -= 2;

        Dir += RH;

        Dir = Mathf.Clamp(Dir, -180, 0);
        var dirRad = Dir * Mathf.Deg2Rad;
        var d = new Vector3(1 * Mathf.Cos(dirRad + Mathf.PI / 2), 1 * Mathf.Sin(dirRad + Mathf.PI / 2), 0);
        var pos = transform.position;
        Debug.DrawLine(pos + d, pos, Color.red);
    }
}
