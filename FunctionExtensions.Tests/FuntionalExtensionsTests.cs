using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using FunctionalExtensions;

namespace FunctionExtensions.Tests
{
    [TestClass]
    public class FuntionalExtensionsTests
    {
        [TestMethod]
        public void MapTest()
        {
            "foo".Map(s => s + s).Should().Be("foofoo");
        }

        [TestMethod]
        public void TryMapTest()
        {
            "foo".TryMap(s => s + s).Should().Be("foofoo");
            
            string str = null;
            str.TryMap(s => s + s).Should().BeNull();
            str.TryMap(s => s + s, "bar").Should().Be("bar");

            int? num = null;
            num.TryMap(n => n * 2).Should().Be(null);
            num.TryMap(n => n * 2, 0).Should().Be(0);
        }

        [TestMethod]
        public void PatternMatching()
        {
            "foo".Case("bar").Then("bar").Otherwise("baz").Should().Be("baz");
            "foo".Case("foo").Then("bar").Otherwise("baz").Should().Be("bar");
            "foo".Case(s => s == "foo").Then(s => s + "bar").Otherwise(s => s + "baz").Should().Be("foobar");
            "foo".Case("bar").Then(s => s + "bar").Otherwise(s => s + "baz").Should().Be("foobaz");
            "foo".Case("bar").Then("bar").OtherwiseDefault().Should().BeNull();
            
            Action a = () => "foo".Case("bar").Then("bar").OtherwiseFail("Ooops");

            a.ShouldThrow<Exception>().WithMessage("Ooops");
        }
    }
}
