using System;
using System.Collections.Generic;
using Purchases;
using UnityEngine;

namespace Views
{
    public class ShopView : BaseView
    {
        [SerializeField] private List<ShopItemView> _shopItemViews;

        private GamePurchaseService _purchaseService;

        [EasyButtons.Button]
        private void RefreshViewsList()
        {
            _shopItemViews = new List<ShopItemView>(GetComponentsInChildren<ShopItemView>());
        }
        
        public void Initialize(GamePurchaseService purchaseService)
        {
            _purchaseService = purchaseService;

            foreach (ShopItemView view in _shopItemViews)
            {
                view.Initialize(_purchaseService);
            }
        }
        
        
    }
}