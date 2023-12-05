using System;
using System.Linq;

namespace EventBus
{
    public class EventBusService
    {
        private readonly InternalEventBus _internalEventBus;

        public EventBusService()
        {
            _internalEventBus = new InternalEventBus(Enum.GetValues(typeof(EventKey)).Cast<int>());
        }

        public void AddListener(EventKey eventKey, EventHandler eventHandler)
        {
            _internalEventBus.AddListener((int)eventKey, eventHandler);
        }
        
        public void Broadcast(EventKey eventKey, object sender = null, EventArgs args = null)
        {
            _internalEventBus.Broadcast((int)eventKey, sender, args);
        }
        
        public void RemoveListener(EventKey eventKey, EventHandler eventHandler)
        {
            _internalEventBus.RemoveListener((int)eventKey, eventHandler);
        }
    }
}