using System;
using UnityEngine.Purchasing;

namespace Purchases.InAppPurchases
{
    public class InAppPurchase
    {
        public string ProductId { get; private set; }
        public ProductType ProductType { get; private set; }
        public string PriceString { get; private set; }
        public Action ProcessPurchase { get; private set; }
        
        public InAppPurchase(string productId, ProductType productType, string priceString, Action processPurchase)
        {
            ProductId = productId;
            ProductType = productType;
            PriceString = priceString;
            ProcessPurchase = processPurchase;
        }
    }
}