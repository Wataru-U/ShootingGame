using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagger : MonoBehaviour
{
    public GameObject _scoreManagger;
    ScoreManagger _scoreManaggerSqript;
    // Start is called before the first frame update
    void Start()
    {
        _scoreManagger = GameObject.Find("ScoreManagger");
        _scoreManaggerSqript = _scoreManagger.GetComponent<ScoreManagger>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((_scoreManaggerSqript.SceneChange & 0b_01) == 0b_01)
        {
            SceneManager.LoadScene("ScoreScene");
        }
    }
}