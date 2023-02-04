using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class LevelLoaderConvertSettings : ScriptableObject
{
    [SerializeField]
    private ConvertPairData[] _datas;
    public ConvertPairData[] Datas => _datas;

    public Dictionary<string, GameObject> GetPairDataDictionary()
    {
        return Datas.ToDictionary(x => x.Key, x => x.Prefab);
    }

}


[Serializable]
public class ConvertPairData
{
    public string Key;
    public GameObject Prefab;
    
}