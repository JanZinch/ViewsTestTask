using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class CongratsDailyBonusView : BaseView
    {
        [SerializeField] private Button _hideArea;
        [SerializeField] private DailyBonusView _dailyBonusView;

        private void OnEnable()
        {
            _hideArea.onClick.AddListener(Hide);
        }

        public CongratsDailyBonusView Initialize(int dayIndex, DailyBonus dailyBonus)
        {
            _dailyBonusView.InjectDependencies(dailyBonus).SetDayIndex(dayIndex);
            
            return this;
        }
        
        private void OnDisable()
        {
            _hideArea.onClick.AddListener(Hide);
        }
        
    }
}