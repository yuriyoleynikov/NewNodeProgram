using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using FluentAssertions;

namespace NodeTests
{
    [TestClass]
    public class ContentNodeToStringTests
    {
        [TestMethod]
        public void ContentNodeToStringTestMethod()
        {
            new ContentNode("").ToString().Should().Be("\"\"");
            new ContentNode("\"").ToString().Should().Be("\"\\\"\"");
            new ContentNode("\\").ToString().Should().Be("\"\\\\\"");
            new ContentNode("a ").ToString().Should().Be("\"a \"");
            new ContentNode("\"as\"").ToString().Should().Be("\"\\\"as\\\"\"");
        }
    }
}
