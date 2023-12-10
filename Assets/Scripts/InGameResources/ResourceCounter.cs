using System.Globalization;
using TMPro;
using UnityEngine;

namespace InGameResources
{
    public class ResourceCounter : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private TextMeshProUGUI _textMesh;

        private ResourceService _resourceService;

        public ResourceCounter Initialize(ResourceService resourceService)
        {
            _resourceService = resourceService;
            _resourceService.OnResourceAmountChanged += OnResourceAmountChanged;
            
            UpdateView(_resourceService.GetResourceAmount(_resourceType));
            
            return this;
        }
        
        private void OnResourceAmountChanged(object sender, ResourceChangedEventArgs eventArgs)
        {
            if (eventArgs.ResourceType == _resourceType)
            {
                UpdateView(eventArgs.NewValue);
            }
        }

        private void UpdateView(double value)
        {
            _textMesh.SetText(value.ToString(CultureInfo.InvariantCulture));
        }

        private void OnDestroy()
        {
            if (_resourceService != null)
            {
                _resourceService.OnResourceAmountChanged -= OnResourceAmountChanged;
            }
        }
    }
}