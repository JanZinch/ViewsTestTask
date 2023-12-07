using System;
using System.Collections.Generic;
using InAppResources;
using Newtonsoft.Json;

namespace Progress
{
    public class ProgressDataModel
    {
        [JsonProperty] private Dictionary<ResourceType, double> _resources = new Dictionary<ResourceType, double>();
        [JsonProperty] private BonusInfo _lastReceivedBonus = new BonusInfo(-1, new DateTime());
        [JsonProperty] private int _currentLevelIndex;
        
        [JsonIgnore] public BonusInfo LastReceivedBonus => _lastReceivedBonus;
        [JsonIgnore] public int CurrentLevelIndex => _currentLevelIndex;

        public event Action OnDemandSave;
        
        
        public ProgressDataModel()
        {
            // Read data
        }

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

        public void DemandSave()
        {
            OnDemandSave?.Invoke();
        }

    }
}