using AtHash.Apps.ElevatorsDvt.Base.Enumerations;

namespace AtHash.Apps.ElevatorsDvt.Base.Models;

public class ElevatorModel : BaseModel
{
    public int MaximumPassengers { get; set; }
    public bool IsOperational { get; set; }
    public ElevatorMotionStatusEnum MotionStatus { get; set; } = ElevatorMotionStatusEnum.Stationary;
    public bool IsInMotion { get; set; }
    public ElevatorMotionDirectionEnum MotionDirection { get; set; }
    public ElevatorOperationStatusEnum OperationStatus { get; set; }
    //public FloorModel CurrentFloor { get; set; }
    public IList<ElevatorButtonModel> ElevatorButtons { get; set; }
    public IList<PassengerModel> Passengers { get; set; }
    //public IList<FloorModel> DestinationFloors { get; set; }

    public string Id { get; } = Guid.NewGuid().ToString();
    public int CurrentFloorId { get; set; } = 1;
    public FloorModel CurrentFloor { get; set; }
    public ElevatorStatus Status { get; set; } = ElevatorStatus.Idle;
    public List<FloorModel> RequestedFloors { get; } = new List<FloorModel>();
    public List<int> RequestedFloorIds { get; } = new List<int>();
    public ElevatorDirection? CurrentDirection { get; set; }
}
