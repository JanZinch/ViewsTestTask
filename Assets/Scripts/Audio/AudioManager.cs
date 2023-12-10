using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        
        [SerializeField] private AudioSource _soundSource;

        private const string SoundsPitchParam = "sounds_pitch";
        
        private void Awake()
        {
            Instance = this;
        }
        
        public void PlayOneShot(AudioClip audioClip)
        {
            _soundSource.outputAudioMixerGroup.audioMixer.SetFloat(SoundsPitchParam, Random.Range(0.9f, 1.1f));
            _soundSource.PlayOneShot(audioClip);
        }
    }
}