using System;
using System.Collections.Generic;
using System.Globalization;
using InGameResources;
using Progress;
using Purchases.Common;
using UnityEngine;

namespace Purchases.PurchasesForResource
{
    public class PurchaseForResourceService : IPurchaseService
    {
        private readonly ResourceService _resourceService;
        private readonly ProgressDataModel _progressDataModel;
        private readonly PurchaseAccessConfig _purchaseAccessConfig;
        
        private readonly Dictionary<PurchaseType, PurchaseForResource> _purchases = new Dictionary<PurchaseType, PurchaseForResource>()
        {
            { 
                PurchaseType.Character1, 
                new PurchaseForResource(new ResourcePack(ResourceType.Tickets, 250), () =>
                {
                    Debug.Log("Open character 1");
                })
            },
            { 
                PurchaseType.Character2, 
                new PurchaseForResource(new ResourcePack(ResourceType.Tickets, 500), () =>
                {
                    Debug.Log("Open character 2");
                })
            },
            { 
                PurchaseType.Location1, 
                new PurchaseForResource(new ResourcePack(ResourceType.Tickets, 250), () =>
                {
                    Debug.Log("Open location 1");
                })
            },
            { 
                PurchaseType.Location2, 
                new PurchaseForResource(new ResourcePack(ResourceType.Tickets, 500), () =>
                {
                    Debug.Log("Open location 2");
                })
            },
            { 
                PurchaseType.Location3, 
                new PurchaseForResource(new ResourcePack(ResourceType.Tickets, 500), () =>
                {
                    Debug.Log("Open location 3");
                })
            },
            
        };

        public PurchaseAccessConfig PurchaseAccessConfig => _purchaseAccessConfig;
        public ProgressDataModel ProgressDataModel => _progressDataModel;
        
        
        public PurchaseForResourceService(ResourceService resourceService, ProgressDataModel progressDataModel, PurchaseAccessConfig purchaseAccessConfig)
        {
            _resourceService = resourceService;
            _progressDataModel = progressDataModel;
            _purchaseAccessConfig = purchaseAccessConfig;
        }
        
        public bool IsPurchased(PurchaseType purchaseType)
        {
            return _progressDataModel.GetGamePurchaseState(purchaseType) == PurchaseState.Bought;
        }
        
        public bool CanBePurchased(PurchaseType purchaseType)
        {
            if (IsPurchased(purchaseType))
            {
                return false;
            }
            
            if (_purchaseAccessConfig.GetRequiredLevelIndex(purchaseType) <= _progressDataModel.CurrentLevelIndex &&
                _purchases.TryGetValue(purchaseType, out PurchaseForResource purchase))
            {
                return purchase.Price.ResourceAmount <= _resourceService.GetResourceAmount(purchase.Price.ResourceType);
            }

            return false;
        }

        public void TryPurchase(PurchaseType purchaseType, Action<bool> onCompleteCallback)
        {
            onCompleteCallback?.Invoke(TryPurchase(purchaseType));
        }
        
        private bool TryPurchase(PurchaseType purchaseType)
        {
            if (!CanBePurchased(purchaseType))
            {
                return false;
            }

            if (_purchases.TryGetValue(purchaseType, out PurchaseForResource purchase))
            {
                _resourceService.SubtractResourceAmount(purchase.Price.ResourceType, purchase.Price.ResourceAmount);
                purchase.ProcessPurchase?.Invoke();
                
                _progressDataModel.SetGamePurchaseState(purchaseType, PurchaseState.Bought);

                return true;
            }

            return false;
        }

        public string GetPriceString(PurchaseType purchaseType)
        {
            return GetPrice(purchaseType).ResourceAmount.ToString(CultureInfo.InvariantCulture);
        }

        public string GetProfitString(PurchaseType purchaseType)
        {
            return string.Empty;
        }
        
        public ResourcePack GetPrice(PurchaseType purchaseType)
        {
            if (_purchases.TryGetValue(purchaseType, out PurchaseForResource purchase))
            {
                return purchase.Price;
            }

            return new ResourcePack();
        }
        
    }
}