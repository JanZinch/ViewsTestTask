using System.Collections.Generic;
using InAppResources;
using Progress;

namespace Purchases
{
    public class GamePurchaseService
    {
        private ResourceService _resourceService;
        private ProgressDataModel _progressDataModel;
        
        //private List<Purchase> _purchases;
        private Dictionary<PurchaseType, Purchase> _purchases;

        public GamePurchaseService(ResourceService resourceService, ProgressDataModel progressDataModel)
        {
            _resourceService = resourceService;
            _progressDataModel = progressDataModel;


            /*_purchases = new List<Purchase>()
            {
                new Purchase(()=> resourceService.GetResourceAmount(ResourceType.Tickets) > )
                
                
            }*/
        }


        public bool CanBePurchased(PurchaseType purchaseType)
        {
            if (_purchases.TryGetValue(purchaseType, out Purchase purchase))
            {
                return CanBePurchased(purchase);
            }

            return false;
        }
        
        private bool CanBePurchased(Purchase purchase)
        {
            return purchase.Price.ResourceAmount <= _resourceService.GetResourceAmount(purchase.Price.ResourceType);
        }
        
        public bool IsPurchased(PurchaseType purchaseType)
        {
            return _progressDataModel.GetGamePurchaseState(purchaseType) == PurchaseState.Bought;
        }

        
        public bool TryPurchase(PurchaseType purchaseType)
        {
            if (IsPurchased(purchaseType))
            {
                return false;
            }

            if (_purchases.TryGetValue(purchaseType, out Purchase purchase))
            {
                if (!CanBePurchased(purchase))
                {
                    return false;
                }
                
                _resourceService.SubtractResourceAmount(purchase.Price.ResourceType, purchase.Price.ResourceAmount);
                purchase.PurchaseAction?.Invoke();
                
                _progressDataModel.SetGamePurchaseState(purchaseType, PurchaseState.Bought);

                return true;
            }

            return false;
        }

    }
}