using Purchases.Common;
using Purchases.PurchasesForResource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Purchases.Views
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private PurchaseType _purchaseType;

        [Space] 
        [SerializeField] private GameObject _itemIcon;
        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _purchasedSign;
        [SerializeField] private GameObject _priceView;
        [SerializeField] private GameObject _profitView;
        
        
        [Space]
        [SerializeField] private TextMeshProUGUI _priceTextMesh;
        [SerializeField] private TextMeshProUGUI _profitTextMesh;
        [SerializeField] private TextMeshProUGUI _requiredLevelText;
        
        [Space]
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

            UpdateLevelRequirementView();

        }

        private void UpdateLevelRequirementView()
        {
            if (_purchaseService is PurchaseForResourceService castedPurchaseService)
            {
                int requiredLevel = castedPurchaseService.PurchaseAccessConfig.GetRequiredLevelIndex(_purchaseType);
                int currentLevel = castedPurchaseService.ProgressDataModel.CurrentLevelIndex;

                if (requiredLevel > currentLevel)
                {
                    _itemIcon.SetActive(false);
                    _lock.SetActive(true);
                    _requiredLevelText.gameObject.SetActive(true);
                    _requiredLevelText.SetText($"LV. {requiredLevel}");
                    
                    return;
                }
            }
            
            _itemIcon.SetActive(true);
            _lock.SetActive(false);
            _requiredLevelText.gameObject.SetActive(false);
            _requiredLevelText.SetText(string.Empty);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyClick);
        }

    }
}