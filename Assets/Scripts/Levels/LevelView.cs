using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _numberTextMesh;
        [SerializeField] private Button _completeButton;

        private int _levelIndex;
        private bool _isOpened;
        
        public Action<LevelView> OnClick;

        public LevelView Initialize(int levelIndex, bool isOpened)
        {
            _levelIndex = levelIndex;
            _isOpened = isOpened;
            
            _numberTextMesh.SetText((_levelIndex + 1).ToString());
            return this;
        }
        
        private void OnEnable()
        {
            _completeButton.onClick.AddListener(OnClickInvoke);
        }
        
        private void OnClickInvoke()
        {
            OnClick?.Invoke(this);
        }
        
        private void OnDisable()
        {
            _completeButton.onClick.RemoveListener(OnClickInvoke);
        }
    }
}