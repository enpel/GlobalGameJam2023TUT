using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    public class SoundManager : SingletonMonobehavior<SoundManager>
    {
        public enum AudioType
        {
            BGM,
            SE,
            Master,
        }

        private float masterVolume = 1.0f;
        public float MasterVolume { get { return masterVolume; } }

        private float bgmVolume = 0.5f;
        public float BgmVolume { get { return Mathf.Clamp01(bgmVolume * masterVolume); } }

        private float seVolume = 0.5f;
        public float SeVolume { get { return Mathf.Clamp01(seVolume * masterVolume); } }

        [SerializeField]
        private List<AudioSource> bgmAudios = new List<AudioSource>();

        [SerializeField]
        private List<AudioSource> seAudios = new List<AudioSource>();

        private void Start()
        {
            LoadSoundVolume();
        }

        public void AudioListControll(AudioSource audio, AudioType type, bool isAdding)
        {
            if (type == AudioType.BGM)
            {
                if (isAdding)
                    bgmAudios.Add(audio);
                else
                    bgmAudios.Remove(audio);
            }
            else if (type == AudioType.SE)
            {
                if (isAdding)
                    seAudios.Add(audio);
                else
                    seAudios.Remove(audio);
            }
        }

        private void CheckListOfNull()
        {
            bgmAudios.RemoveAll(i => i == null);
            seAudios.RemoveAll(i => i == null);
        }

        public void SetVolume(float master, float bgm, float se)
        {
            CheckListOfNull();
            SetMasterVolume(master);
            SetBgmVolume(bgm);
            SetSeVolume(se);
        }

        public void SetVolume(float volume, AudioType type)
        {
            CheckListOfNull();
            switch (type)
            {
                case AudioType.BGM:
                    SetBgmVolume(volume);
                    break;
                case AudioType.SE:
                    SetSeVolume(volume);
                    break;
                case AudioType.Master:
                    SetMasterVolume(volume);
                    break;
            }
        }

        public void SetMasterVolume(float master)
        {
            masterVolume = master;
            SetTheVolumeOfBgm();
            SetTheVolumeOfSe();
        }

        public void SetBgmVolume(float bgm)
        {
            bgmVolume = bgm;
            SetTheVolumeOfBgm();
        }

        public void SetSeVolume(float se)
        {
            seVolume = se;
            SetTheVolumeOfSe();
        }

        private void SetTheVolumeOfBgm()
        {
            foreach (var bgm in bgmAudios)
            {
                bgm.volume = masterVolume * bgmVolume;
            }
        }

        private void SetTheVolumeOfSe()
        {
            foreach (var se in seAudios)
            {
                se.volume = masterVolume * seVolume;
            }
        }

        public void SaveSoundVolume()
        {
            SaveSystemManager.Instance.Saving(masterVolume, bgmVolume, seVolume);
        }

        public void LoadSoundVolume()
        {
            var data = SaveSystemManager.Instance.SaveData;
            SetVolume(data.masterVolume, data.bgmVolume, data.seVolume);
        }
    }

}
