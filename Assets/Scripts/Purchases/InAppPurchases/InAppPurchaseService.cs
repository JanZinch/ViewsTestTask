using System;
using System.Collections.Generic;
using System.Globalization;
using Purchases.Common;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Purchases.InAppPurchases
{
    public class InAppPurchaseService : IStoreListener, IPurchaseService
    {
        private readonly ProfitDistributor _profitDistributor;
        private IStoreController _storeController;

        private readonly Dictionary<PurchaseType, InAppPurchase> _purchases;
        private readonly LinkedList<PendingPair> _pendingPairs = new LinkedList<PendingPair>();
        
        public InAppPurchaseService(ProfitDistributor profitDistributor)
        {
            _profitDistributor = profitDistributor;
            
            _purchases = new Dictionary<PurchaseType, InAppPurchase>()
            {
                {
                    PurchaseType.EpicChest, 
                    new InAppPurchase("epic_chest", ProductType.Consumable, "1.99$", () =>
                    {
                        _profitDistributor.AppendResourceProfit(PurchaseType.EpicChest);
                    })
                },
                {
                    PurchaseType.LuckyChest, 
                    new InAppPurchase("lucky_chest", ProductType.Consumable, "4.99$", () =>
                    {
                        _profitDistributor.AppendResourceProfit(PurchaseType.LuckyChest);
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
            Debug.LogWarning($"In-App Purchasing initialize failed: {error}");
            _storeController = new FakeStoreController(_pendingPairs);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Product purchasedProduct = purchaseEvent.purchasedProduct;
            PendingPair foundPair = default;
            
            foreach (PendingPair pendingPair in _pendingPairs)
            {
                if (pendingPair.Purchase.ProductId == purchasedProduct.definition.id)
                {
                    foundPair = pendingPair;
                    break;
                }
            }

            if (foundPair.ProcessIfPossible())
            {
                _pendingPairs.Remove(foundPair);
                
                Debug.Log($"Purchase Complete - Product: {purchasedProduct.definition.id}");
                return PurchaseProcessingResult.Complete;
            }
            else
            {
                Debug.LogError($"Process purchase failed - Product: {purchasedProduct.definition.id}");
                return PurchaseProcessingResult.Pending;
            }
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogWarning($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }
        
        public bool IsAlreadyPurchased(PurchaseType purchaseType)
        {
            if (_purchases.TryGetValue(purchaseType, out InAppPurchase purchase))
            {
                if (_storeController is FakeStoreController)
                {
                    return false;
                }

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
            if (IsAlreadyPurchased(purchaseType))
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

        public string GetProfitString(PurchaseType purchaseType)
        {
            return _profitDistributor.GetResourceProfit(purchaseType).ResourceAmount.ToString(CultureInfo.InvariantCulture);
        }
    }
}