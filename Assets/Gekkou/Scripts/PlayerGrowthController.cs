using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gekkou;

public class PlayerGrowthController : MonoBehaviour
{
    [SerializeField]
    private PlayerGrowthParameters _growthParameters;

    [SerializeField] private AnimatableText effectText;
    //[SerializeField] GameObject particlePrefab;
    public AudioClip sound1;
    AudioSource audioSource;
    //[SerializeField]
    //private TimeManager timeManager;

    private void Start()
    {
        if (_growthParameters == null)
            _growthParameters = PlayerGrowthParameters.Instance;
        audioSource = GetComponent<AudioSource>();
    }

    private void HitNutrition(Nutrition nutrition)
    {
        if (GameSystemController.Instance.IsGameLevelStop)
            return;

        if (nutrition.HitNutritionObject(_growthParameters.PenetrationgPower))
        {
            // 栄養を吸収できた
            _growthParameters.SetParameter(nutrition.GrowthType, nutrition.NutritionQuantity);
            //Instantiate(particlePrefab, transform.position, Quaternion.identity);
            EffectManager.Instance.PlayEffect(EffectName.Absorption, transform.position);
            nutrition.AbsorbedObject();
            audioSource.PlayOneShot(sound1);
            //timeManager.SlowDown();
            
            
            effectText.PlayJump("+" + nutrition.NutritionQuantity);
        }
        else
        {
            // 栄養に抵抗された
            _growthParameters.UploadParameter();
            //SceneSystemManager.Instance.SceneLoading(Scene.ResultScene);
            GameSystemController.Instance.IsGameLevelStop = true;
            GameSystemController.Instance.ChangeGameLevel(GameSystemController.GameLevel.Result);
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
