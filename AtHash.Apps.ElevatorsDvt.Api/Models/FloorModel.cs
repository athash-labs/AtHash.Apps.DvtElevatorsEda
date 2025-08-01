namespace AtHash.Apps.ElevatorsDvt.Api.Models
{
    public class FloorModel : BaseModel
    {
        public int FloorNumber { get; set; }
        public int BuildingId { get; set; }
        public BuildingModel? Building { get; set; }
        public List<PassengerModel> WaitingPassengers { get; set; } = [];
        public List<PassengerModel> WaitingPassengersDown { get; set; } = [];
        public List<PassengerModel> WaitingPassengersUp { get; set; } = [];
        public bool UpButtonActive { get; set; }
        public bool DownButtonActive { get; set; }
    }
}
