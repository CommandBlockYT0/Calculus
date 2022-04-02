using System;
using Pavlychev.Calculus;
using Pavlychev.Calculus.Functions;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynomial polynomial = new(new Monomial((Number)8, 'x', (Number)1), new Monomial((Number)(-1), 'x', (Number)2));

            Number a = new(5, 4);
            Number b = new(2, 1);
            Console.WriteLine($"{a} + {b} = {a + b}");

            Console.WriteLine($"F({polynomial}) = {polynomial.AntiDerivative}");
            Console.WriteLine($"∫₀⁸({polynomial})dx = {polynomial.Integrate(0, 8)}");
        }
    }
}
