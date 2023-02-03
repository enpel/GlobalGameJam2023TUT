﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;

public class PlayerGrowthParameters : MonoBehaviour
{
    public enum GrowthType
    { 
        Penetration,
        Growth,
        Beauty,
        Absorption,
    }

    [SerializeField, ReadOnly, EnumIndex(typeof(GrowthType))]
    private int[] _growthParameters = new int[4];

    /// <summary> 貫通力 </summary>
    public int PenetrationgPower { get => _growthParameters[(int)GrowthType.Penetration]; }

    /// <summary> 成長速度 </summary>
    public int GrowthSpeed { get => _growthParameters[(int)GrowthType.Growth]; }

    /// <summary> 美力 </summary>
    public int BeautyPower { get => _growthParameters[(int)GrowthType.Beauty]; }

    /// <summary> 吸収力 </summary>
    public int AbsorptionPower { get => _growthParameters[(int)GrowthType.Absorption]; }

    private void Awake()
    {
        SettingParameter();
    }

    public void SetParameter(GrowthType type, int value)
    {
        _growthParameters[(int)type] += value;
    }

    public void UploadParameter()
    {
        GrowthParameterManager.Instance.UploadParameter(_growthParameters);
    }

    private void SettingParameter()
    {
        var param = GrowthParameterManager.Instance.GrowthParameters;

        for (int i = 0; i < _growthParameters.Length; i++)
        {
            _growthParameters[i] = param[i];
        }
    }
}
