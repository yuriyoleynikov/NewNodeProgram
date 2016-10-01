using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using FluentAssertions;

namespace NodeTests
{
    [TestClass]
    public class ParseNodesForNumberNodeTests
    {
        [TestMethod]
        public void ParseNodesForNumberNodeTestMethod()
        {
            Node.ParseNodes("1,2").Should().Equal(
                new NumberNode(1),
                new NumberNode(2));

            Node.ParseNodes("           -12").Should().Equal(new NumberNode(-12));

            Node.ParseNodes("           +12     ").Should().Equal(new NumberNode(12));

            Node.ParseNodes("123").Should().Equal(new NumberNode(123));

            Node.ParseNodes(" +123, -123a,  -123  ").Should().Equal(new NumberNode(123), new ContentNode(" -123a"), new NumberNode(-123));

            Node.ParseNodes("9223372036854775807,9223372036854775808").Should().Equal(new NumberNode(9223372036854775807), new ContentNode("9223372036854775808"));

            Node.ParseNodes("-9223372036854775808,-9223372036854775809").Should().Equal(new NumberNode(-9223372036854775808), new ContentNode("-9223372036854775809"));

            /*
            "   123  "->NumberNode(123)
            " +123, -123a,  -123  "->NumberNode(123), ContentNode(" -123a"), NumberNode(-123)
            "9223372036854775807,9223372036854775808"->NumberNode(9223372036854775807), ContentNode("9223372036854775808")
            "-9223372036854775808,-9223372036854775809"->NumberNode(-9223372036854775808), ContentNode("-9223372036854775809")
            */
        }
    }
}
