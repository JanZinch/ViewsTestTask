using System;
using UnityEngine.Purchasing;

namespace Purchases
{
    public class InAppPurchase
    {
        public string ProductId { get; private set; }
        public ProductType ProductType { get; private set; }
        public Action ProcessPurchase { get; private set; }
        
        public InAppPurchase(string productId, ProductType productType, Action processPurchase)
        {
            ProductId = productId;
            ProductType = productType;
            ProcessPurchase = processPurchase;
        }
    }
}