using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

namespace Gekkou
{

    public class BGMManager : SingletonMonobehavior<BGMManager>
    {
        [SerializeField]
        private AudioSource[] audioSources;

        [SerializeField, Min(0)]
        private int mainAudioIndex = 0;

        [SerializeField]
        private float audioFadeTime = 0.5f;

        private SoundManager soundManager;

        private void Start()
        {
            soundManager = SoundManager.Instance;
        }

        /// <summary>
        /// BGMを瞬時に切り替える
        /// </summary>
        public void ChangeAudio(AudioClip newClip)
        {
            var newIndex = mainAudioIndex + 1;
            if (newIndex >= audioSources.Length) newIndex = 0;

            audioSources[newIndex].clip = newClip;

            audioSources[newIndex].Play();
            audioSources[newIndex].volume = soundManager.BgmVolume;
            audioSources[mainAudioIndex].volume = 0.0f;
            audioSources[mainAudioIndex].Stop();

            mainAudioIndex = newIndex;
        }

        /// <summary>
        /// BGMをフェードしながら切り替える
        /// </summary>
        public void FadeAudio(AudioClip newClip) { FadeAudio(newClip, audioFadeTime); }

        /// <summary>
        /// BGMをフェードしながら切り替える
        /// </summary>
        public void FadeAudio(AudioClip newClip, float fadeTime)
        {
            var newIndex = mainAudioIndex + 1;
            if (newIndex >= audioSources.Length) newIndex = 0;

            audioSources[newIndex].clip = newClip;

            StartCoroutine(FadeAudio(newIndex, fadeTime));
        }

        private IEnumerator FadeAudio(int newIndex, float fadeTime)
        {
            audioSources[newIndex].Play();

            {
                var t = 0.0f;
                var nVolume = audioSources[newIndex].volume;
                var mVolume = audioSources[mainAudioIndex].volume;
                while (true)
                {
                    t += Time.deltaTime;
                    audioSources[newIndex].volume = Mathf.Lerp(nVolume, 1.0f * soundManager.BgmVolume, t);
                    audioSources[mainAudioIndex].volume = Mathf.Lerp(mVolume, 0.0f, t);
                    if (t >= fadeTime)
                    {
                        audioSources[newIndex].volume = 1.0f * soundManager.BgmVolume;
                        audioSources[mainAudioIndex].volume = 0.0f;
                        break;
                    }

                    yield return null;
                }
            }
            {
                //audioSources[newIndex].DOFade(1.0f * soundManager.BgmVolume, fadeTime);
                //yield return audioSources[mainAudioIndex].DOFade(0.0f, fadeTime).WaitForCompletion();
            }

            audioSources[mainAudioIndex].Stop();

            mainAudioIndex = newIndex;
        }
    }

}
