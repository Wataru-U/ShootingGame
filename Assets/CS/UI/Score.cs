using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public GameObject _scoreObj;
    Text _scoreText;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText = _scoreObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "point: " + score.ToString();
    }
}
