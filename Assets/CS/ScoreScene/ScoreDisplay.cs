using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public GameObject _scoreManagger;
    // Start is called before the first frame update
    void Start()
    {
        _scoreManagger = GameObject.Find("ScoreManagger");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
