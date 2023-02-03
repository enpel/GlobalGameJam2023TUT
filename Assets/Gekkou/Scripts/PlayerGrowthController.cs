using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowthController : MonoBehaviour
{
    [SerializeField]
    private PlayerGrowthParameters _growthParameters;

    private void Start()
    {
        if (_growthParameters == null)
            _growthParameters = GetComponent<PlayerGrowthParameters>();
    }

    private void HitNutrition(Nutrition nutrition)
    {
        if (nutrition.HitNutritionObject(_growthParameters.PenetrationgPower))
        {
            // 栄養を吸収できた
            _growthParameters.SetParameter(nutrition.GrowthType, nutrition.NutritionQuantity);
            nutrition.AbsorbedObject();
            Gekkou.Log.Info(this, "Get {0} : {1}", nutrition.GrowthType.ToString(), nutrition.NutritionQuantity);
        }
        else
        {
            // 栄養に抵抗された
            Gekkou.Log.Info(this, "Game Over");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Nutrition nutrition))
        {
            HitNutrition(nutrition);
        }
    }

}
