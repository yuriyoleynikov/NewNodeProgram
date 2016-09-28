using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;

namespace NodeTests
{
    [TestClass]
    public class ListNodeEqualityTests
    {
        [TestMethod]
        public void ListNodeEqualityTestMethod()
        {            
            Assert.AreEqual(new ListNode(new[] { new ContentNode("") }), new ListNode(new[] { new ContentNode("") }));
            Assert.AreNotEqual(new ListNode(new[] { new ContentNode("") }), new ListNode(new[] { new ContentNode(""), new ContentNode("") }));
            Assert.AreNotEqual(new ListNode(new[] { new ContentNode("a") }), new ListNode(new[] { new ContentNode("b") }));
            Assert.AreNotEqual(null, new ListNode(new[] { new ContentNode("a") }));
            Assert.AreNotEqual(new ListNode(new[] { new ContentNode("a") }), 20);

        }
    }
}
