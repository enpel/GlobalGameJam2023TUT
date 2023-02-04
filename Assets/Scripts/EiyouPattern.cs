using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EiyouPattern : MonoBehaviour
{
    [SerializeField] GameObject pattern1;
    [SerializeField] GameObject pattern2;

    void Start()
    {
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
