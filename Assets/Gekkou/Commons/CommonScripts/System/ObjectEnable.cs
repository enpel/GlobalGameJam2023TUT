using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    public class ObjectEnable : MonoBehaviour
    {
        public void VisibleObj(GameObject obj) { obj.SetActive(true); }
        public void InvisibleObj(GameObject obj) { obj.SetActive(false); }

        public void VisibleObjs(params GameObject[] objs) { foreach (var obj in objs) { obj.SetActive(true); } }
        public void InvisibleObjs(params GameObject[] objs) { foreach (var obj in objs) { obj.SetActive(false); } }
    }

}
