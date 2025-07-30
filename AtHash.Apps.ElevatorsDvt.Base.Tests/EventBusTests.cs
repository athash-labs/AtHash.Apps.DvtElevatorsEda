using AtHash.Apps.ElevatorsDvt.Base.EventHandling;

namespace AtHash.Apps.ElevatorsDvt.Base.Tests
{
    public class EventBusTests
    {
        [Fact]
        public void Publish_InvokesSubscribedHandler()
        {
            // Arrange
            var eventBus = new EventBus();
            bool wasCalled = false;
            eventBus.Subscribe<TestEvent>(e => wasCalled = true);

            // Act
            eventBus.Publish(new TestEvent());

            // Assert
            Assert.True(wasCalled);
        }

        [Fact]
        public void Publish_DoesNotInvokeUnrelatedHandlers()
        {
            // Arrange
            var eventBus = new EventBus();
            bool wasCalled = false;
            eventBus.Subscribe<OtherTestEvent>(e => wasCalled = true);

            // Act
            eventBus.Publish(new TestEvent());

            // Assert
            Assert.False(wasCalled);
        }

        [Fact]
        public void Publish_InvokesMultipleHandlers()
        {
            // Arrange
            var eventBus = new EventBus();
            int callCount = 0;
            eventBus.Subscribe<TestEvent>(e => callCount++);
            eventBus.Subscribe<TestEvent>(e => callCount++);

            // Act
            eventBus.Publish(new TestEvent());

            // Assert
            Assert.Equal(2, callCount);
        }

        private class TestEvent { }
        private class OtherTestEvent { }
    }
}
