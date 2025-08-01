using AtHash.Apps.ElevatorsDvt.Api.Models;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Api.Tests.Fixtures
{
    public class ElevatorManagementFixture
    {
        public Fixture Fixture { get; }

        public ElevatorManagementFixture()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            // Customize building creation
            Fixture.Customize<BuildingModel>(c => c
                .Without(b => b.Id)
                .Without(b => b.Floors)
                .Without(b => b.Elevators));

            // Customize floor creation
            Fixture.Customize<FloorModel>(c => c
                .Without(f => f.Id)
                .Without(f => f.Building)
                .Without(f => f.WaitingPassengersUp)
                .Without(f => f.WaitingPassengersDown));

            // Customize elevator creation
            Fixture.Customize<ElevatorModel>(c => c
                .Without(e => e.Id)
                .Without(e => e.Building)
                .Without(e => e.Passengers)
                .Without(e => e.RequestedFloors));

            // Customize passenger creation
            Fixture.Customize<PassengerModel>(c => c
                .Without(p => p.Id)
                .Without(p => p.Elevator));
        }
    }
}
