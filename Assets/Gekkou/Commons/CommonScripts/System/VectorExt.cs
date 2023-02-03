using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    public static class VectorExt
    {
        public static readonly Vector3 VectorXZ = new Vector3(1, 0, 1);

        public static Vector3 GetVecXZ(this Transform trans)
        {
            return GetVecXZ(trans.position);
        }

        public static Vector3 GetVecXZ(this Vector3 vec)
        {
            return new Vector3(vec.x, 0, vec.z);
        }

        public static Vector3 GetVecXZNor(this Vector3 vec)
        {
            return new Vector3(vec.x, 0, vec.z).normalized;
        }
    }

}
