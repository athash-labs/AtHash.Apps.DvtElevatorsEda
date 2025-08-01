using AtHash.Apps.ElevatorsDvt.Api.Models;

namespace AtHash.Apps.ElevatorsDvt.Api.Dtos
{
    public class PassengerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CurrentFloorId { get; set; }
        public FloorModel CurrentFloor { get; set; }
        public int DestinationFloorId { get; set; }
        public FloorModel DestinationFloor { get; set; }
        public DateTime CallTime { get; set; }
        public int? ElevatorId { get; set; }
    }
}
