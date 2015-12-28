using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Errata.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Errata.Core.Tests.Text
{
    [TestClass]
    public class RegexExtensionsTests
    {
        [TestMethod]
        public void GetGroupTest()
        {
            var text = "server=myServerAddress;Database=myDataBase;User Id=myUsername;Password = myPassword;";
            var rx = new Regex (@"Database\s*=\s*(?<Name>.*?);");
            var extracted = rx.GroupValue(text,"Name");
            extracted.Should().Be("myDataBase");
        }

    }
}
