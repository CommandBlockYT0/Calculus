using System;
using Pavlychev.Calculus;
using Pavlychev.Calculus.Functions;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynomial polynomial = new(new Monomial((Number)8, 'x'), new Monomial((Number)(-1), 'x', (Number)2));

            Console.WriteLine("Сложение и вычитание действительных чисел:");

            Number a = new(5);
            Number b = new(4);
            Number c = a + b;
            Number d = a - b;
            Console.WriteLine($"{a} + {b} = {c}");
            Console.WriteLine($"{a} - {b} = {d}");


            Console.WriteLine("\nСложение и вычитание комплексных чисел:");

            a = new(5, 1);
            b = new(4, 2);
            c = a + b;
            d = a - b;
            Console.WriteLine($"{a} + {b} = {c}");
            Console.WriteLine($"{a} - {b} = {d}");


            Console.WriteLine("\nСложение и вычитание действительных дробей с разными знаменателями:");

            a = new(new(2), new(3));
            b = new(new(5), new(8));
            c = a + b;
            d = a - b;
            Console.WriteLine($"{a} + {b} = {c}");
            Console.WriteLine($"{a} - {b} = {d}");


            Console.WriteLine("\nСложение и вычитание комплексных дробей с разными знаменателями:");

            a = new(5, 7, 2, 2);
            b = new(4, 2, 3, 8);
            c = a + b;
            d = a - b;
            Console.WriteLine($"{a} + {b} = {c}");
            Console.WriteLine($"{a} - {b} = {d}");



            Console.WriteLine("\nПроизводная, первообразная и определённое интегрирование:");
            Console.WriteLine($"f(x) = {polynomial}");
            Console.WriteLine($"f'(x) = {polynomial.Derivative}");
            Console.WriteLine($"F(x) = {polynomial.AntiDerivative}");
            Console.WriteLine($"∫₀⁸({polynomial})dx = {polynomial.Integrate(0, 8)} ≈ {polynomial.Integrate(0, 8).ToDouble(1)}");
        }
    }
}
