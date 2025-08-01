using AtHash.Apps.ElevatorsDvt.Api.Models;

namespace AtHash.Apps.ElevatorsDvt.Api.Dtos
{
    public class ElevatorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CurrentFloorId { get; set; }
        public FloorModel CurrentFloor { get; set; }
        public string Status { get; set; } = string.Empty;
        public int BuildingId { get; set; }
        public BuildingModel Building { get; set; }
        public List<int> PassengerIds { get; set; } = [];
        public List<int> PassengerIdsDown { get; set; } = [];
        public List<int> PassengerIdsUp { get; set; } = [];
        public int MaxCapacity { get; set; }
        public int CurrentLoad => PassengerIds.Count;
        public List<int> RequestedFloors { get; set; } = [];
    }
}
