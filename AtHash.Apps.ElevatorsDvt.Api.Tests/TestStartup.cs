using AtHash.Apps.ElevatorsDvt.Base.Controllers;
using AtHash.Apps.ElevatorsDvt.Base.EventHandling;
using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace AtHash.Apps.ElevatorsDvt.Api.Tests
{
    public class TestStartup : WebApplicationFactory<Program>
    {
        public Mock<EventBus> EventBusMock { get; } = new Mock<EventBus>();
        public Mock<ElevatorController> ElevatorControllerMock { get; } = new Mock<ElevatorController>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IEventBus>(EventBusMock.Object);
                services.AddSingleton<IElevatorController>(ElevatorControllerMock.Object);
            });
        }
    }
}
