using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerRock : Nutrition
{
    public override void AbsorbedObject()
    {
        // ここにカメラを引くとかの処理を追加する。
        // 必要なら関数とか追加して

        Invoke("EnableObj", _disableTime);
    }
}
