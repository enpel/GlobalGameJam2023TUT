using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gekkou;

public class CountDown : MonoBehaviour
{

    [SerializeField]
    GameObject count_object = null;

    [SerializeField]
    float countdownSeconds = 100;

    bool endFlag = false;

    void Start()
    {
        
    }
    
    void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        Text count_text = count_object.GetComponent<Text>();
        // カウントダウン
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
        }
        else
        {
            countdownSeconds = 0;
        }
        // テキストの中身
        count_text.text = countdownSeconds.ToString("f0");

        if (!endFlag && countdownSeconds <= 0)
        {
            SceneSystemManager.Instance.SceneLoading(Scene.ResultScene);
            endFlag = true;
        }
    }
}
