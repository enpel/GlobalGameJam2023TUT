using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrition : MonoBehaviour
{
    /// <summary>
    /// 栄養に当たった時の挙動の種類
    /// </summary>
    public enum NutritionType
    {
        Absorption, // 吸収
        Resistance, // 抵抗
    }

    /// <summary>
    /// 栄養に当たった時の挙動
    /// </summary>
    [SerializeField]
    protected NutritionType _nutritionType = NutritionType.Absorption;

    /// <summary>
    /// 栄養の種類
    /// </summary>
    [SerializeField]
    protected PlayerGrowthParameters.GrowthType _growthType = 0;
    /// <summary> 栄養の種類 </summary>
    public PlayerGrowthParameters.GrowthType GrowthType { get => _growthType; }

    /// <summary>
    /// 栄養素の量
    /// </summary>
    [SerializeField, Min(1)]
    protected int _nutritionQuantity = 0;
    /// <summary> 栄養素の量 </summary>
    public int NutritionQuantity { get => _nutritionQuantity; }

    /// <summary>
    /// 抵抗できる貫通力
    /// </summary>
    [SerializeField, Min(0)]
    protected int _resistPenetrationPower = 0;

    /// <summary>
    /// 非表示になるまでの時間
    /// </summary>
    [SerializeField, Min(0)]
    protected float _disableTime = 0.5f;

    /// <summary>
    /// 栄養に当たった際に、吸収できるかどうか判定
    /// </summary>
    /// <param name="penetrationPower"> 貫通力 </param>
    /// <returns>  </returns>
    public virtual bool HitNutritionObject(int penetrationPower)
    {
        // 種類が吸収なので吸収
        if (_nutritionType == NutritionType.Absorption)
            return true;

        // 抵抗値が上回ったので抵抗
        if (_resistPenetrationPower > penetrationPower)
            return false;

        // 下回ったので吸収
        return true;
    }

    /// <summary>
    /// 吸収された後の処理
    /// </summary>
    public virtual void AbsorbedObject()
    {
        Invoke("EnableObj", _disableTime);
    }

    protected void EnableObj()
    {
        gameObject.SetActive(false);
    }
}
