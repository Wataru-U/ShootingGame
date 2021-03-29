using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaderController : MonoBehaviour
{
    public int numBoids;
    public GameObject boidPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numBoids; i++)
        {
            Instantiate(boidPrefab);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.zero;
        pos.x = 4 * Mathf.Cos(Time.time * 2);
        pos.y = 4 * Mathf.Sin(Time.time * 2);
        transform.position = pos;
    }
}
