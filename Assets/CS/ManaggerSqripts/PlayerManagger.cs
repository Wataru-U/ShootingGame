using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//　主に外とのやりとり
// どれを狙うか決める
// 敵の弾を狙うとバグが起きやすいのでこうほから外した
// 多分HPが少なすぎて壊れても狙って見つからなくて起きる
public class PlayerManagger : MonoBehaviour
{
    public GameObject _enemiesManagger;
    public GameObject _player;
    QueueVector3 _queueVec3;
    public GameObject _target;
    private GameObject _targetReserve;
    public int targetNum;
    public int Lock = 0;
    public GameObject[] ememies;
    Enemy _enemySqript;
    // Start is called before the first frame update


    // 
    void Start()
    {
        _enemiesManagger = GameObject.Find("EnemiesManagger");
        _player = GameObject.Find("player");
        setTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Lockon();
        ChangeTarget();
        setTarget();
    }

    void setTarget()　// targetのゲームオブジェクトをセット
    {
        //取り出しが一回なので
        //優先度付きのキューより標準のソートの方が早い
        _queueVec3 = new QueueVector3(200, _player.transform.position);
        ememies = GameObject.FindGameObjectsWithTag("Enemy")
                        .Where(x => (x.GetComponent<Enemy>().breakable & ~0b_0) == 0)
                        .Where(x => x.GetComponent<Enemy>().name != "Bullet")
                        .ToArray();

        foreach (var item in ememies)
        {
            _enemySqript = item.GetComponent<Enemy>();
            _queueVec3.Add(item.transform.position);
        }

        // ここだけchangetargetにかかる
        if (targetNum < 0)
            targetNum = ememies.Length - 1;
        if (targetNum >= ememies.Length)
            targetNum = 0;

        for (int i = 0; i <= targetNum; i++) //何番目に近いのを取るか
            _targetReserve = ememies[_queueVec3.PullNum()];
        if (_targetReserve == null)
            _targetReserve = ememies[_queueVec3.PullNum()];


        if ((Lock & 0b_01) == 0 || _target == null)
        {
            _target = _targetReserve;
        }
    }
    void ChangeTarget() // ターゲットの距離がなばんめか
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("L1"))
            targetNum--;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown("R2"))
            targetNum++;

    }

    void Lockon()
    {
        if (Input.GetButtonDown("R2") || Input.GetKeyDown(KeyCode.L))
        {
            Lock ^= 0b_01;
        }
        if ((_target.GetComponent<Enemy>().breakable & 0b_01) == 0b_01)
            Lock &= ~0b_1;
    }
}
