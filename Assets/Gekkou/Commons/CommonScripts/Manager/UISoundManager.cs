using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    [RequireComponent(typeof(AudioSource))]
    public class UISoundManager : SingletonMonobehavior<UISoundManager>
    {
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip successClip;

        [SerializeField]
        private AudioClip failureClip;

        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySuccess() { audioSource.PlayOneShot(successClip); }
        public void PlayFailure() { audioSource.PlayOneShot(failureClip); }
        public void PlayAudioClip(AudioClip clip) { audioSource.PlayOneShot(clip); }
    }

}
