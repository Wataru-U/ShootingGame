using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//シーン遷移しても残しておく
public class ScoreManagger : MonoBehaviour
{
    public GameObject _hive;
    public GameObject _ui;
    public int SceneChange = 0;
    public int thisScore;
    public int[] Scores = new int[5];
    int SceneFlag = 0;
    // Start is called before the first frame update
    void Start()
    {
        SceneChange = 0;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            // シーンがサンプルになった時だけ
            if ((SceneFlag & 0b_001) == 0b_000)
            {
                _hive = GameObject.Find("蜂の巣");
                _ui = GameObject.Find("UI");
                SceneFlag |= 0b_001;
                SceneChange &= ~0b_001;
                Debug.Log(_hive.GetComponent<hive>().hiveHp);
            }


            thisScore = _ui.GetComponent<Score>().score;
            if (_hive.GetComponent<hive>().hiveHp <= 0)
            {
                thisScore *= (int)_ui.GetComponent<TimeSqript>().time;
                updateScore(thisScore);
                SceneChange |= 0b_01;
            }
        }
        else if ((SceneFlag & 0b_001) == 0b_001)
        {
            SceneFlag ^= 0b_001;
        }
    }

    // 過去のトップ５より大きかったらならべかえる
    void updateScore(int score)
    {
        for (int i = 0; i < 5; i++)
        {
            if (Scores[i] < score)
            {
                if (i == 4)
                    Scores[4] = score;
                else
                {
                    for (int j = 4; j > i; j--)
                    {
                        Scores[j] = Scores[j - 1];
                    }
                    Scores[i] = score;
                }
                break;
            }
        }
    }
}
