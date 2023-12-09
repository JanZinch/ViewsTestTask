using System;
namespace Purchases
{
    public class PurchaseForResource
    {
        public ResourcePrice Price { get; private set; }
        public Action ProcessPurchase { get; private set; }
        
        public PurchaseForResource(ResourcePrice price, Action processPurchase)
        {
            Price = price;
            ProcessPurchase = processPurchase;
        }
        
    }
}