using System;

namespace Purchases.Common
{
    public interface IPurchaseService
    {
        public bool IsAlreadyPurchased(PurchaseType purchaseType);
        public bool CanBePurchased(PurchaseType purchaseType);
        public void TryPurchase(PurchaseType purchaseType, Action<bool> onCompleteCallback = null);
        public string GetPriceString(PurchaseType purchaseType);
        public string GetProfitString(PurchaseType purchaseType);
    }
}