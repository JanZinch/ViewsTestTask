using InGameResources;

namespace Purchases.Common
{
    public struct ResourcePack
    {
        public ResourceType ResourceType { get; private set; }
        public double ResourceAmount { get; private set; }
        
        public ResourcePack(ResourceType resourceType, double resourceAmount)
        {
            ResourceType = resourceType;
            ResourceAmount = resourceAmount;
        }
    }
}