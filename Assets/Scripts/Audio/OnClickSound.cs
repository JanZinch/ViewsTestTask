using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    //[RequireComponent(typeof(AudioSource))]
    public class OnClickSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _sound;
        
        private Button _button;
        //private AudioSource _audioSource;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            
            /*_audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
            _audioSource.spatialBlend = 0.0f;*/
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayOneShot(_sound);
            }
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}