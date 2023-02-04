using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EiyouPattern : MonoBehaviour
{
    // パターンの枠
    [SerializeField] GameObject pattern1;
    [SerializeField] GameObject pattern2;
    [SerializeField] GameObject pattern3;
    [SerializeField] GameObject pattern4;
    [SerializeField] GameObject pattern5;

    void Start()
    {
        // ランダムにパターンを選出
        int rnd = Random.Range(1, 10);
        Debug.Log(rnd);
        if(rnd <= 5)
        {
            Instantiate(pattern1);
        }
        else if(rnd >= 6)
        {
            Instantiate(pattern2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
