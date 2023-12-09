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
        [SerializeField] private GameObject _profitView;
        [SerializeField] private TextMeshProUGUI _priceTextMesh;
        [SerializeField] private TextMeshProUGUI _profitTextMesh;
        [SerializeField] private Button _buyButton;
        
        private IPurchaseService _purchaseService;
        
        public ShopItemView Initialize(IPurchaseService purchaseService)
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
            _purchaseService.TryPurchase(_purchaseType, result =>
            {
                if (result)
                {
                    UpdateView();
                }
            });
        }
        
        private void UpdateView()
        {
            bool isPurchased = _purchaseService.IsPurchased(_purchaseType);
            
            _purchasedSign.SetActive(isPurchased);
            _priceView.SetActive(!isPurchased);
            
            if (!isPurchased)
            {
                _priceTextMesh.SetText(_purchaseService.GetPriceString(_purchaseType));
            }

            string possibleProfit = _purchaseService.GetProfitString(_purchaseType);

            if (!string.IsNullOrEmpty(possibleProfit))
            {
                _profitView.SetActive(true);
                _profitTextMesh.SetText($"x{possibleProfit}");
            }
            else
            {
                _profitView.SetActive(false);
                _profitTextMesh.SetText(string.Empty);
            }

        }
        
        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyClick);
        }

    }
}