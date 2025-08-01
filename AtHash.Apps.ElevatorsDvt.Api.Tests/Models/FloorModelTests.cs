using AtHash.Apps.ElevatorsDvt.Api.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Api.Tests.Models
{
    public class FloorModelTests
    {
        [Fact]
        public void Floor_ShouldInitializeCollections()
        {
            // Arrange & Act
            var floor = new FloorModel();

            // Assert
            floor.WaitingPassengersUp.Should().NotBeNull();
            floor.WaitingPassengersDown.Should().NotBeNull();
        }

        [Fact]
        public void Floor_ButtonDefaultsShouldBeFalse()
        {
            // Arrange & Act
            var floor = new FloorModel();

            // Assert
            floor.UpButtonActive.Should().BeFalse();
            floor.DownButtonActive.Should().BeFalse();
        }
    }
}
