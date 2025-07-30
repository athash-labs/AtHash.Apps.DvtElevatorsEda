using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;

public interface IEventHandler<TEvent>
    where TEvent : IEvent
{
    void Handle(TEvent evt);
}