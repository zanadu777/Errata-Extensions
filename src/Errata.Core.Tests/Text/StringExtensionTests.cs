using Errata.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Errata.Core.Tests.Text
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void Lines()
        {
            var srt = "1\n2";
            srt.Lines().Count.Should().Be(2);

            srt = "1\r\n2";
            srt.Lines().Count.Should().Be(2);
        }
    }
}
