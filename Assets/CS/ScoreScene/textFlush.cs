using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textFlush : MonoBehaviour
{
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float alpha = (Mathf.Cos(time * 2) + 1) / 2;
        Color col = this.GetComponent<Text>().color;
        col.a = alpha;
        this.GetComponent<Text>().color = col;
    }
}