using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Nutrition
{
    public override bool HitNutritionObject(int penetrationPower)
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

    public override void AbsorbedObject()
    {
        Invoke("EnableObj", _disableTime);
    }
}
