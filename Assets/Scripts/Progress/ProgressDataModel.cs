using System;
using System.Collections.Generic;
using Bonuses;
using Bonuses.Models;
using InGameResources;
using Newtonsoft.Json;
using Purchases.Common;

namespace Progress
{
    public class ProgressDataModel
    {
        [JsonProperty] private Dictionary<ResourceType, double> _resources = new Dictionary<ResourceType, double>();
        [JsonProperty] private Dictionary<PurchaseType, PurchaseState> _gamePurchaseStates = new Dictionary<PurchaseType, PurchaseState>();

        [JsonProperty] private BonusInfo _lastReceivedBonus = new BonusInfo(-1, new DateTime());
        [JsonProperty] private int _currentLevelIndex;
        
        [JsonIgnore] public BonusInfo LastReceivedBonus => _lastReceivedBonus;
        [JsonIgnore] public int CurrentLevelIndex => _currentLevelIndex;

        public event Action OnDemandSave;

        public double GetResourceAmount(ResourceType resourceType)
        {
            if (_resources.TryGetValue(resourceType, out var amount))
            {
                return amount;
            }
            else
            {
                return 0.0;
            }
        }

        public void SetResourceAmount(ResourceType resourceType, double amount)
        {
            _resources[resourceType] = amount;
            DemandSave();
        }
        
        public void AppendReceivedBonus(BonusInfo bonusInfo)
        {
            _lastReceivedBonus = bonusInfo;
            DemandSave();
        }

        public void OpenNextLevel()
        {
            _currentLevelIndex++;
            DemandSave();
        }

        public PurchaseState GetGamePurchaseState(PurchaseType purchaseType)
        {
            if (_gamePurchaseStates.TryGetValue(purchaseType, out PurchaseState purchaseState))
            {
                return purchaseState;
            }
            
            return PurchaseState.None;
        }
        
        public void SetGamePurchaseState(PurchaseType purchaseType, PurchaseState purchaseState)
        {
            _gamePurchaseStates[purchaseType] = purchaseState;
            DemandSave();
        }
        
        private void DemandSave()
        {
            OnDemandSave?.Invoke();
        }

    }
}