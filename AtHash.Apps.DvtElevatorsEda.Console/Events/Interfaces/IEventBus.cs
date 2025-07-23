using System;
using System.Threading.Tasks;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : IEvent;
    void Subscribe<TEvent>(IEventHandler<TEvent> handler)
        where TEvent : IEvent;
}
