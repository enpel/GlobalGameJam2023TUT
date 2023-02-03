using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    [RequireComponent(typeof(AudioSource))]
    public class SeController : SingletonMonobehavior<SeController>
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _successClip;

        private void Reset()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySuccess()
        {
            _audioSource.PlayOneShot(_successClip);
        }

        public void PlayAudioClip(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }

}
