using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    public class OnClickSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _sound;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
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