using Bonuses;
using Factories;
using InAppResources;
using Levels;
using Roots;
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
        
        private GameRoot _gameRoot;
        private ViewsFactory _viewsFactory;
        private DailyBonusService _dailyBonusService;
        
        public MenuView Initialize(GameRoot gameRoot)
        {
            _gameRoot = gameRoot;
            _viewsFactory = _gameRoot.ViewsFactory;
            _dailyBonusService = gameRoot.DailyBonusService;
            _resourceCounter.Initialize(_gameRoot.ResourceService);
            
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
            _viewsFactory.ShowView<LevelMapView>().Initialize(_gameRoot.ProgressDataModel);
        }
        
        private void ShowSettings()
        {
            _viewsFactory.ShowView<SettingsView>().InjectDependencies(_gameRoot.Settings);
        }
        
        private void ShowDailyBonuses()
        {
            new DailyBonusExecutor(_viewsFactory, _dailyBonusService).Execute();
        }
        
        private void ShowShop()
        {
            _viewsFactory.ShowView<ShopView>().Initialize(_gameRoot.InAppPurchaseService, _gameRoot.PurchaseForResourceService);
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