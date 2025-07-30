using AtHash.Apps.ElevatorsDvt.Base.Enumerations;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Console.Services.Interfaces
{
    public interface IElevatorService
    {
        Task CallElevator(int floorNumber, ElevatorDirection direction);
        Task SelectFloor(string elevatorId, int destinationFloor);
        Task GetStatus();
    }
}
