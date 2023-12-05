using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class SettingsView : BaseView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _hideArea;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundsToggle;
        
        private Settings _settings;
        
        public SettingsView InjectDependencies(Settings settings)
        {
            _settings = settings;
            _musicToggle.isOn = _settings.IsMusicOn;
            _soundsToggle.isOn = _settings.IsSoundsOn;
            return this;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Hide);
            _hideArea.onClick.AddListener(Hide);
            _musicToggle.onValueChanged.AddListener(OnMusicStateChanged);
            _soundsToggle.onValueChanged.AddListener(OnSoundsStateChanged);
        }

        private void OnMusicStateChanged(bool value)
        {
            _settings.IsMusicOn = value;
        }

        private void OnSoundsStateChanged(bool value)
        {
            _settings.IsSoundsOn = value;
        }
        
        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _hideArea.onClick.RemoveListener(Hide);
            _musicToggle.onValueChanged.RemoveListener(OnMusicStateChanged);
            _soundsToggle.onValueChanged.RemoveListener(OnSoundsStateChanged);
        }
    }
}