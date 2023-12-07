using System;
using EventBus;
using Progress;

namespace InAppResources
{
    public class ResourceService
    {
        //private readonly EventBusService _eventBusService;
        private readonly ProgressDataModel _progressDataModel;

        public EventHandler<ResourceChangedEventArgs> OnResourceAmountChanged;

        public ResourceService(/*EventBusService eventBusService,*/ ProgressDataModel progressService)
        {
            //_eventBusService = eventBusService;
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
            
            /*_eventBusService.Broadcast(EventKey.ResourceCountChanged, this, 
                new ResourceChangedEventArgs(resourceType, oldValue, newValue));*/
        }
        
    }
}