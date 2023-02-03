using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;

public class GrowthParameterManager : SingletonMonobehavior<GrowthParameterManager>
{
    [SerializeField, ReadOnly, EnumIndex(typeof(PlayerGrowthParameters.GrowthType))]
    private int[] _growthParameters = new int[4];
    public int[] GrowthParameters { get => _growthParameters; }

    public void UploadParameter(int[] param)
    {
        // 全てのパラメータを上書きする(仮置き)
        for (int i = 0; i < _growthParameters.Length; i++)
        {
            _growthParameters[i] = param[i];
        }
    }
}
