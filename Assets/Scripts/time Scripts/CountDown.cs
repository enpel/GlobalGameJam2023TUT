using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public GameObject count_object = null;
    public float countdownSeconds = 100;
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
    }
}
