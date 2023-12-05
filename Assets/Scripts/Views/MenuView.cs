using System;
using Factories;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class MenuView : BaseView
    {
        [SerializeField] private Button _playButton;
        
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _dailyBonusButton;
        [SerializeField] private Button _shopButton;

        private ViewsFactory _viewsFactory;
        private Settings _settings;

        private void Awake()
        {
            _settings = new Settings();
        }

        public MenuView InjectDependencies(ViewsFactory viewsFactory)
        {
            _viewsFactory = viewsFactory;
            return this;
        }
        
        private void OnEnable()
        {
            _playButton.onClick.AddListener(Play);
            _settingsButton.onClick.AddListener(ShowSettings);;
            _dailyBonusButton.onClick.AddListener(ShowDailyBonuses);;
            _shopButton.onClick.AddListener(ShowShop);;
        }

        private void Play()
        {

        }
        
        private void ShowSettings()
        {
            _viewsFactory.ShowView<SettingsView>().InjectDependencies(_settings);
        }
        
        private void ShowDailyBonuses()
        {
            _viewsFactory.ShowView<DailyBonusPresenter>();
        }
        
        private void ShowShop()
        {

        }
        
        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(Play);
            _settingsButton.onClick.RemoveListener(ShowSettings);
            _dailyBonusButton.onClick.RemoveListener(ShowDailyBonuses);
            _shopButton.onClick.RemoveListener(ShowShop);
        }
    }
}