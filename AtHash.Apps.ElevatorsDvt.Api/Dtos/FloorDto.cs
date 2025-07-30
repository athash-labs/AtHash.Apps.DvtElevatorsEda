using AtHash.Apps.ElevatorsDvt.Api.Models;

namespace AtHash.Apps.ElevatorsDvt.Api.Dtos
{
    public class FloorDto
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public int BuildingId { get; set; }
        public BuildingModel Building { get; set; }
        public List<int> WaitingPassengerIds { get; set; } = [];
        public List<PassengerModel> WaitingPassengers { get; set; } = [];
    }
}
