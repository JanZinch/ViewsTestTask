using Bonuses.Models;
using Core.Basics;
using UnityEngine;
using UnityEngine.UI;

namespace Bonuses.Views
{
    public class CongratsDailyBonusView : BaseView
    {
        [SerializeField] private Button _hideArea;
        [SerializeField] private DailyBonusView _dailyBonusView;

        private void OnEnable()
        {
            _hideArea.onClick.AddListener(Hide);
        }

        public CongratsDailyBonusView Initialize(int bonusIndex, DailyBonus dailyBonus)
        {
            _dailyBonusView.Initialize(bonusIndex, dailyBonus);
            return this;
        }
        
        private void OnDisable()
        {
            _hideArea.onClick.AddListener(Hide);
        }
        
    }
}