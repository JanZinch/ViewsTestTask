using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        
        [SerializeField] private AudioSource _soundSource;
        
        private void Awake()
        {
            Instance = this;
        }

        /*public AudioManager Initialize(Settings settings)
        {
            return this;
        }*/

        public void PlayOneShot(AudioClip audioClip)
        {
            _soundSource.PlayOneShot(audioClip);
        }
    }
}