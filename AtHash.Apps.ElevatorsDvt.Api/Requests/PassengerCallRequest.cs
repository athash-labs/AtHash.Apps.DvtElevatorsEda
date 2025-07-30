using AtHash.Apps.ElevatorsDvt.Api.Models;

namespace AtHash.Apps.ElevatorsDvt.Api.Requests
{
    public class PassengerCallRequest
    {
        public string? Name { get; set; }
        public int CurrentFloorId { get; set; }
        public FloorModel CurrentFloor { get; set; }
        public int DestinationFloorId { get; set; }
        public FloorModel DestinationFloor { get; set; }
        public int BuildingId { get; set; }
        public BuildingModel Building { get; set; }
    }
}
