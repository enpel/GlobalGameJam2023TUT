using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    public static class TransformExt
    {
        public static void DoubleLocalScale(this Transform trans, Vector3 value)
        {
            var lsc = trans.localScale;
            lsc.x *= value.x; lsc.y *= value.y; lsc.z *= value.z;
            trans.localScale = lsc;
        }

        public static void DoubleLocalScaleX(this Transform trans, float value)
        {
            var lsc = trans.localScale;
            lsc.x *= value;
            trans.localScale = lsc;
        }
    }

}
