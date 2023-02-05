using System.Collections;
using System.Collections.Generic;
using Gekkou;
using UnityEngine;

public class LayerRock : Nutrition
{
    [SerializeField]
    private AudioClip sound1;
    [SerializeField]
    private AudioClip sound2;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField] FibrousRootsManager m_fibMgr;


    public override bool HitNutritionObject(int penetrationPower)
    {
        // 種類が吸収なので吸収
        if (_nutritionType == NutritionType.Absorption)
            return true;

        // 抵抗値が上回ったので抵抗
        if (_resistPenetrationPower > penetrationPower)
        {
            audioSource.PlayOneShot(sound1);
            return false;
        }

        // 下回ったので吸収
        return true;
    }

    public override void AbsorbedObject()
    {
        // ここにカメラを引くとかの処理を追加する。
        // 必要なら関数とか追加して


        audioSource.PlayOneShot(sound2);

        // ひげ根
        m_fibMgr.InstantiateFabirousRoots();
        Invoke("EnableObj", _disableTime);
        
    }
}
