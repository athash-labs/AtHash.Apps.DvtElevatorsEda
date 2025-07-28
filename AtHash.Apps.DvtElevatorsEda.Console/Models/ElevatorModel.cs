using System.Collections.Generic;
using AtHash.Apps.DvtElevatorsEda.Enumerations;

namespace AtHash.Apps.DvtElevatorsEda.Models;

public class ElevatorModel : BaseModel
{
    public int MaximumPassengers { get; set; }
    public bool IsOperational { get; set; }
    public ElevatorMotionStatusEnum MotionStatus { get; set; } = ElevatorMotionStatusEnum.Stationary;
    public bool IsInMotion { get; set; }
    public ElevatorMotionDirectionEnum MotionDirection { get; set; }
    public ElevatorOperationStatusEnum OperationStatus { get; set; }
    public FloorModel CurrentFloor { get; set; }
    public IList<ElevatorButtonModel> ElevatorButtons { get; set; }
    public IList<PassengerModel> Passengers { get; set; }
    //public IList<FloorModel> DestinationFloors { get; set; }
}
