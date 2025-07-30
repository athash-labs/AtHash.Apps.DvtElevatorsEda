namespace AtHash.Apps.ElevatorsDvt.Api.Models;

public class PassengerModel : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CurrentFloorId { get; set; }
    public FloorModel CurrentFloor { get; set; }
    public int DestinationFloorId { get; set; }
    public FloorModel DestinationFloor { get; set; }
    public DateTime CallTime { get; set; }
    public int? ElevatorId { get; set; }
    public ElevatorModel? Elevator { get; set; }
}
