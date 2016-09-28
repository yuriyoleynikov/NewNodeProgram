using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using System.Collections.Generic;

namespace NodeTests
{
    [TestClass]
    public class NamedNodeToStringTests
    {
        [TestMethod]
        public void NamedNodeToStringTestMethod()
        {
            new NamedNode().ToString().Should().Be("{}");

            var entries = new Dictionary<string, Node>();

            entries.Add("a", new ContentNode("a"));
            new NamedNode(entries).ToString().Should().Be("{a = \"a\"}");

            entries.Clear();

            entries.Add("x", new ListNode(new ContentNode("1"), new ContentNode("2")));
            new NamedNode(entries).ToString().Should().Be("{x = [\"1\", \"2\"]}");

            new NamedNode(
                new Dictionary<string, Node>
                {
                    { "x", new ListNode(new ContentNode("1"), new ContentNode("2")) },
                    { "y", new CommentNode("Y")}
                }).ToString()
                .Should().Be("{x = [\"1\", \"2\"], y = /*Y*/}");

            //Assert.AreEqual("[{}]", new ListNode(new NamedNode()).ToString());

            //Assert.AreEqual("[]", new ListNode(new ContentNode[] { }).ToString());
        }
    }
}
