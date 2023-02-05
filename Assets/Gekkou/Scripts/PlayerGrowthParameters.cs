using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;

public class PlayerGrowthParameters : SingletonMonobehavior<PlayerGrowthParameters>
{
    public enum GrowthType
    { 
        Penetration,
        Growth,
        Beauty,
        Absorption,
        Random,
        All,
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

    public int GetParameter(GrowthType type)
    {
        if ((int)type < 4)
            return _growthParameters[(int)type];
        else
            return -1;
    }

    protected override void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UploadPlayerStatus();
    }

    public void SetParameter(GrowthType type, int value)
    {
        if (type == GrowthType.Random)
        {
            _growthParameters[Random.Range(0, 4)] += value;
        }
        else if(type == GrowthType.All)
        {
            for (int i = 0; i < 4; i++)
            {
                _growthParameters[i] += value;
            }
        }
        else
        {
            _growthParameters[(int)type] += value;
        }

        if (type == GrowthType.Growth)
        {
            UploadPlayerStatus();
        }
    }

    /// <summary>
    /// 栄養によってパワーアップするのを反映させる
    /// </summary>
    public void UploadPlayerStatus()
    {
        PlayerMovementController.Instance.GrowthRate = _growthParameters[(int)GrowthType.Growth];
    }

    public void UploadParameter()
    {
        GrowthParameterManager.Instance.UploadParameter(_growthParameters);
    }

    public void SettingParameter(int[] param)
    {
        for (int i = 0; i < _growthParameters.Length; i++)
        {
            _growthParameters[i] = param[i];
        }
    }
}
