using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Base.EventHandling
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            if (!_handlers.ContainsKey(typeof(TEvent)))
            {
                _handlers[typeof(TEvent)] = new List<Delegate>();
            }

            _handlers[typeof(TEvent)].Add(handler);
        }

        public void Publish<TEvent>(TEvent evt)
        {
            if (_handlers.TryGetValue(typeof(TEvent), out var handlers))
            {
                foreach (var handler in handlers)
                {
                    ((Action<TEvent>)handler)(evt);
                }
            }
        }
    }
}
