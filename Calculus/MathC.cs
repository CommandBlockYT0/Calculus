using System;

namespace Pavlychev.Calculus
{
    /// <summary>
    /// Класс математических операций с комплексными числами.
    /// </summary>
    public static class MathC
    {
        public static Number Pow(Number n, int power)
        {
            if (n == 0) return (Number)0;

            Number res = (Number)1;

            for (int i = 0; i < power; i++)
            {
                res = res * n;
            }

            return res;
        }

        public static double Abs(Number n) => Math.Sqrt(n.Re * n.Re + n.Im * n.Im);
    }
}
