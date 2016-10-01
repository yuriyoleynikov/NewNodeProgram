using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using FluentAssertions;

namespace NodeTests
{
    [TestClass]
    public class NumberNodeToStringTests
    {
        [TestMethod]
        public void NumberNodeToStringTestMethod()
        {
            new NumberNode(1).ToString().Should().Be("1");
            new NumberNode(-1).ToString().Should().Be("-1");
            new NumberNode(0).ToString().Should().Be("0");
        }
    }
}
