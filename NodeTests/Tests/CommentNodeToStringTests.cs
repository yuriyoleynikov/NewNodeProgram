using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using FluentAssertions;

namespace NodeTests
{
    [TestClass]
    public class CommentNodeToStringTests
    {
        [TestMethod]
        public void CommentNodeToStringTestMethod()
        {
            new CommentNode("hey").ToString().Should().Be("/*hey*/");            
        }
    }
}
