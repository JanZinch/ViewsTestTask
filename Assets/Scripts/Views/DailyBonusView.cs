using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class DailyBonusView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private Image _resourceIcon;
        [SerializeField] private TextMeshProUGUI _resourceAmountText;
        
        public DailyBonusView InjectDependencies(DailyBonus dailyBonus)
        {
            _resourceAmountText.SetText($"X{dailyBonus.ResourceAmount}");
            return this;
        }

        public void SetDayIndex(int index)
        {
            _headerText.SetText($"DAY{++index}");
        }

    }
}