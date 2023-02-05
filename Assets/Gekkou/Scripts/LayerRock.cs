﻿using System.Collections;
using System.Collections.Generic;
using Gekkou;
using UnityEngine;

public class LayerRock : Nutrition
{
    [SerializeField] private AudioSource source;
    [SerializeField] FibrousRootsManager m_fibMgr;

    public override void AbsorbedObject()
    {
        // ここにカメラを引くとかの処理を追加する。
        // 必要なら関数とか追加して

        
        source.PlayOneShot(source.clip);

        // ひげ根
        m_fibMgr.InstantiateFabirousRoots();
        Invoke("EnableObj", _disableTime);
        
    }
}
