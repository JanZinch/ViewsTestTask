using System;
using Bonuses;
using Factories;
using InAppResources;
using Levels;
using Models;
using Progress;
using Purchases;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class MenuView : BaseView
    {
        [SerializeField] private ResourceCounter _resourceCounter;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _dailyBonusButton;
        [SerializeField] private Button _shopButton;
        
        [Space]
        [SerializeField] private DailyBonusesContainer _dailyBonusesContainer;
        
        private ViewsFactory _viewsFactory;
        private Settings _settings;
        private ProgressDataModel _progressDataModel;
        private DailyBonusService _dailyBonusService;

        private ResourceService _resourceService;

        private PurchaseForResourceService _purchaseService;
        private InAppPurchaseService _inAppPurchaseService;
        
        private void Awake()
        {
            _settings = new Settings();
            
            ProgressDataAdapter progressDataAdapter = new ProgressDataAdapter();
            _progressDataModel = progressDataAdapter.GetProgressModel();
            
            _resourceService = new ResourceService(_progressDataModel);
            _dailyBonusService = new DailyBonusService(_progressDataModel, _dailyBonusesContainer, _resourceService);

            _purchaseService = new PurchaseForResourceService(_resourceService, _progressDataModel);
            _inAppPurchaseService = new InAppPurchaseService();
            
            _resourceCounter.InjectDependencies(_resourceService);
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
            _viewsFactory.ShowView<LevelMapView>().Initialize(_progressDataModel);
        }
        
        private void ShowSettings()
        {
            _viewsFactory.ShowView<SettingsView>().InjectDependencies(_settings);
        }
        
        private void ShowDailyBonuses()
        {
            _viewsFactory.ShowView<DailyBonusPresenter>().InjectDependencies(_viewsFactory, _dailyBonusService);
        }
        
        private void ShowShop()
        {
            _viewsFactory.ShowView<ShopView>().Initialize(_inAppPurchaseService, _purchaseService);
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