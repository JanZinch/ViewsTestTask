using System.Collections.Generic;
using Core.Basics;
using Purchases.InAppPurchases;
using Purchases.PurchasesForResource;
using UnityEngine;
using UnityEngine.UI;

namespace Purchases.Views
{
    public class ShopView : BaseView
    {
        [SerializeField] private Button _hideButton;
        [SerializeField] private List<ShopItemView> _inAppPurchaseItems;
        [SerializeField] private List<ShopItemView> _purchaseForResourceItems;
        
        private InAppPurchaseService _inAppPurchaseService;
        private PurchaseForResourceService _purchaseForResourceService;
        
        public void Initialize(InAppPurchaseService inAppPurchaseService, PurchaseForResourceService purchaseForResourceService)
        {
            _inAppPurchaseService = inAppPurchaseService;
            _purchaseForResourceService = purchaseForResourceService;
            
            foreach (ShopItemView item in _inAppPurchaseItems)
            {
                item.Initialize(_inAppPurchaseService);
            }
            
            foreach (ShopItemView item in _purchaseForResourceItems)
            {
                item.Initialize(_purchaseForResourceService);
            }
        }

        private void OnEnable()
        {
            _hideButton.onClick.AddListener(Hide);
        }
        
        private void OnDisable()
        {
            _hideButton.onClick.RemoveListener(Hide);
        }
    }
}