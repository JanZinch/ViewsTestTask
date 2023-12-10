using System.Collections.Generic;
using Bonuses;
using Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class DailyBonusPresenter : BaseView
    {
        [SerializeField] private Button _hideArea;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private List<DailyBonusView> _dailyBonusViews;

        private ViewsFactory _viewsFactory;
        private DailyBonusService _dailyBonusService;
        
        public DailyBonusPresenter InjectDependencies(ViewsFactory viewsFactory, DailyBonusService dailyBonusService)
        {
            _viewsFactory = viewsFactory;
            _dailyBonusService = dailyBonusService;
            
            for (int i = 0; i < _dailyBonusViews.Count; i++)
            {
                _dailyBonusViews[i].Initialize(i, _dailyBonusService.Container.GetBonusByIndex(i));
            }

            UpdateView();
            
            return this;
        }

        private void OnEnable()
        {
            _hideArea.onClick.AddListener(Hide);

            foreach (DailyBonusView view in _dailyBonusViews)
            {
                view.OnClick += OnBonusViewClick;
            }
        }

        private void OnBonusViewClick(int bonusIndex)
        {
            _dailyBonusService.AcceptAvailableBonus();
            Hide();
        }

        private void UpdateView()
        {
            _progressSlider.value = _dailyBonusService.LastReceivedBonusIndex + 1;

            foreach (DailyBonusView view in _dailyBonusViews)
            {
                view.IsAvailable = false;
            }
            
            int availableBonusIndex = _dailyBonusService.AvailableBonusIndex;

            if (availableBonusIndex >= 0 && availableBonusIndex < _dailyBonusViews.Count)
            {
                _dailyBonusViews[availableBonusIndex].IsAvailable = true;
            }
            
        }

        private void OnDisable()
        {
            _hideArea.onClick.RemoveListener(Hide);
            
            foreach (DailyBonusView view in _dailyBonusViews)
            {
                view.OnClick -= OnBonusViewClick;
            }
        }
    }
}