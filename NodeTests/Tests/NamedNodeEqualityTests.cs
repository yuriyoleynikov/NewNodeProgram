using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using FluentAssertions;

namespace NodeTests
{
    [TestClass]
    public class NamedNodeEqualityTests
    {
        [TestMethod]
        public void NamedNodeEqualityTestMethod()
        {
            new NamedNode().Equals(new NamedNode()).Should().Be(true);
            new NamedNode().Equals(new ListNode()).Should().Be(false);

            var entries = new Dictionary<string, Node>();
            entries.Add("a", new ContentNode("a"));
            new NamedNode(entries).Equals(new NamedNode(entries)).Should().Be(true);

            entries.Clear();
            entries.Add("c", new ListNode(new ListNode()));
            new NamedNode(entries).Equals(new NamedNode(entries)).Should().Be(true);

            entries.Clear();
            entries.Add("c", new NamedNode());
            new NamedNode(entries).Equals(new NamedNode(entries)).Should().Be(true);

            new NamedNode(new Dictionary<string, Node> { { "b", new ContentNode("a") } })
                .Equals(new NamedNode(new Dictionary<string, Node> { { "b", new ContentNode("a") } }))
                .Should().Be(true);

            new NamedNode(
                new Dictionary<string, Node> { { "a", new ContentNode("a") } })
                .Equals(
                new NamedNode(new Dictionary<string, Node> { { "b", new ContentNode("a") } }))
                .Should().Be(false);

            new NamedNode(new Dictionary<string, Node> { { "b", new ContentNode("a") }, { "a", new ContentNode("b") } })
                .Equals(new NamedNode(new Dictionary<string, Node> { { "a", new ContentNode("b") }, { "b", new ContentNode("a") } }))
                .Should().Be(true);
        }
    }
}