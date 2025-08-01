using AtHash.Apps.ElevatorsDvt.Base.Controllers;
using AtHash.Apps.ElevatorsDvt.Base.Enumerations;
using AtHash.Apps.ElevatorsDvt.Base.EventHandling;
using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Events;
using Moq;

namespace AtHash.Apps.ElevatorsDvt.Base.Tests
{
    public class ElevatorControllerTests
    {
        private readonly Mock<EventBus> _eventBusMock;
        private readonly ElevatorController _controller;

        public ElevatorControllerTests()
        {
            _eventBusMock = new Mock<EventBus>();
            _controller = new ElevatorController(_eventBusMock.Object);
        }

        [Fact]
        public void Constructor_InitializesElevators()
        {
            Assert.Equal(5, _controller.GetElevators().Count);
        }

        [Fact]
        public void HandleElevatorRequest_DispatchesNearestElevator()
        {
            // Arrange
            var elevators = _controller.GetElevators();
            elevators[0].CurrentFloor = 5;  // Nearest to floor 3
            elevators[1].CurrentFloor = 10;

            // Act
            _controller.HandleElevatorRequest(new ElevatorRequestedEvent
            {
                FloorNumber = 3,
                ElevatorDirection = ElevatorDirection.GoingUp
            });

            // Assert
            _eventBusMock.Verify(
                x => x.Publish(It.Is<ElevatorArrivedEvent>(e => e.FloorNumber == 3)),
                Times.Once);
        }

        [Fact]
        public void HandleInsideButtonPress_MovesElevatorToDestination()
        {
            // Arrange
            var elevator = _controller.GetElevators().First();
            elevator.CurrentFloor = 1;

            // Act
            _controller.HandleInsideButtonPress(new InsideButtonPressedEvent
            {
                ElevatorId = elevator.Id,
                DestinationFloor = 10
            });

            // Assert
            _eventBusMock.Verify(
                x => x.Publish(It.Is<ElevatorArrivedEvent>(e => e.FloorNumber == 10)),
                Times.Once);
        }
    }
}
