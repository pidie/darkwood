using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource _audioSource;
        private AudioClipController[] _audioClipControllers;

        public static AudioSource AudioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioClipControllers = GetComponents<AudioClipController>();

            AudioSource = _audioSource;
        }

        public static void PlayAudioClip(AudioClip clip) => AudioSource.PlayOneShot(clip);
    }
}
