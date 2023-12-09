using System;
using System.Collections.Generic;
using InAppResources;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Purchases
{
    public class InAppPurchaseService : IStoreListener, IPurchaseService
    {
        private ResourceService _resourceService;
        private IStoreController _storeController;

        private readonly LinkedList<PendingPair> _pendingPairs = new LinkedList<PendingPair>();
        private readonly Dictionary<PurchaseType, InAppPurchase> _purchases;
        
        private struct PendingPair
        {
            public InAppPurchase Purchase { get; private set; }
            public Action<bool> OnCompleteCallback { get; private set; }

            public PendingPair(InAppPurchase purchase, Action<bool> onCompleteCallback)
            {
                Purchase = purchase;
                OnCompleteCallback = onCompleteCallback;
            }
        }
        
        public InAppPurchaseService(ResourceService resourceService)
        {
            _resourceService = resourceService;
            
            _purchases = new Dictionary<PurchaseType, InAppPurchase>()
            {
                {
                    PurchaseType.EpicChest, 
                    new InAppPurchase("epic_chest", ProductType.Consumable, "1.99$", () =>
                    {
                        _resourceService.AppendResourceAmount(ResourceType.Tickets, 500);
                        Debug.Log("Epic chest opened");
                    })
                },
                {
                    PurchaseType.LuckyChest, 
                    new InAppPurchase("lucky_chest", ProductType.Consumable, "4.99$", () =>
                    {
                        _resourceService.AppendResourceAmount(ResourceType.Tickets, 1200);
                        Debug.Log("Lucky chest opened");
                    })
                }
            };
            
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            foreach (PurchaseType key in _purchases.Keys)
            {
                InAppPurchase purchase = _purchases[key];
                builder.AddProduct(purchase.ProductId, purchase.ProductType);
            }
            
            UnityPurchasing.Initialize(this, builder);
        }
        
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            Debug.Log("In-App Purchasing successfully initialized");
        }
        
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log($"In-App Purchasing initialize failed: {error}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Product product = purchaseEvent.purchasedProduct;
            PendingPair foundPair = default;
            
            foreach (PendingPair pendingPair in _pendingPairs)
            {
                if (pendingPair.Purchase.ProductId == product.definition.id)
                {
                    foundPair = pendingPair;
                    break;
                }
            }

            if (foundPair.Purchase != null)
            {
                foundPair.Purchase.ProcessPurchase?.Invoke();
                foundPair.OnCompleteCallback?.Invoke(true);

                _pendingPairs.Remove(foundPair);
                
                Debug.Log($"Purchase Complete - Product: {product.definition.id}");
                return PurchaseProcessingResult.Complete;
            }
            else
            {
                Debug.LogError($"Process purchase failed - Product: {product.definition.id}");
                return PurchaseProcessingResult.Pending;
            }
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogWarning($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }
        
        public bool IsPurchased(PurchaseType purchaseType)
        {
            if (_purchases.TryGetValue(purchaseType, out InAppPurchase purchase))
            {
                Product product = _storeController.products.WithID(purchase.ProductId);

                if (product.definition.type == ProductType.NonConsumable || 
                    product.definition.type == ProductType.Subscription)
                {
                    return product.hasReceipt;
                }
            }
            
            return false;
        }
        
        public bool CanBePurchased(PurchaseType purchaseType)
        {
            if (IsPurchased(purchaseType))
            {
                return false;
            }

            return _purchases.TryGetValue(purchaseType, out InAppPurchase purchase);
        }

        public void TryPurchase(PurchaseType purchaseType, Action<bool> onCompleteCallback)
        {
            if (!CanBePurchased(purchaseType))
            {
                return;
            }

            if (_purchases.TryGetValue(purchaseType, out InAppPurchase purchase))
            {
                _pendingPairs.AddLast(new PendingPair(purchase, onCompleteCallback));
                _storeController.InitiatePurchase(purchase.ProductId);
            }
        }
        
        public string GetPriceString(PurchaseType purchaseType)
        {
            if (_purchases.TryGetValue(purchaseType, out InAppPurchase purchase))
            {
                return purchase.PriceString;
            }

            return null;
        }
    }
}