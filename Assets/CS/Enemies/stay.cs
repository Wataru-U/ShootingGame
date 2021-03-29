using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stay : Enemy
{
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        Speed = Vector3.zero;
        float x = Random.Range(-3, 3);
        float y = Random.Range(0, 2);
        transform.position = new Vector3(x, y, 0);
    }
}
