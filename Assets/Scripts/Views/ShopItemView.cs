using System;
using System.Globalization;
using Purchases;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private PurchaseType _purchaseType;
        [SerializeField] private GameObject _purchasedSign;
        [SerializeField] private GameObject _priceView;
        [SerializeField] private TextMeshProUGUI _priceTextMesh;
        [SerializeField] private Button _buyButton;
        
        private GamePurchaseService _purchaseService;
        
        public ShopItemView Initialize(GamePurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
            UpdateView();
            return this;
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyClick);
        }

        private void OnBuyClick()
        {
            if (_purchaseService.TryPurchase(_purchaseType))
            {
                UpdateView();
            }
        }
        
        private void UpdateView()
        {
            bool isPurchased = _purchaseService.IsPurchased(_purchaseType);
            
            _purchasedSign.SetActive(isPurchased);
            _priceView.SetActive(!isPurchased);
            
            if (!isPurchased)
            {
                _priceTextMesh.SetText(_purchaseService.GetPrice(_purchaseType).ResourceAmount
                    .ToString(CultureInfo.InvariantCulture));
            }
            
        }
        
        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyClick);
        }

    }
}