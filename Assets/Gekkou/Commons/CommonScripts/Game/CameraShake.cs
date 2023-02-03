using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    /// <summary>
    /// カメラを揺らす
    /// </summary>
    public class CameraShake : SingletonMonobehavior<CameraShake>
    {
        private Transform cameraTrans;
        private Vector3 origin;

        protected override void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            cameraTrans = Camera.main.transform;
            origin = cameraTrans.localPosition;
        }

        public void PlayShake(float duration, float magnitude)
        {
            StartCoroutine(Shake(duration, magnitude));
        }

        private IEnumerator Shake(float duration, float magnitude)
        {
            var elapsed = 0.0f;

            while (elapsed < duration)
            {
                var x = Random.Range(-1f, 1f) * magnitude;
                var y = Random.Range(-1f, 1f) * magnitude;

                cameraTrans.localPosition = new Vector3(x, y, 0) + origin;

                elapsed += Time.deltaTime;

                yield return null;

            }

            cameraTrans.localPosition = origin;
        }
    }

}
