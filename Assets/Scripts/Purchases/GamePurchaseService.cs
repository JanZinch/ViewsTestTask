using System.Collections.Generic;
using InAppResources;
using Progress;
using UnityEngine;

namespace Purchases
{
    public class GamePurchaseService
    {
        private readonly ResourceService _resourceService;
        private readonly ProgressDataModel _progressDataModel;
        
        private readonly Dictionary<PurchaseType, Purchase> _purchases = new Dictionary<PurchaseType, Purchase>()
        {
            { 
                PurchaseType.Character1, 
                new Purchase(new ResourcePrice(ResourceType.Tickets, 250), () =>
                {
                    Debug.Log("Open character 1");
                })
            },
            { 
                PurchaseType.Character2, 
                new Purchase(new ResourcePrice(ResourceType.Tickets, 500), () =>
                {
                    Debug.Log("Open character 2");
                })
            },
            { 
                PurchaseType.Location1, 
                new Purchase(new ResourcePrice(ResourceType.Tickets, 250), () =>
                {
                    Debug.Log("Open location 1");
                })
            },
            { 
                PurchaseType.Location2, 
                new Purchase(new ResourcePrice(ResourceType.Tickets, 500), () =>
                {
                    Debug.Log("Open location 2");
                })
            },
            { 
                PurchaseType.Location3, 
                new Purchase(new ResourcePrice(ResourceType.Tickets, 500), () =>
                {
                    Debug.Log("Open location 3");
                })
            },
            
        };

        public GamePurchaseService(ResourceService resourceService, ProgressDataModel progressDataModel)
        {
            _resourceService = resourceService;
            _progressDataModel = progressDataModel;
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

        public ResourcePrice GetPrice(PurchaseType purchaseType)
        {
            if (_purchases.TryGetValue(purchaseType, out Purchase purchase))
            {
                return purchase.Price;
            }

            return new ResourcePrice();
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