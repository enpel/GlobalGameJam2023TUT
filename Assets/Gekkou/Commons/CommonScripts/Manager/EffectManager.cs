using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    public class EffectManager : SingletonMonobehavior<EffectManager>
    {
        [SerializeField, EnumIndex(typeof(EffectName))]
        private GameObject[] Prefabs;

        private Dictionary<EffectName, List<GameObject>> effectObjs = new Dictionary<EffectName, List<GameObject>>();

        protected override void Awake()
        {
            Instance = this;
        }

        public GameObject PlayEffect(EffectName eName, Vector3 pos)
        {
            return PlayEffect(eName, pos, Quaternion.identity);
        }

        public GameObject PlayEffect(EffectName eName, Vector3 pos, Quaternion rot)
        {
            if (effectObjs.ContainsKey(eName)) // eNameが含まれている
            {
                foreach (var o in effectObjs[eName])
                {
                    if (!o.activeSelf) // 非アクティブのため、使用する
                    {
                        o.transform.position = pos;
                        o.SetActive(true);
                        return o;
                    }
                }
            }

            // 在庫がない場合は、新たに生成する
            var obj = Instantiate(Prefabs[(int)eName], pos, rot);
            if (effectObjs.ContainsKey(eName))
            {
                effectObjs[eName].Add(obj);
            }
            else
            {
                var list = new List<GameObject>() { obj };
                effectObjs.Add(eName, list);
            }
            return obj;
        }
    }

}
