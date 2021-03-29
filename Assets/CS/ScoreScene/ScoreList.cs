using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ベストスコアを出すところ
// 1つで複数のオブジェクトに対応


public class ScoreList : MonoBehaviour
{
    public GameObject _scoreManagger;
    public GameObject _thisObj;
    Text _thisText;
    string thisName;
    int num;
    // Start is called before the first frame update
    void Start()
    {
        //　オブジェクトの名前が Score + 番号　なので6番目の文字を見て何番目かわかる
        // char から int　がうまくいかなかったので一旦stringにしてから　int にした
        thisName = _thisObj.name;
        string str = "";
        str += thisName[5];
        num = int.Parse(str);

        _thisText = _thisObj.GetComponent<Text>();
        _scoreManagger = GameObject.Find("ScoreManagger");
        _thisText.text = _scoreManagger.GetComponent<ScoreManagger>().Scores[num].ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
