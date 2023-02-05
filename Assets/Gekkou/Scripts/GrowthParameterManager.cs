using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;

public class GrowthParameterManager : SingletonMonobehavior<GrowthParameterManager>
{
    [SerializeField, ReadOnly, EnumIndex(typeof(PlayerGrowthParameters.GrowthType))]
    private int[] _growthParameters = new int[4];
    public int[] GrowthParameters { get => _growthParameters; }

    private int[] _currentGrowthParameters = new int[4];
    public int[] CurrentGrowthParameters { get => _currentGrowthParameters; }

    [SerializeField]
    private AnimationCurve _addRateCurve;

    private void Start()
    {
        for (int i = 0; i < _growthParameters.Length; i++)
        {
            _growthParameters[i] = SaveSystemManager.Instance.SaveData.seedParameters[i];
        }
    }

    public void UploadParameter(int[] param)
    {
        // 今回のパラメータを保存
        for (int i = 0; i < _growthParameters.Length; i++)
        {
            _currentGrowthParameters[i] = param[i];
        }
    }

    /// <summary>
    /// 栄養素の上書き保存
    /// </summary>
    public void UpdateGrowthParameter()
    {
        // パラメータを美力のRate分掛けて加算する
        var addDouble = _addRateCurve.Evaluate(_currentGrowthParameters[(int)PlayerGrowthParameters.GrowthType.Beauty]);

        for (int i = 0; i < _growthParameters.Length; i++)
        {
            // 増えた量を計算
            var diff = _currentGrowthParameters[i] - _growthParameters[i];

            // 増えた量*増加割合　を加算
            _growthParameters[i] += Mathf.CeilToInt(diff * addDouble);
        }

        SaveSystemManager.Instance.Saving(_growthParameters);
    }
}
