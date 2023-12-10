using System.Collections.Generic;
using InGameResources;

namespace Purchases.Common
{
    public class ProfitDistributor
    {
        private readonly ResourceService _resourceService;
        
        private readonly Dictionary<PurchaseType, ResourcePack> _resourceProfits = new Dictionary<PurchaseType, ResourcePack>()
        {
            { PurchaseType.EpicChest, new ResourcePack(ResourceType.Tickets, 500) },
            { PurchaseType.LuckyChest, new ResourcePack(ResourceType.Tickets, 1200) },
        };

        public ProfitDistributor(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public ResourcePack GetResourceProfit(PurchaseType purchaseType)
        {
            return _resourceProfits.TryGetValue(purchaseType, out ResourcePack profit) ? profit : new ResourcePack();
        }
        
        public void AppendResourceProfit(PurchaseType purchaseType)
        {
            ResourcePack profit = GetResourceProfit(purchaseType);
            _resourceService.AppendResourceAmount(profit.ResourceType, profit.ResourceAmount);
        }
    }
}