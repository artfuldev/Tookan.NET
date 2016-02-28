using System;
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

        private enum TestEnum
        {
            None = 0,
            TwoDays = 1,
            FiveDays = 2
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

        [Theory]
        [InlineData(1)]
        [InlineData((byte)1)]
        [InlineData((sbyte)1)]
        [InlineData((short)1)]
        [InlineData((ushort)1)]
        [InlineData((long)1)]
        [InlineData((ulong)1)]
        [InlineData(1U)]
        [InlineData(1D)]
        [InlineData(1F)]
        [InlineData(true)]
        [InlineData('n')]
        [InlineData("Some random string")]
        public void ConsecutiveSerializationAndDeserializationOfPrimitiveTypesIsIdentityFunction(object input)
        {
            // Arrange
            var expected = input;

            // Act
            var actual = (object) SerializeAndDeserialize((dynamic) input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConsecutiveSerializationAndDeserializationOfGuidIsIdentityFunction()
        {
            var input = Guid.NewGuid();

            // Arrange
            var expected = input;

            // Act
            var actual = SerializeAndDeserialize(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConsecutiveSerializationAndDeserializationOfDateTimeIsIdentityFunction()
        {
            var input = DateTime.Now;

            // Arrange
            var expected = input;

            // Act
            var actual = SerializeAndDeserialize(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConsecutiveSerializationAndDeserializationOfDateTimeOffsetIsIdentityFunction()
        {
            var input = DateTimeOffset.UtcNow;

            // Arrange
            var expected = input;

            // Act
            var actual = SerializeAndDeserialize(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConsecutiveSerializationAndDeserializationOfTimeSpanIsIdentityFunction()
        {
            var input = TimeSpan.FromMinutes(12);

            // Arrange
            var expected = input;

            // Act
            var actual = SerializeAndDeserialize(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConsecutiveSerializationAndDeserializationOfEnumIsIdentityFunction()
        {
            var input = TestEnum.TwoDays;

            // Arrange
            var expected = input;

            // Act
            var actual = SerializeAndDeserialize(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        private T SerializeAndDeserialize<T>(T input)
        {
            return _service.Deserialize<T>(_service.Serialize(input));
        }
    }
}