using System.Text;
using Errata.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
 

namespace StringExtensionsTests
{
    [TestClass]
    public class StringBuilderExtensionsTests
    {
        [TestMethod]
        public void RemoveLeft()
        {
            var sb = new StringBuilder();
            sb.Append("123456");
            sb.RemoveLeft(2).ToString().Should().Be("3456");
        }

        [TestMethod]
        public void RemoveRight()
        {
            var sb = new StringBuilder();
            sb.Append("123456");
            sb.RemoveRight(2).ToString().Should().Be("1234");
        }
    }
}
