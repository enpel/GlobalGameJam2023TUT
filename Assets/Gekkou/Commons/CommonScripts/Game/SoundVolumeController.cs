using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gekkou
{

    /// <summary>
    /// 設定などで音量を変更する際に使用する
    /// </summary>
    public class SoundVolumeController : MonoBehaviour
    {
        [SerializeField]
        private SoundManager.AudioType audioType;

        [SerializeField]
        private Slider audioSlider;

        private SoundManager soundManager;

        private void Start()
        {
            soundManager = SoundManager.Instance;

            audioSlider.value = audioType switch
            {
                SoundManager.AudioType.BGM => soundManager.BgmVolume,
                SoundManager.AudioType.SE => soundManager.SeVolume,
                SoundManager.AudioType.Master => soundManager.MasterVolume,
                _ => 0.5f,
            };

            audioSlider.onValueChanged.AddListener(ChangeVolume);
        }

        public void ChangeVolume(float volume)
        {
            soundManager.SetVolume(volume, audioType);
        }

        public void SaveSoundVolume()
        {
            soundManager.SaveSoundVolume();
        }
    }

}
