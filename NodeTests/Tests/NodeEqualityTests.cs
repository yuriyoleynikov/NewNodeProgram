using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;

namespace NodeTests
{
    [TestClass]
    public class NodeEqualityTests
    {
        [TestMethod]
        public void NodeEqualityTestMethod()
        {
            Assert.AreEqual(new ContentNode(""), new ContentNode(""));
            Assert.AreNotEqual(new ContentNode("a"), new ContentNode("b"));
            Assert.AreNotEqual(new ContentNode("a"), null);
            Assert.AreNotEqual(new ContentNode("a"), 10);
        }
    }
}
