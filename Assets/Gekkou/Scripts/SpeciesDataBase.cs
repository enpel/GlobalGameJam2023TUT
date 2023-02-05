using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;

[System.Serializable]
public struct SpeciesData
{
    public string SpeciesName_jp;
    public string SpeciesName_en;

    [HelpBox("この数値全てを超えた場合に適応される")]
    [EnumIndex(typeof(PlayerGrowthParameters.GrowthType))]
    public int[] ConditionsSpeciesParameters;

    public Color SpeciesColor;
    public Color TreeColor;
}

[CreateAssetMenu(fileName = "SpeciesDataBase", menuName = "DataBase/New SpeciesDataBase")]
public class SpeciesDataBase : ScriptableObject
{
    [HelpBox("最も条件が厳しいものから順番にしていく", HelpBoxType.Info)]
    public SpeciesData[] SpeciesDatas;

    public SpeciesData GetSpeciesData(int[] param)
    {
        for (int i = 0; i < SpeciesDatas.Length; i++)
        {
            var isSuccess = true;
            for (int j = 0; j < SpeciesDatas[i].ConditionsSpeciesParameters.Length; j++)
            {
                if (param[j] < SpeciesDatas[i].ConditionsSpeciesParameters[j]) // 基準を下回ったら次の判定に移る
                {
                    isSuccess = false;
                    break;
                }
            }
            if (!isSuccess)
                continue;

            return SpeciesDatas[i];
        }


        return SpeciesDatas.GetLast();
    }
}
