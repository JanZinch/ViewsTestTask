using System.Collections.Generic;
using Purchases;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class ShopView : BaseView
    {
        [SerializeField] private Button _hideButton;
        [SerializeField] private List<ShopItemView> _inAppPurchaseItems;
        [SerializeField] private List<ShopItemView> _purchaseForResourceItems;
        
        private InAppPurchaseService _inAppPurchaseService;
        private PurchaseForResourceService _purchaseForResourceService;
        
        public void Initialize(InAppPurchaseService inAppPurchaseService, PurchaseForResourceService purchaseForResourceService, PurchaseAccessConfig purchaseAccessConfig)
        {
            _inAppPurchaseService = inAppPurchaseService;
            _purchaseForResourceService = purchaseForResourceService;
            
            foreach (ShopItemView item in _inAppPurchaseItems)
            {
                item.Initialize(_inAppPurchaseService, purchaseAccessConfig);
            }
            
            foreach (ShopItemView item in _purchaseForResourceItems)
            {
                item.Initialize(_purchaseForResourceService, purchaseAccessConfig);
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