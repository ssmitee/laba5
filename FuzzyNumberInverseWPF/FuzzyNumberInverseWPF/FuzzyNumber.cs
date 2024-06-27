using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyNumberInverseWPF
{
    public struct FuzzyNumber
    {
        public double A { get; set; }
        public double B { get; set; }

        public FuzzyNumber(double a, double b)
        {
            A = a;
            B = b;
        }

        public override string ToString()
        {
            return $"({A}, {B})";
        }
    }

    public static class FuzzyNumberOperations
    {
        public static FuzzyNumber Inverse(FuzzyNumber number)
        {
            if (number.A == 0 || number.B == 0)
            {
                throw new ArgumentException("Числа A и B не должны быть равны нулю.");
            }

            double inverseA = 1 / number.B;
            double inverseB = 1 / number.A;

            return new FuzzyNumber(inverseA, inverseB);
        }
    }
}
