using AtHash.Apps.ElevatorsDvt.Api.Models;
using AtHash.Apps.ElevatorsDvt.Api.Tests.Fixtures;
using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Api.Tests.Models
{
    public class BuildingModelTests : IClassFixture<ElevatorManagementFixture>
    {
        private readonly ElevatorManagementFixture _fixture;

        public BuildingModelTests(ElevatorManagementFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void BuildingModel_ShouldInitializeCollections()
        {
            // Arrange & Act
            var building = new BuildingModel();

            // Assert
            building.Floors.Should().NotBeNull();
            building.Elevators.Should().NotBeNull();
        }

        [Fact]
        public void Building_ShouldInitializeCollections()
        {
            // Arrange & Act
            var building = _fixture.Fixture.Create<BuildingModel>();

            // Assert
            building.Floors.Should().NotBeNull();
            building.Elevators.Should().NotBeNull();
        }

        [Fact]
        public void Building_NameShouldNotBeEmpty()
        {
            // Arrange
            var building = _fixture.Fixture.Create<BuildingModel>();

            // Act & Assert
            building.Name.Should().NotBeNullOrWhiteSpace();
        }
    }
}
