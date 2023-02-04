using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class LevelLoaderConvertSettings : ScriptableObject
{
    [SerializeField]
    private ConvertPairData[] _datas;
    public ConvertPairData[] Datas => _datas;

}


[Serializable]
public class ConvertPairData
{
    public string Key;
    public GameObject Prefab;
    
}