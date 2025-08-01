using AtHash.Apps.ElevatorsDvt.Api.Controllers;
using AtHash.Apps.ElevatorsDvt.Api.Data;
using AtHash.Apps.ElevatorsDvt.Api.Models;
using AtHash.Apps.ElevatorsDvt.Api.Services.Interfaces;
using AtHash.Apps.ElevatorsDvt.Api.Tests.Fixtures;
using AtHash.Apps.ElevatorsDvt.Api.Tests.Helpers;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AtHash.Apps.ElevatorsDvt.Api.Tests.Controllers
{
    public class ElevatorsControllerTests : IClassFixture<ElevatorManagementFixture>
    {
        private readonly ElevatorManagementFixture _fixture;
        private readonly BuildingDbContext _context;
        private readonly Mock<IElevatorService> _mockService;
        private readonly ElevatorsController _controller;

        public ElevatorsControllerTests(ElevatorManagementFixture fixture)
        {
            _fixture = fixture;
            _context = DbContextHelper.GetInMemoryDbContext("ElevatorsControllerTests");
            _mockService = new Mock<IElevatorService>();
            _controller = new ElevatorsController(_context, _mockService.Object);
        }

        [Fact]
        public async Task BoardPassenger_ReturnsBadRequest_WhenElevatorFull()
        {
            // Arrange
            var building = _fixture.Fixture.Create<BuildingModel>();
            var elevator = _fixture.Fixture.Build<ElevatorModel>()
                .With(e => e.Building, building)
                .With(e => e.MaximumPassengers, 1)
                .Create();
            var passenger1 = _fixture.Fixture.Create<PassengerModel>();
            var passenger2 = _fixture.Fixture.Create<PassengerModel>();

            elevator.Passengers.Add(passenger1);

            await _context.Buildings.AddAsync(building);
            await _context.Elevators.AddAsync(elevator);
            await _context.Passengers.AddRangeAsync(passenger1, passenger2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.BoardPassenger(elevator.Id, passenger2.Id);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
