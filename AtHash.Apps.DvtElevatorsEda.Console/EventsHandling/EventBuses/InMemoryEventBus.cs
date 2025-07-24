using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling;

public class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<object>> _handlers = new();
    
    public async Task Subscribe<TEvent>(IEventHandler<TEvent> handler)
        where TEvent : IEvent
    {
        // Finding the actual underlying type of the generic TEvent
        var eventType = typeof(TEvent);

        if (!_handlers.ContainsKey(eventType))
        {
            _handlers[eventType] = new List<object>();
        }

        _handlers[eventType].Add(handler);
        await Task.CompletedTask;
    }
    
    public async Task PublishAsync<TEvent>(TEvent evt)
        where TEvent : IEvent
    {
        var eventType = typeof(TEvent);

        if (_handlers.TryGetValue(eventType, out var handlers))
        {
            // Publish to all of the subscribers
            foreach (var handler in handlers)
            {
                // Handle the event
                await ((IEventHandler<TEvent>)handler).HandleAsync(evt);
            }
        }
    }
}