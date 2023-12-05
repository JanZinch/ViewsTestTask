using System;
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
            UpdateView();
            return this;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Hide);
            _hideArea.onClick.AddListener(Hide);
            _musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
            _soundsToggle.onValueChanged.AddListener(OnSoundsToggleChanged);
        }

        private void OnMusicToggleChanged(bool value)
        {
            _settings.IsMusicOn = value;
            UpdateView();
        }

        private void OnSoundsToggleChanged(bool value)
        {
            _settings.IsSoundsOn = value;
            UpdateView();
        }

        private void UpdateView()
        {
            
            
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _hideArea.onClick.RemoveListener(Hide);
            _musicToggle.onValueChanged.RemoveListener(OnMusicToggleChanged);
            _soundsToggle.onValueChanged.RemoveListener(OnSoundsToggleChanged);
        }
    }
}