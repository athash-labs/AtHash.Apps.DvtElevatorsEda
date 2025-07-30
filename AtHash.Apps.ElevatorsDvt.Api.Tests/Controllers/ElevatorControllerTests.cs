using AtHash.Apps.ElevatorsDvt.Base.Enumerations;
using AtHash.Apps.ElevatorsDvt.Base.EventHandling;
using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Events;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Api.Tests.Controllers
{
    public class ElevatorControllerTests : IClassFixture<TestStartup>
    {
        private readonly TestStartup _factory;
        private readonly Mock<EventBus> _eventBusMock;

        public ElevatorControllerTests(TestStartup factory)
        {
            _factory = factory;
            _eventBusMock = _factory.EventBusMock;
        }

        [Fact]
        public async Task RequestElevator_PublishesEvent()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new { FloorNumber = 5, ElevatorDirection = ElevatorDirection.GoingUp };

            // Act
            var response = await client.PostAsJsonAsync("/api/elevator/request", request);

            // Assert
            response.EnsureSuccessStatusCode();
            _eventBusMock.Verify(
                x => x.Publish(It.Is<ElevatorRequestedEvent>(e =>
                    e.FloorNumber == 5 && e.ElevatorDirection == ElevatorDirection.GoingUp)),
                Times.Once);
        }

        [Fact]
        public async Task SelectFloor_PublishesEvent()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new { ElevatorId = "elev-1", DestinationFloor = 10 };

            // Act
            var response = await client.PostAsJsonAsync("/api/elevator/select-floor", request);

            // Assert
            response.EnsureSuccessStatusCode();
            _eventBusMock.Verify(
                x => x.Publish(It.Is<InsideButtonPressedEvent>(e =>
                    e.ElevatorId == "elev-1" && e.DestinationFloorId == 10)),
                Times.Once);
        }
    }
}
