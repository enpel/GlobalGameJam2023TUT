using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

namespace Gekkou
{

    public class GameUIManager : SingletonMonobehavior<GameUIManager>
    {
        [SerializeField]
        private GameObject loadingMenuObj;
        [SerializeField]
        private Image fadePanel;
        [SerializeField]
        private Slider loadingSlider;

        public void ChangeEnableLoadingMenu(bool argEnable)
        {
            loadingMenuObj.SetActive(argEnable);
        }

        public void ChangeSlider(float value)
        {
            loadingSlider.value = value;
        }

        public IEnumerator FadeInOut(float inTime = 1.0f, float outTime = 1.0f)
        {
            yield return StartCoroutine(FadeIn(inTime));
            yield return StartCoroutine(FadeOut(outTime));
        }

        public IEnumerator FadeIn(float time = 1.0f)
        {
            fadePanel.gameObject.SetActive(true);
            yield return StartCoroutine(Fade(1.0f, time));
        }

        public IEnumerator FadeOut(float time = 1.0f)
        {
            yield return StartCoroutine(Fade(0.0f, time));
            fadePanel.gameObject.SetActive(false);
        }

        /*
        // DoTween 導入済の場合
        public IEnumerator Fade(float alpha, float time = 1.0f)
        {
            yield return fadePanel.DOFade(alpha, time).WaitForCompletion();
        }
        */


        // DoTween 未導入の場合
        public IEnumerator Fade(float alpha, float time = 1.0f)
        {
            var t = 0.0f;
            var a = fadePanel.color.a;
            while (true)
            {
                t += Time.deltaTime;
                fadePanel.SetOpacity(Mathf.Lerp(a, alpha, Mathf.Clamp01(t / time)));
                if (t >= time)
                {
                    fadePanel.SetOpacity(alpha);
                    break;
                }

                yield return null;
            }
        }

    }

}
