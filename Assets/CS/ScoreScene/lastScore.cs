using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lastScore : MonoBehaviour
{
    public GameObject _this;
    Text _thisText;
    public GameObject _scoreManagger;
    // Start is called before the first frame update
    void Start()
    {
        _this = GameObject.Find("lastScore");
        _thisText = _this.GetComponent<Text>();
        _scoreManagger = GameObject.Find("ScoreManagger");
        _thisText.text = _scoreManagger.GetComponent<ScoreManagger>().thisScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
