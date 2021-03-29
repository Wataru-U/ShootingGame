using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵を生成するところ
public class EnemyManagger : MonoBehaviour
{
    public GameObject _time;
    public GameObject _enemy;
    public GameObject _butterflyPrefab;
    public GameObject _kumaPrefab;
    float timeleft;
    public int suzumeCount;
    public GameObject _suzumePrefab;
    // 壊されたエネミーの数x2 
    public int brokenEnemyCount;
    public int beCount => brokenEnemyCount / 2; // 破壊されたエネミーの数を返す
    bool once = true;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Count();
    }

    public void Count()
    {
        timeleft -= Time.deltaTime;
        if (timeleft <= 0)
        {
            timeleft = 5;
            int ran = Random.Range(0, 100);
            if (ran < 10)
                _enemy = _kumaPrefab;
            else
                _enemy = _butterflyPrefab;
            Instantiate(_enemy, new Vector3(5, Random.Range(-4, 5), 0), Quaternion.identity, this.transform);
        }

        if (_time.GetComponent<TimeSqript>().time >= 20 && once == true)
        {
            once = false;
            Instantiate(_suzumePrefab, new Vector3(Random.Range(4, 7), Random.Range(-4, 5), 0), Quaternion.identity, this.transform);
        }


        if ((suzumeCount & 0b_01) == 1)
        {
            suzumeCount--;
            Instantiate(_suzumePrefab, new Vector3(Random.Range(4, 7), Random.Range(-4, 5), 0), Quaternion.identity, this.transform);
            Instantiate(_suzumePrefab, new Vector3(Random.Range(4, 7), Random.Range(-4, 5), 0), Quaternion.identity, this.transform);
            Instantiate(_suzumePrefab, new Vector3(Random.Range(4, 7), Random.Range(-4, 5), 0), Quaternion.identity, this.transform);
        }
    }
}
