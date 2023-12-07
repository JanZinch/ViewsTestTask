using System;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class DailyBonusView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private Image _background;
        [SerializeField] private Image _resourceIcon;
        [SerializeField] private TextMeshProUGUI _resourceAmountText;
        [SerializeField] private Button _button;
        
        public event Action<int> OnClick;
        private int _index;

        private bool _isAvailable;

        public bool IsAvailable
        {
            get => _isAvailable;

            set
            {
                _isAvailable = value;

                _background.color = _isAvailable ? 
                    new Color(1.0f, 1.0f, 1.0f, 1.0f) : 
                    new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickInvoke);
        }

        private void OnClickInvoke()
        {
            OnClick?.Invoke(_index);
        }

        public DailyBonusView InjectDependencies(DailyBonus dailyBonus)
        {
            _resourceAmountText.SetText($"X{dailyBonus.ResourceAmount}");
            return this;
        }

        public void SetDayIndex(int index)
        {
            _index = index;
            _headerText.SetText($"DAY{_index + 1}");
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickInvoke);
        }
    }
}