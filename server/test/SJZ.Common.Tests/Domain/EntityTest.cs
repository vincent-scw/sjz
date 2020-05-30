using SJZ.Common.Domain;
using SJZ.Common.Extensions;
using System;
using Xunit;

namespace SJZ.Common.Tests
{
    public class EntityTest
    {
        [Fact]
        public void Entity_IsTransient_ShouldAsExpected()
        {
            var dummy = new DummyClass();
            Assert.True(dummy.IsTransient());

            var dummyNotTransient = (new { Id = 1 }).ConvertTo<DummyClass>();
            Assert.False(dummyNotTransient.IsTransient());
        }

        [Fact]
        public void Entity_WhenIdSame_ShouldBeEqual()
        {
            // Arrange
            var entity1 = new DummyClass(1);
            var entity2 = new DummyClass(1);

            // Act
            var result1 = entity1.Equals(entity2);
            var result2 = entity1 == entity2;

            // Assert
            Assert.True(result1);
            Assert.True(result2);
        }

        [Fact]
        public void Entity_WhenIdNotEqual_ShouldNotBeEqual()
        {
            // Arrange
            var entity1 = new DummyClass(1);
            var entity2 = new DummyClass(2);

            // Act
            var result1 = entity1.Equals(entity2);
            var result2 = entity1 != entity2;

            // Assert
            Assert.False(result1);
            Assert.True(result2);
        }
    }

    class DummyClass : Entity<int>
    {
        public DummyClass() { }

        public DummyClass(int id)
        {
            Id = id;
        }
    }
}
