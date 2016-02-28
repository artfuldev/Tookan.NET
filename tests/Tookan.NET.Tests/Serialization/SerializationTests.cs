using Tookan.NET.Serialization;
using Xunit;

namespace Tookan.NET.Tests.Serialization
{
    public class SerializationTests
    {
        private readonly ISerializationService _service = new SerializationService();

        private class TestClass
        {
            public int GhostTownsCount { get; set; }
        }

        [Fact]
        public void SerializationCanSerializeAnonymousTypes()
        {
            // Arrange
            var obj = new { GhostTownsCount = 5 };

            // Act
            var actual = _service.Serialize(obj);

            // Assert
            Assert.Contains("ghost_towns_count", actual);
        }

        [Fact]
        public void SerializationResolvesToSnakeCase()
        {
            // Arrange
            var obj = new TestClass() {GhostTownsCount = 5};

            // Act
            var actual = _service.Serialize(obj);

            // Assert
            Assert.Contains("ghost_towns_count", actual);
        }

        [Fact]
        public void SerializationResolvesFromSnakeCase()
        {
            // Arrange
            var value = "{\"ghost_towns_count\":5}";

            // Act
            var deserialized = _service.Deserialize<TestClass>(value);
            var actual = deserialized.GhostTownsCount;

            // Assert
            Assert.Equal(5, actual);
        }
    }
}