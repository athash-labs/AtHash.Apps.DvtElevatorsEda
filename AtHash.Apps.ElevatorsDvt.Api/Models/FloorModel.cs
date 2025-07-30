namespace AtHash.Apps.ElevatorsDvt.Api.Models
{
    public class FloorModel
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public int BuildingId { get; set; }
        public BuildingModel? Building { get; set; }
        public List<PassengerModel> WaitingPassengers { get; set; } = new List<PassengerModel>();
    }
}
