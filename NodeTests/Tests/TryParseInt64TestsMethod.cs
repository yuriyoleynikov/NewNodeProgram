using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using FluentAssertions;
using System;
using System.Linq;

namespace NodeTests
{
    [TestClass]
    public class TryParseInt64TestsMethod
    {
        [TestMethod]
        public void TryParseInt64TestMethod()
        {
            var str = "123,456";
            new Action(() => ParseHelpers.TryParseInt64(null, 0, 0)).ShouldThrow<ArgumentNullException>();
            new Action(() => ParseHelpers.TryParseInt64(str, -1, 10)).ShouldThrow<ArgumentException>();
            new Action(() => ParseHelpers.TryParseInt64(str, 0, -1)).ShouldThrow<ArgumentException>();
            new Action(() => ParseHelpers.TryParseInt64(str, 20, 0)).ShouldThrow<ArgumentException>();
            new Action(() => ParseHelpers.TryParseInt64(str, 0, 30)).ShouldThrow<ArgumentException>();

            str = " 1 ";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), 1);
            Assert.AreNotEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), null);

            str = "           -12";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), -12);

            str = "           +12     ";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), 12);

            str = "123";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), 123);

            str = " -123a";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), null);

            str = " +123 -123  ";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), null);

            str = "  9223372036854775807     ";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), 9223372036854775807);

            str = "          9223372036854775808      ";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), null);

            str = " -9223372036854775808         ";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), -9223372036854775808);

            str = "         -9223372036854775809     ";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), null);


            str = "1 ";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length, false, false), null);

            str = "           -12";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length, true), -12);
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length, false), null);

            str = "           +12     ";
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length), 12);
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length, false, false), null);
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length, true, false), null);
            Assert.AreEqual(ParseHelpers.TryParseInt64(str, 0, str.Length, false, true), null);

            new Action(() => ParseHelpers.TryParseInt64(" ", 1, 1)).ShouldThrow<ArgumentException>();

            ParseHelpers.TryParseInt64("", 0, 0).Should().Be(null);

            /*
            "   123  "->NumberNode(123)
            " +123, -123a,  -123  "->NumberNode(123), ContentNode(" -123a"), NumberNode(-123)
            "9223372036854775807,9223372036854775808"->NumberNode(9223372036854775807), ContentNode("9223372036854775808")
            "-9223372036854775808,-9223372036854775809"->NumberNode(-9223372036854775808), ContentNode("-9223372036854775809")

            if (input == null)
                throw new FormatException("input == null");
            if (startIndex < 0)
                throw new FormatException("startIndex < 0");
            if (length < 0)
                throw new FormatException("length < 0");
            if (startIndex > input.Length)
                throw new FormatException("startIndex > input.Length");
            if (length > input.Length)
                throw new FormatException("length > input.Length");

            */
        }
    }
}
