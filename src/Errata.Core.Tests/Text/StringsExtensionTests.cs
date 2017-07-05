using System;
using System.Linq;
using Errata.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Errata.Core.Tests.Text
{
    [TestClass]
    public class StringsExtensionTests
    {
        [TestMethod]
        public void RemoveEmptyTest()
        {
            string text = @"hh

hh";
            text.LineCount().Should().Be(3);
            text.Lines().RemoveEmpty().Count().Should().Be(2);
        }


        [TestMethod]
        public void RemoveEmptyWitParamTest()
        {

            string text = @"hh

hh";
            text.Lines().RemoveEmpty(0).Count().Should().Be(2);
            text.Lines().RemoveEmpty(1).Count().Should().Be(3);
            text.Lines().RemoveEmpty(2).Count().Should().Be(3);
        }

    }
}
