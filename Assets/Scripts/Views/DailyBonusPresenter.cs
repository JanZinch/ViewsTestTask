using System;
using System.Collections.Generic;
using Models;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class DailyBonusPresenter : BaseView
    {
        [SerializeField] private Button _hideArea;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private List<DailyBonusView> _dailyBonusViews;
        
        private ProgressService _progressService;
        private DailyBonusesContainer _dailyBonusesContainer;
        
        public DailyBonusPresenter InjectDependencies(ProgressService progressService, DailyBonusesContainer dailyBonusesContainer)
        {
            _progressService = progressService;
            _dailyBonusesContainer = dailyBonusesContainer;
            
            UpdateView();
            
            return this;
        }

        private void OnEnable()
        {
            _hideArea.onClick.AddListener(Hide);
        }

        private void UpdateView()
        {
            _progressSlider.value = _progressService.Model.LastReceivedBonus.Index;
            
            for (int i = 0; i < _dailyBonusViews.Count; i++)
            {
                _dailyBonusViews[i]
                    .InjectDependencies(_dailyBonusesContainer.GetBonusByIndex(i))
                    .SetDayIndex(i);
            }
        }
        
        private void OnDisable()
        {
            _hideArea.onClick.RemoveListener(Hide);
        }
    }
}