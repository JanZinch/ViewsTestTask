using Models;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;
        
        private Settings _settings;

        private void Awake()
        {
            Instance = this;
        }

        public AudioManager Initialize(Settings settings)
        {
            _settings = settings;

            /*if (_settings.IsMusicOn)
            {
                _musicSource.Play();
            }*/
            
            return this;
        }

        public void PlayOneShot(AudioClip audioClip)
        {
            _soundSource.PlayOneShot(audioClip);
        }
    }
}