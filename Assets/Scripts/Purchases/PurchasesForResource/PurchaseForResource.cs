using System;

namespace Purchases.PurchasesForResource
{
    public class PurchaseForResource
    {
        public ResourcePack Price { get; private set; }
        public Action ProcessPurchase { get; private set; }
        
        public PurchaseForResource(ResourcePack price, Action processPurchase)
        {
            Price = price;
            ProcessPurchase = processPurchase;
        }
        
    }
}