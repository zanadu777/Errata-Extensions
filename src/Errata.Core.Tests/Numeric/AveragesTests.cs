using Errata.Numeric;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace NumericExtensionsTests
{
    [TestClass]
    public class AveragesTests
    {
        [TestMethod]
        public void AverageHarmonic()
        {
            double[] doubles = { 4, 36, 45, 50, 75 };
            doubles.AverageHarmonic().Should().Be(15);

            int[] ints = { 4, 36, 45, 50, 75 };
            ints.AverageHarmonic().Should().Be(15);
        }


        [TestMethod]
        public void AverageGeometric()
        {

            double[] doubles = { 4, 36, 45, 50, 75 };
            doubles.AverageGeometric().Should().BeApproximately(30, .0000001);

            int[] ints = { 4, 36, 45, 50, 75 };
            ints.AverageGeometric().Should().BeApproximately(30, .0000001);
        }
    }
}
