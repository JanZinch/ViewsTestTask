using System.Collections.Generic;
using Core.Basics;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class LevelMapView : BaseView
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private List<LevelView> _levelViews;

        private ProgressDataModel _progressDataModel;

        [EasyButtons.Button]
        private void RefreshViewsList()
        {
            _levelViews = new List<LevelView>(GetComponentsInChildren<LevelView>());
        }
        
        public LevelMapView Initialize(ProgressDataModel progressDataModel)
        {
            _progressDataModel = progressDataModel;
            UpdateViews();
            ScrollToCurrentLevel();
            return this;
        }
        
        private void OnEnable()
        {
            _homeButton.onClick.AddListener(Hide);
            
            foreach (LevelView view in _levelViews)
            {
                view.OnClick += OnLevelViewClick;
            }
        }
        
        private void ScrollToCurrentLevel()
        {
            int i = _progressDataModel.CurrentLevelIndex;

            if (i >= 0 && i < _levelViews.Count)
            {
                ScrollTo(_levelViews[i].transform);
            }
        }
        
        private void ScrollTo(Transform target)
        {
            RectTransform scrollRectContent = _scrollRect.content;
            Vector2 offset = _scrollRect.transform.InverseTransformPoint(scrollRectContent.position) - 
                             _scrollRect.transform.InverseTransformPoint(target.position);
            
            Vector2 contentAnchoredPosition = scrollRectContent.anchoredPosition;
            contentAnchoredPosition.y = offset.y + _scrollRect.GetComponent<RectTransform>().rect.height / 2.0f;
            scrollRectContent.anchoredPosition = contentAnchoredPosition;
        }

        private void OnLevelViewClick(LevelView view)
        {
            if (_levelViews.IndexOf(view) == _progressDataModel.CurrentLevelIndex)
            {
                _progressDataModel.OpenNextLevel();
                UpdateViews();
            }
        }

        private void UpdateViews()
        {
            int currentLevelIndex = _progressDataModel.CurrentLevelIndex;
            
            for (int i = 0; i < _levelViews.Count; i++)
            {
                _levelViews[i].Initialize(i, currentLevelIndex >= i);
            }
        }

        private void OnDisable()
        {
            foreach (LevelView view in _levelViews)
            {
                view.OnClick -= OnLevelViewClick;
            }
            
            _homeButton.onClick.RemoveListener(Hide);
        }
        
    }
}