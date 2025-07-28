using System;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.Models;

public class ElevatorLocationModel
{
    public ElevatorModel Elevator { get; set; }
    public FloorModel CurrentFloor { get; set; }
}
