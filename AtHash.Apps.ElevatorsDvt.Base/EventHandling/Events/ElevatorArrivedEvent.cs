using AtHash.Apps.ElevatorsDvt.Base.Models;

namespace AtHash.Apps.ElevatorsDvt.Base.EventHandling.Events
{
    public class ElevatorArrivedEvent
    {
        public int FloorNumber { get; set; }
        public string ElevatorId { get; set; }
        public ElevatorModel Elevator { get; set; }
    }
}
