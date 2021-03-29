using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ここはゲームと関係なし
public class queue : MonoBehaviour
{
    QueueVector3 _Queue;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        _Queue = new QueueVector3(100, pos);
        for (int i = 0; i < 8; i++)
        {
            _Queue.Add(new Vector3(i, Random.Range(-10, 10), 0));
        }
        for (int i = 0; i < 8; i++)
        {
            Debug.Log(_Queue.Pull());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
