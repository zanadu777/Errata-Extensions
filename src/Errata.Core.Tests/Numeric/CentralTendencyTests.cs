using Errata.Numeric;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Errata.Core.Tests.Numeric
{
    [TestClass]
    public class CentralTendencyTests
    {
        [TestMethod]
        public void PopulationVariance()
        {
            double[] vals = { 12, 23, 7, 88.2 };

            vals.PopulationVariance().Should().BeApproximately(1065.8075, .001);
        }

        [TestMethod]
        public void SampleVariance()
        {
            double[] vals = { 12, 23, 7, 88.2 };
            vals.SampleVariance().Should().BeApproximately(1421.076667, .001);
        }


        [TestMethod]
        public void PopulationStandardDeviation()
        {
            double[] vals = { 12, 23, 7, 88.2 };

            vals.PopulationStandardDeviation().Should().BeApproximately(32.64670734, .001);
        }

        [TestMethod]
        public void SampleStandardDeviation()
        {
            double[] vals = { 12, 23, 7, 88.2 };
            vals.SampleStandardDeviation().Should().BeApproximately(37.69717054, .001);
        }

        [TestMethod]
        public void PopulationSkewness()
        {
            double[] vals = { 12, 23, 7, 88.2 };
            vals.PopulationSkewness().Should().BeApproximately(1.049827, .001);
        }

        //[TestMethod] //needs checking
        //public void SampleSkewness()
        //{
        //    double[] vals = { 12, 23, 7, 88.2 };
        //    vals.SampleSkewness().Should().BeApproximately(1.818353, .001);
        //}

        [TestMethod]
        public void PopulationKurtosis()
        {
            double[] vals = { 12, 23, 7, 88.2 };
            vals.PopulationKurtosis().Should().BeApproximately(2.245648, .001);
        }

        //[TestMethod] //needs checking
        //public void SampleKurtosis()
        //{
        //    double[] vals = { 12, 23, 7, 88.2 };
        //    vals.SampleKurtosis().Should().BeApproximately(3.342362, .001);
        //}
    }
}
