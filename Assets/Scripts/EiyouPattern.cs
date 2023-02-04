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
        var parent = this.transform;
        // ランダムにパターンを選出
        Instantiate(patterns.GetRandom(), parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
