using InAppResources;

namespace Purchases
{
    public struct ResourcePrice
    {
        public ResourceType ResourceType { get; private set; }
        public double ResourceAmount { get; private set; }

        public ResourcePrice(ResourceType resourceType, double resourceAmount)
        {
            ResourceType = resourceType;
            ResourceAmount = resourceAmount;
        }
    }
}