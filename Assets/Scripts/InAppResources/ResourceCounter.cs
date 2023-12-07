using System.Globalization;
using TMPro;
using UnityEngine;

namespace InAppResources
{
    public class ResourceCounter : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private TextMeshProUGUI _textMesh;

        private ResourceService _resourceService;
        
        public ResourceCounter InjectDependencies(ResourceService resourceService)
        {
            _resourceService = resourceService;
            UpdateView(_resourceService.GetResourceAmount(_resourceType));
            return this;
        }
        
        private void OnEnable()
        {
            _resourceService.OnResourceAmountChanged += OnResourceAmountChanged;
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

        private void OnDisable()
        {
            _resourceService.OnResourceAmountChanged -= OnResourceAmountChanged;
        }
    }
}