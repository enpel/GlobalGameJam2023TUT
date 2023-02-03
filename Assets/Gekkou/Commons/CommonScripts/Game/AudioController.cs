using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    /// <summary>
    /// audiosourceが付いているものにアタッチすることで、自動的に音量を設定する
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private SoundManager.AudioType audioType;

        [SerializeField]
        private AudioSource audioSource;

        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            SoundManager.Instance.AudioListControll(audioSource, audioType, true);

            audioSource.volume = audioType switch
            {
                SoundManager.AudioType.BGM => SoundManager.Instance.BgmVolume,
                SoundManager.AudioType.SE => SoundManager.Instance.SeVolume,
                _ => 0.5f,
            };
        }

        private void OnDestroy()
        {
            if (!Application.isPlaying && !Application.isEditor)
                SoundManager.Instance.AudioListControll(audioSource, audioType, false);
        }
    }

}
