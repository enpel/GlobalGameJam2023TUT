using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Nutrition
{
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;
    [SerializeField]
    private TimeManager timeManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeManager = TimeManager.Instance;
    }

    public override bool HitNutritionObject(int penetrationPower)
    {
        // 種類が吸収なので吸収
        if (_nutritionType == NutritionType.Absorption)
            return true;

        // 抵抗値が上回ったので抵抗
        if (_resistPenetrationPower > penetrationPower)
        {
            audioSource.PlayOneShot(sound1);
            timeManager.SlowDown();
            return false;
        }

        // 下回ったので吸収
        return true;
    }

    public override void AbsorbedObject()
    {
        Invoke("EnableObj", _disableTime);
        audioSource.PlayOneShot(sound2);
        timeManager.SlowDown();
    }
}
