using SJZ.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SJZ.Common.Tests.Domain
{
    public class EnumerationTest
    {
        [Fact]
        public void Enumeration_ToString_ShouldReturnValue()
        {
            var e = TestEnumeration.Value1;
            var result = e.ToString();

            Assert.Equal("Value1", result);
        }

        [Fact]
        public void Enumeration_GetAll_ShouldReturnAllValues()
        {
            var values = Enumeration.GetAll<TestEnumeration>().ToList();

            Assert.Equal(2, values.Count);
            Assert.Equal(1, values[0].Code);
            Assert.Equal(2, values[1].Code);
        }

        [Fact]
        public void Enumeration_GetFromValue_ShouldSucceed()
        {
            var value = 1;
            var e = Enumeration.FromValue<TestEnumeration>(value);

            Assert.Same(TestEnumeration.Value1, e);
        }

        [Fact]
        public void Enumeration_GetFromValue_NotExists_ShouldBeNull()
        {
            var value = 0;
            var e = Enumeration.FromValue<TestEnumeration>(value);

            Assert.Null(e);
        }

        [Fact]
        public void Enumeration_GetFromDisplayName_ShouldSucceed()
        {
            var name = "Value1";
            var e = Enumeration.FromDisplayName<TestEnumeration>(name);

            Assert.Same(TestEnumeration.Value1, e);
        }

        [Fact]
        public void Enumeration_SameValue_ShouldEqual()
        {
            var e = new TestEnumeration(1, "Value1");
            var result = e.Equals(TestEnumeration.Value1);

            Assert.True(result);
        }

        [Fact]
        public void ParseCodeToEnum_ShouldReturnExpectedEnum()
        {
            var v1 = Enumeration.ParseCodeToEnum<TestEnum>(TestEnumeration.Value1);

            Assert.Equal(TestEnum.Value1, v1);
        }
    }

    class TestEnumeration : Enumeration
    {
        public static TestEnumeration Value1 = new TestEnumeration(1, "Value1");
        public static TestEnumeration Value2 = new TestEnumeration(2, "Value2");
        public TestEnumeration(int code, string name) : base(code, name)
        {
        }
    }

    enum TestEnum
    { 
        Value1 = 1,
        Value2 = 2
    }
}
