using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;

namespace NodeTests
{
    [TestClass]
    public class NumberNodeEqualityTests
    {
        [TestMethod]
        public void NumberNodeEqualityTestMethod()
        {
            Assert.AreEqual(new NumberNode(123), new NumberNode(123));

            Assert.AreNotEqual(new NumberNode(123), 123);
        }
    }
}
