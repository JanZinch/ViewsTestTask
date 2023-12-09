using System;
using Progress;

namespace InAppResources
{
    public class ResourceService
    {
        private readonly ProgressDataModel _progressDataModel;

        public EventHandler<ResourceChangedEventArgs> OnResourceAmountChanged;

        public ResourceService(ProgressDataModel progressService)
        {
            _progressDataModel = progressService;
        }

        public void SetResourceAmount(ResourceType resourceType, double amount)
        {
            double oldValue = _progressDataModel.GetResourceAmount(resourceType);
            ChangeValueAndFire(resourceType, oldValue, amount);
        }

        public void AppendResourceAmount(ResourceType resourceType, double amount)
        {
            double oldValue = _progressDataModel.GetResourceAmount(resourceType);
            double newValue = oldValue + amount;
            ChangeValueAndFire(resourceType, oldValue, newValue);
        }
        
        public void SubtractResourceAmount(ResourceType resourceType, double amount)
        {
            double oldValue = _progressDataModel.GetResourceAmount(resourceType);
            double newValue = Math.Clamp(oldValue - amount, 0, double.MaxValue);
            
            ChangeValueAndFire(resourceType, oldValue, newValue);
        }

        public double GetResourceAmount(ResourceType resourceType)
        {
            return _progressDataModel.GetResourceAmount(resourceType);
        }

        private void ChangeValueAndFire(ResourceType resourceType, double oldValue, double newValue)
        {
            _progressDataModel.SetResourceAmount(resourceType, newValue);
            OnResourceAmountChanged?.Invoke(this, new ResourceChangedEventArgs(resourceType, oldValue, newValue));
        }
        
    }
}