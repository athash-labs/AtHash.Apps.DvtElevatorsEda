using AtHash.Apps.ElevatorsDvt.Api.Models;

namespace AtHash.Apps.ElevatorsDvt.Api.Dtos
{
    public class FloorDto
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public int BuildingId { get; set; }
        public BuildingModel Building { get; set; }
        public List<int> WaitingPassengerDownIds { get; set; } = [];
        public List<int> WaitingPassengerUpIds { get; set; } = [];
        public bool UpButtonActive { get; set; }
        public bool DownButtonActive { get; set; }
    }
}
