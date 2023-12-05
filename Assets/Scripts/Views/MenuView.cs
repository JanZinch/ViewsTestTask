using System;
using Factories;
using Models;
using Progress;
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

        [Space]
        [SerializeField] private DailyBonusesContainer _dailyBonusesContainer;
        
        private ViewsFactory _viewsFactory;
        private Settings _settings;
        private ProgressService _progressService;
        

        private void Awake()
        {
            _settings = new Settings();
            _progressService = new ProgressService();
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
            _viewsFactory.ShowView<DailyBonusPresenter>().InjectDependencies(_progressService, _dailyBonusesContainer);
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