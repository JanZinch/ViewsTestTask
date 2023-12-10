using System;
using Bonuses;
using Bonuses.Models;
using Core.Basics;
using InGameResources;
using Progress;
using Purchases.Common;
using Purchases.InAppPurchases;
using Purchases.PurchasesForResource;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Audio;

namespace Core
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private MenuView _menuView;
        [SerializeField] private ViewsFactory _viewsFactory;
        
        [Space]
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private PurchaseAccessConfig _purchaseAccessConfig;
        [SerializeField] private DailyBonusesContainer _dailyBonusesContainer;
        
        private const string EnvironmentName = "dev";

        public Settings.Settings Settings { get; private set; }
        public ViewsFactory ViewsFactory => _viewsFactory;
        public ProgressDataModel ProgressDataModel { get; private set; }
        public ResourceService ResourceService { get; private set; }
        public PurchaseForResourceService PurchaseForResourceService { get; private set; }
        public InAppPurchaseService InAppPurchaseService { get; private set; }
        public DailyBonusService DailyBonusService { get; private set; }
        
        private static async void InitializeUnityServices(Action onComplete)
        {
            try {
                
                InitializationOptions options = new InitializationOptions().SetEnvironmentName(EnvironmentName);
                await UnityServices.InitializeAsync(options);
                
                onComplete?.Invoke();
            }
            catch (Exception exception) {
                
                Debug.LogException(exception);
            }
        }
        
        private void Awake()
        {
            ProgressDataAdapter progressDataAdapter = new ProgressDataAdapter();
            ProgressDataModel = progressDataAdapter.GetProgressModel();
            
            ResourceService = new ResourceService(ProgressDataModel);
            DailyBonusService = new DailyBonusService(ProgressDataModel, _dailyBonusesContainer, ResourceService);
            PurchaseForResourceService = new PurchaseForResourceService(ResourceService, ProgressDataModel, _purchaseAccessConfig);
        }

        private void Start()
        {
            Settings = new Settings.Settings(_audioMixer);

            InitializeUnityServices(() =>
            {
                InAppPurchaseService = new InAppPurchaseService(new ProfitDistributor(ResourceService));
                _menuView.Initialize(this);
            });
        }
    }
}
