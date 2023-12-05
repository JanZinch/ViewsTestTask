using System;
using EventBus;
using Progress;

namespace InAppResources
{
    public class ResourceService
    {
        private readonly EventBusService _eventBusService;
        private readonly ProgressService _progressService;

        public ResourceService(EventBusService eventBusService, ProgressService progressService)
        {
            _eventBusService = eventBusService;
            _progressService = progressService;
        }

        public void SetResourceAmount(ResourceType resourceType, double amount)
        {
            double oldValue = _progressService.Model.GetResourceAmount(resourceType);
            ChangeValueAndFire(resourceType, oldValue, amount);
        }

        public void AppendResourceAmount(ResourceType resourceType, double amount)
        {
            double oldValue = _progressService.Model.GetResourceAmount(resourceType);
            double newValue = oldValue + amount;
            ChangeValueAndFire(resourceType, oldValue, newValue);
        }
        
        public void SubtractResourceAmount(ResourceType resourceType, double amount)
        {
            double oldValue = _progressService.Model.GetResourceAmount(resourceType);
            double newValue = Math.Clamp(oldValue - amount, 0, double.MaxValue);
            
            ChangeValueAndFire(resourceType, oldValue, newValue);
        }

        public double GetResourceAmount(ResourceType resourceType)
        {
            return _progressService.Model.GetResourceAmount(resourceType);
        }

        private void ChangeValueAndFire(ResourceType resourceType, double oldValue, double newValue)
        {
            _progressService.Model.SetResourceAmount(resourceType, newValue);
            
            _eventBusService.Broadcast(EventKey.ResourceCountChanged, this, 
                new ResourceChangedEventArgs(resourceType, oldValue, newValue));
        }
        
    }
}