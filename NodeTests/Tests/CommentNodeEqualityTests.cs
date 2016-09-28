using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using FluentAssertions;

namespace NodeTests
{
    [TestClass]
    public class CommentNodeEqualityTests
    {
        [TestMethod]
        public void CommentNodeEqualityTestMethod()
        {
            new CommentNode("hey").Equals(new CommentNode("hey")).Should().Be(true);
            new CommentNode("hey").Equals(new CommentNode("no")).Should().Be(false);
        }
    }
}