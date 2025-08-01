using AtHash.Apps.ElevatorsDvt.Base.Enumerations;

namespace AtHash.Apps.ElevatorsDvt.Base.EventHandling.Events
{
    public class ElevatorRequestedEvent
    {
        public int FloorNumber { get; set; }
        public ElevatorDirectionEnum ElevatorDirection { get; set; }
    }
}
