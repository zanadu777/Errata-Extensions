using System;
using System.Collections.Generic;
using System.Linq;

namespace Errata.Numeric
{
    public static class CentralTendency
    {

        public static double PopulationVariance(this IEnumerable<double> values)
        {
            var moments = Moments2(values);

            return moments[2] / moments[0] - Math.Pow(moments[1] / moments[0], 2);
        }

        public static double SampleVariance(this IEnumerable<double> values)
        {
            var moments = Moments2(values);

            return moments[2] / (moments[0] - 1) - Math.Pow(moments[1] / moments[0], 2) * (moments[0] / (moments[0] - 1));

        }


        public static double PopulationStandardDeviation(this IEnumerable<double> values)
        {
            return Math.Sqrt(values.PopulationVariance());
        }

        public static double SampleStandardDeviation(this IEnumerable<double> values)
        {
            return Math.Sqrt(values.SampleVariance());
        }


        public static double PopulationSkewness(this IEnumerable<double> values)
        {
            var vals = values.ToArray(); // can be more efficient
            var mean = vals.Average();
            var fourth = 0d;
            foreach (var value in vals)
            {
                fourth += Math.Pow(value - mean, 3);
            }
            return fourth / (vals.Length * Math.Pow(vals.PopulationStandardDeviation(), 3));
        }

        public static double SampleSkewness(this IEnumerable<double> values)
        {
            var vals = values.ToArray(); // can be more efficient
            var mean = vals.Average();
            var third = 0d;
            foreach (var value in vals)
            {
                third += Math.Pow(value - mean, 3);
            }
            return third / ((vals.Length - 1) * Math.Pow(vals.SampleStandardDeviation(), 3));
        }


        public static double PopulationKurtosis(this IEnumerable<double> values)
        {
            var vals = values.ToArray(); // can be more efficient
            var mean = vals.Average();
            var fourth = 0d;
            foreach (var value in vals)
            {
                fourth += Math.Pow(value - mean, 4);
            }
            return fourth / (vals.Length * Math.Pow(vals.PopulationVariance(), 2));
        }

        public static double SampleKurtosis(this IEnumerable<double> values)
        {
            var vals = values.ToArray(); // can be more efficient
            var mean = vals.Average();
            var fourth = 0d;
            foreach (var value in vals)
            {
                fourth += Math.Pow(value - mean, 4);
            }
            return fourth / ((vals.Length - 1) * Math.Pow(vals.SampleVariance(), 2));
        }

        #region Moments
        private static double[] Moments2(IEnumerable<double> values)
        {
            var m1 = 0d;
            var m2 = 0d;
            var count = 0d;
            foreach (var value in values)
            {
                count += 1;
                m1 += value;
                m2 += Math.Pow(value, 2);
            }

            return new double[] { count, m1, m2 };
        }

        private static double[] Moments3(IEnumerable<double> values)
        {
            var m1 = 0d;
            var m2 = 0d;
            var m3 = 0d;
            var count = 0d;
            foreach (var value in values)
            {
                count += 1;
                m1 += value;
                m2 += Math.Pow(value, 2);
                m3 += Math.Pow(value, 3);
            }

            return new double[] { count, m1, m2, m3 };
        }

        private static double[] Moments4(IEnumerable<double> values)
        {
            var m1 = 0d;
            var m2 = 0d;
            var m3 = 0d;
            var m4 = 0d;
            var count = 0d;
            foreach (var value in values)
            {
                count += 1;
                m1 += value;
                m2 += Math.Pow(value, 2);
                m3 += Math.Pow(value, 3);
                m4 += Math.Pow(value, 4);
            }

            return new double[] { count, m1, m2, m3, m4 };
        }

        #endregion
    }
}
