using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;

namespace NodeTests
{
    [TestClass]
    public class ListNodeToStringTests
    {
        [TestMethod]
        public void ListNodeToStringTestMethod()
        {
            Assert.AreEqual("[\"\", \"\"]", new ListNode(new[] { new ContentNode(""), new ContentNode("") }).ToString());
            Assert.AreEqual("[\"b\", \"c\"]", new ListNode(new[] { new ContentNode("b"), new ContentNode("c") }).ToString());
            Assert.AreEqual("[\"a\"]", new ListNode(new[] { new ContentNode("a") }).ToString());
            Assert.AreEqual("[\"a\", \"b\", \"c\"]", new ListNode(new[] { new ContentNode("a"), new ContentNode("b"), new ContentNode("c") }).ToString());
            Assert.AreEqual("[[\"a\"]]", new ListNode(new[] { new ListNode(new[] { new ContentNode("a") }) }).ToString());
            Assert.AreEqual("[[\"a\", \"b\"]]", new ListNode(new[] { new ListNode(new[] { new ContentNode("a"), new ContentNode("b") }) }).ToString());

            Assert.AreEqual("[]", new ListNode(new ContentNode[] { }).ToString());
        }
    }
}
