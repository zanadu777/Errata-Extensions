using System;
using System.Collections.Generic;
using System.Linq;

namespace Errata.Numeric
{
    public static class Averages
    {
        public static double AverageHarmonic(this IEnumerable<double> values)
        {
            var hSum = 0d;
            double n = 0;
            foreach (var i in values)
            {
                hSum += 1 / i;
                n++;
            }
            return n / hSum;
        }


        public static double AverageHarmonic(this IEnumerable<int> values)
        {
            var doubles = from i in values
                          select (double)i;
            return doubles.AverageHarmonic();

        }

        public static double AverageGeometric(this IEnumerable<double> values)
        {
            var product = 1d;
            double n = 0;
            foreach (var i in values)
            {
                product *= i;
                n++;
            }
            return Math.Pow(product, 1 / n);
        }


        public static double AverageGeometric(this IEnumerable<int> values)
        {
            var doubles = from i in values
                          select (double)i;
            return doubles.AverageGeometric();

        }
    }
}
