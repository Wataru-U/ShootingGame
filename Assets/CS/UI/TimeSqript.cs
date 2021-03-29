using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSqript : MonoBehaviour
{
    public GameObject _scoreObj;
    Text _scoreText;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText = _scoreObj.GetComponent<Text>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        _scoreText.text = time.ToString();

    }
}
