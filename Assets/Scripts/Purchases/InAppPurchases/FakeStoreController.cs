using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Purchases.InAppPurchases
{
    public class FakeStoreController : IStoreController
    {
        private readonly LinkedList<PendingPair> _pendingPairs;
        public ProductCollection products => null;

        public FakeStoreController(LinkedList<PendingPair> pendingPairs)
        {
            _pendingPairs = pendingPairs;
        }
        
        public void InitiatePurchase(string productId, string payload)
        {
            if (_pendingPairs == null)
            {
                return;
            }

            PendingPair foundPair = default;
            
            foreach (PendingPair pendingPair in _pendingPairs)
            {
                if (pendingPair.Purchase.ProductId == productId)
                {
                    foundPair = pendingPair;
                    break;
                }
            }

            if (foundPair.ProcessIfPossible())
            {
                _pendingPairs.Remove(foundPair);
            }
        }

        public void InitiatePurchase(Product product, string payload)
        {
            InitiatePurchase(product.definition.id, payload);
        }
        
        public void InitiatePurchase(Product product)
        {
            InitiatePurchase(product, string.Empty);
        }

        public void InitiatePurchase(string productId)
        {
            InitiatePurchase(productId, string.Empty);
        }

        public void FetchAdditionalProducts(HashSet<ProductDefinition> additionalProducts, Action successCallback,
            Action<InitializationFailureReason> failCallback) { }

        public void ConfirmPendingPurchase(Product product) { }
    }
}