using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using FluentAssertions;
using System.Linq;
using System.Collections.Generic;

namespace NodeTests
{
    [TestClass]
    public class ParseNodesTests
    {
        [TestMethod]
        public void ParseNodesTestMethod()
        {
            Node.ParseNodes("").Should().Equal();
            Node.ParseNodes("()").Should().Equal(new ListNode());

            Node.ParseNodes(",").Should().Equal(
                new ContentNode(""),
                new ContentNode(""));
            Node.ParseNodes("(),").Should().Equal(
                new ListNode(),
                new ContentNode(""));

            Node.ParseNodes("f,(c)").Should().Equal(
                new ContentNode("f"),
                new ListNode(new ContentNode("c")));

            Node.ParseNodes("f,(c,d)").Should().Equal(
                new ContentNode("f"),
                new ListNode(
                    new ContentNode("c"),
                    new ContentNode("d")));

            Node.ParseNodes("f,(c),d").Should().Equal(
                new ContentNode("f"),
                new ListNode(new ContentNode("c")),
                new ContentNode("d"));

            Node.ParseNodes("a").Should().Equal(new ContentNode("a"));
            Node.ParseNodes("a,b").Should().Equal(
                new ContentNode("a"),
                new ContentNode("b"));


            // "(),(,),(a),(,a),(a,),(())" -> [[],["",""],["a"],["","a"],["a",""],[[]]]

            Node.ParseNodes("(),(,),(a),(,a),(a,),(())").Should().Equal(
                new ListNode(),
                new ListNode(
                    new ContentNode(""),
                    new ContentNode("")),
                new ListNode(
                    new ContentNode("a")),
                new ListNode(
                    new ContentNode(""),
                    new ContentNode("a")),
                new ListNode(
                    new ContentNode("a"),
                    new ContentNode("")),
                new ListNode(
                    new ListNode()));

            Node.ParseNodes("((a,b)),c").Should().Equal(
                new ListNode(
                    new ListNode(
                        new ContentNode("a"),
                        new ContentNode("b")
                        )
                    ),
                new ContentNode("c"));

            new Action(() => Node.ParseNodes("(").Count()).ShouldThrow<FormatException>().WithMessage("closeMarker not found.");
            new Action(() => Node.ParseNodes("(a,b))").Count()).ShouldThrow<FormatException>();
            new Action(() => Node.ParseNodes("()a").Count()).ShouldThrow<FormatException>();

            new Action(() => Node.ParseNodes("a,/*hey*,b").Count()).ShouldThrow<FormatException>();
            new Action(() => Node.ParseNodes("a,/*/*hey*/*/,b").Count()).ShouldThrow<FormatException>();
            new Action(() => Node.ParseNodes("/*hey*/)").Count()).ShouldThrow<FormatException>();

            Node.ParseNodes("/*hey*/").Should().Equal(new CommentNode("hey"));
            Node.ParseNodes("/*/*hey*/").Should().Equal(new CommentNode("/*hey"));

            Node.ParseNodes("f,,((c))", ",,", "((", "))").Should().Equal(
                new ContentNode("f"),
                new ListNode(new ContentNode("c")));

            Node.ParseNodes("f,(c)").Should().Equal(
                new ContentNode("f"),
                new ListNode(new ContentNode("c")));

            Node.ParseNodes("a,/*hey*/,b").Should().Equal(
                new ContentNode("a"),
                new CommentNode("hey"),
                new ContentNode("b"));
            Node.ParseNodes("(/*hey*/)").Should().Equal(
                new ListNode(
                    new CommentNode("hey")));

            Node.ParseNodes("{}").Should().Equal(
               new NamedNode());

            Node.ParseNodes("{a={a=b}}").Should().Equal(
                new NamedNode(
                    new Dictionary<string, Node>
                    {
                        { "a", new NamedNode(new Dictionary<string, Node>
                        {
                            { "a", new ContentNode("b") }
                        })
                        }
                    }));

            Node.ParseNodes("{a=b}").Should().Equal(
                new NamedNode(new Dictionary<string, Node> { { "a", new ContentNode("b") } }));
            /*
             
            "" -> [""]
            "()" -> [[""]]

            "," -> ["",""]
            "()," -> [[""],""]
            
            "f,(c)"
            "f,(c,d)"
            "f,(c),d"
            
            "a" -> ["a"]
            "a,b" -> ["a", "b"]
            "(a),b" -> [["a"], "b"]
            "(a,b),(c,(d,e)),f" -> [["a", "b"], ["c", ["d", "e"]], "f"]
            
            "(" -> exception
            "(a,b))" -> exception
            
            */
        }
    }
}
