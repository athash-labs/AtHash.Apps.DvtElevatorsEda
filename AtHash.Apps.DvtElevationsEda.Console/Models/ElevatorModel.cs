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
    public IEnumerable<ElevatorButtonModel> ElevatorButtons { get; set; }
    public IEnumerable<PassengerModel> Passengers { get; set; }
    public IEnumerable<FloorModel> DestinationFloors { get; set; }
}
