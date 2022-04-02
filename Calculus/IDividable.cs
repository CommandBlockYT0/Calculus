using System;
using Pavlychev.Calculus.Functions;

namespace Pavlychev.Calculus
{
    /// <summary>
    /// Делимое.
    /// </summary>
    public interface IDividable : IFunction
    {
        public double Gcd();
        public IDividable DivideBy(Number n);
        public Number MultiplyBy(Number n);
    }
}
