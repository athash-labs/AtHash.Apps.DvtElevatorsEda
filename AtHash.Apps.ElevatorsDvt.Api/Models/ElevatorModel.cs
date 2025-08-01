using AtHash.Apps.ElevatorsDvt.Api.Enumerations;
using AtHash.Apps.ElevatorsDvt.Apis.Enumerations;

namespace AtHash.Apps.ElevatorsDvt.Api.Models;

public class ElevatorModel : BaseModel
{
    public int CurrentFloorId { get; set; } = 1;
    public FloorModel CurrentFloor { get; set; }
    public ElevatorStatusEnum Status { get; set; } = ElevatorStatusEnum.Idle;
    public int BuildingId { get; set; }
    public BuildingModel Building { get; set; }
    public List<PassengerModel> Passengers { get; set; } = [];
    public int MaximumPassengers { get; set; }
    public List<FloorModel> RequestedFloors { get; } = new List<FloorModel>();

    public bool IsOperational { get; set; }
    public ElevatorMotionStatusEnum MotionStatus { get; set; } = ElevatorMotionStatusEnum.Stationary;
    public bool IsInMotion { get; set; }
    public ElevatorMotionDirectionEnum MotionDirection { get; set; }
    public ElevatorOperationStatusEnum OperationStatus { get; set; }
    //public FloorModel CurrentFloor { get; set; }
    public IList<ElevatorButtonModel> ElevatorButtons { get; set; }
    //public IList<FloorModel> DestinationFloors { get; set; }

    public List<int> RequestedFloorIds { get; } = new List<int>();
    public ElevatorDirectionEnum? CurrentDirection { get; set; }
}
