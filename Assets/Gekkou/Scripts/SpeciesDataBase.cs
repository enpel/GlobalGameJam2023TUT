using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;

[System.Serializable]
public struct SpeciesData
{
    public string SpeciesName_jp;
    public string SpeciesName_en;

    [HelpBox("この数値を超えた場合に適応される")]
    public int ConditionsSpeciesParameter;
    public int AddOutstanding;

    public Color SpeciesColor;
    public Color TreeColor;
}

[CreateAssetMenu(fileName = "SpeciesDataBase", menuName = "DataBase/New SpeciesDataBase")]
public class SpeciesDataBase : ScriptableObject
{
    [HelpBox("最も条件が厳しいものから順番にしていく", HelpBoxType.Info)]
    [SerializeField]
    private SpeciesData[] _AllDatas; // 全ての数値が満たしてることが条件
    [SerializeField]
    private SpeciesData[] _PenetrationDatas;
    [SerializeField]
    private SpeciesData[] _GrowthDatas;
    [SerializeField]
    private SpeciesData[] _BeautyDatas;
    [SerializeField]
    private SpeciesData[] _AbsorptionDatas;

    public SpeciesData GetSpeciesData(int[] param)
    {
        var mostParam = 0;
        var mostType = PlayerGrowthParameters.GrowthType.All;
        var zeroParamCount = 0;
        for (int i = 0; i < param.Length; i++)
        {
            if(param[i] == 0)
            {
                zeroParamCount++;
                continue;
            }
            if(param[i] >= mostParam)
            {
                mostParam = param[i];
                mostType = (PlayerGrowthParameters.GrowthType)i;
            }
        }
        if (zeroParamCount >= 4) // 全てが0なら始まりの種
        {
            return _AllDatas.GetLast();
        }

        foreach (var allData in _AllDatas)
        {
            // 一番高い数値が、基準値を上回ったら特徴種で計算
            if(mostParam >= allData.ConditionsSpeciesParameter + allData.AddOutstanding)
            {
                // 特徴のある品種で、計算
                foreach (var data in TargetSpecies(mostType))
                {
                    if(param[(int)mostType] >= data.ConditionsSpeciesParameter)
                    {
                        return data;
                    }
                }
                Log.Error(this, "Target Error");
                return TargetSpecies(mostType).GetLast();
            }

            // 平均的な品種で計算
            var isUnder = false;
            for (int i = 0; i < param.Length; i++)
            {
                if(param[i] < allData.ConditionsSpeciesParameter)
                {
                    // 一つでも下回っていたら次の計算に
                    isUnder = true;
                    break;
                }
            }
            if (isUnder)
                continue;

            // 全て満たしていたので、この品種
            return allData;
        }

        return _AllDatas.GetLast();
    }

    private SpeciesData[] TargetSpecies(PlayerGrowthParameters.GrowthType type)
    {
        switch (type)
        {
            case PlayerGrowthParameters.GrowthType.Penetration:
                return _PenetrationDatas;
            case PlayerGrowthParameters.GrowthType.Growth:
                return _GrowthDatas;
            case PlayerGrowthParameters.GrowthType.Beauty:
                return _BeautyDatas;
            case PlayerGrowthParameters.GrowthType.Absorption:
                return _AbsorptionDatas;
            default:
                Log.Error(this, "Error Type");
                return null;
        }
    }
}
