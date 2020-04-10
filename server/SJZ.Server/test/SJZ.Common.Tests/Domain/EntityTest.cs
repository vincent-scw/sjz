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
    }

    class DummyClass : Entity<int>
    {

    }
}
