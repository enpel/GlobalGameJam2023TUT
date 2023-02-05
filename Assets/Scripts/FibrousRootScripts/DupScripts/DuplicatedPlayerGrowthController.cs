using UnityEngine;
using Gekkou;

public class DuplicatedPlayerGrowthController : MonoBehaviour
{
    [SerializeField]
    private PlayerGrowthParameters _growthParameters;
    [SerializeField] 
    private FibrousRootManager _fibousRootController;
    public AudioClip sound1;
    AudioSource audioSource;

    private void Start()
    {
        if (_growthParameters == null)
            _growthParameters = PlayerGrowthParameters.Instance;
        audioSource = GetComponent<AudioSource>();
    }

    private void HitNutrition(Nutrition nutrition)
    {
        if (nutrition.HitNutritionObject(_growthParameters.PenetrationgPower))
        {
            // 栄養を吸収できた
            _growthParameters.SetParameter(nutrition.GrowthType, nutrition.NutritionQuantity);
            //Instantiate(particlePrefab, transform.position, Quaternion.identity);
            EffectManager.Instance.PlayEffect(EffectName.Absorption, transform.position);
            nutrition.AbsorbedObject();
            audioSource.PlayOneShot(sound1);
            Log.Info(this, "Get {0} : {1} : Now {2}"
                , nutrition.GrowthType.ToString(), nutrition.NutritionQuantity, _growthParameters.GetParameter(nutrition.GrowthType));
        }
        else
        {
            // 栄養に抵抗された
            Log.Info(this, "Game Over");
            _growthParameters.UploadParameter();
            SceneSystemManager.Instance.SceneLoading(Scene.ResultScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Layer_Rock"))
        {
            _fibousRootController.InstantiateFabirousRoots();
        }
        if (other.TryGetComponent(out Nutrition nutrition))
        {
            HitNutrition(nutrition);
        }
    }

}
