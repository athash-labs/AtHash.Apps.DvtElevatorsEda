using System;
using System.Collections.Generic;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.Services;

public sealed class ElevatorControllerService
{
    private ElevatorControllerService()
    {
    }

    public static ElevatorControllerService Instance { get; } = Singleton.Instance;

    public readonly IEnumerable<FloorModel> _floors;
    public readonly IEnumerable<ElevatorModel> _elevators;
    public readonly IEnumerable<PassengerModel> _passengers;

    private class Singleton
    {
        static Singleton()
        {
        }

        internal static readonly ElevatorControllerService Instance = new ElevatorControllerService();
    }
}
