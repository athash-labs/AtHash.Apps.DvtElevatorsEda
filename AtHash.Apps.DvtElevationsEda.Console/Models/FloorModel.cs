namespace AtHash.Apps.DvtElevatorsEda.Models;

public class FloorModel : BaseModel
{
    public IEnumerable<ElevatorModel> Elevators { get; set; } = [];
    public IEnumerable<FloorButtonModel> FloorButtons { get; set; }
    public IEnumerable<PassengerModel> WaitingPassengers { get; set; } = [];
}
