using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeProg;
using FluentAssertions;
using System;

namespace NodeTests
{
    [TestClass]
    public class ParseHelpersToEnumerableTests
    {
        [TestMethod]
        public void ParseHelpersToEnumerableTestMethod()
        {
            ParseHelpers.ToEnumerable("abcd", 1, 2).Should().Equal(new char[] { 'b', 'c' });
            
            
            var str = "123,456";
            new Action(() => ParseHelpers.ToEnumerable(null, 0, 0)).ShouldThrow<ArgumentNullException>();
            new Action(() => ParseHelpers.ToEnumerable(str, -1, 10)).ShouldThrow<ArgumentException>();
            new Action(() => ParseHelpers.ToEnumerable(str, 0, -1)).ShouldThrow<ArgumentException>();
            new Action(() => ParseHelpers.ToEnumerable(str, 20, 0)).ShouldThrow<ArgumentException>();
            new Action(() => ParseHelpers.ToEnumerable(str, 0, 30)).ShouldThrow<ArgumentException>();
        }
    }
}
