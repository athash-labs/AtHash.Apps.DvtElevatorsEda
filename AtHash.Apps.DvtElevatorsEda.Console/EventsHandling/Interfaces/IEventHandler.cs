using System.Threading.Tasks;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;

public interface IEventHandler<TEvent>
    where TEvent : IEvent
{
    Task HandleAsync(TEvent evt);
}