using System.Collections.Generic;
using InAppResources;
using Newtonsoft.Json;

namespace Progress
{
    public class ProgressDataModel
    {
        [JsonProperty] private Dictionary<ResourceType, double> _resources = new Dictionary<ResourceType, double>();
        
        public LastReceivedBonusData LastReceivedBonus { get; private set; }

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
            
            //DemandSave();
        }
        
    }
}