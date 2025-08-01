using Microsoft.AspNet.SignalR.Client.Hubs;
using Moq;
using System.Data;

namespace AtHash.Apps.ElevatorsDvt.Console.Tests
{
    public class MainFormTests
    {
        private readonly Mock<HttpClient> _httpClientMock;
        private readonly Mock<IHubConnection> _hubConnectionMock;
        private readonly MainForm _form;

        public MainFormTests()
        {
            _httpClientMock = new Mock<HttpClient>();
            _hubConnectionMock = new Mock<IHubConnection>();

            _form = new MainForm(_httpClientMock.Object, _hubConnectionMock.Object);
        }

        [Fact]
        public void CallElevator_SendsCorrectRequest()
        {
            // Arrange
            _form.SetTestValues(floor: 5, directionUp: true);

            // Act
            _form.CallElevatorTest();

            // Assert
            _httpClientMock.Verify(x => x.PostAsJsonAsync(
                "elevator/request",
                It.Is<object>(o =>
                    (int)o.GetType().GetProperty("FloorNumber").GetValue(o) == 5 &&
                    (Direction)o.GetType().GetProperty("Direction").GetValue(o) == Direction.Up)),
                Times.Once);
        }

        [Fact]
        public void SelectFloor_SendsCorrectRequest()
        {
            // Arrange
            _form.SetTestValues(elevatorId: "elev-1", destinationFloor: 10);

            // Act
            _form.SelectFloorTest();

            // Assert
            _httpClientMock.Verify(x => x.PostAsJsonAsync(
                "elevator/select-floor",
                It.Is<object>(o =>
                    (string)o.GetType().GetProperty("ElevatorId").GetValue(o) == "elev-1" &&
                    (int)o.GetType().GetProperty("DestinationFloor").GetValue(o) == 10)),
                Times.Once);
        }
    }

    // Testable MainForm subclass
    public partial class TestableMainForm : MainForm
    {
        public TestableMainForm(HttpClient httpClient, IHubConnection hubConnection)
            : base(httpClient, hubConnection)
        {
        }

        public void CallElevatorTest() => btnCallElevator_Click(null, EventArgs.Empty);
        public void SelectFloorTest() => btnSelectFloor_Click(null, EventArgs.Empty);

        public void SetTestValues(int? floor = null, bool? directionUp = null,
            string elevatorId = null, int? destinationFloor = null)
        {
            if (floor.HasValue) nudCallFloor.Value = floor.Value;
            if (directionUp.HasValue) rbUp.Checked = directionUp.Value;
            _currentElevatorId = elevatorId;
            if (destinationFloor.HasValue) nudDestinationFloor.Value = destinationFloor.Value;
        }
    }
}
