using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;

public class EiyouPattern : MonoBehaviour
{
    // パターンの枠
    [SerializeField] GameObject[] patterns;
    void Start()
    {
        // ランダムにパターンを選出
        Instantiate(patterns.GetRandom());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
