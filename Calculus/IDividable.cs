using System;
using Pavlychev.Calculus.Functions;

namespace Pavlychev.Calculus
{
    /// <summary>
    /// Делимая функция.
    /// </summary>
    public interface IDividable : IFunction
    {
        /// <summary>
        /// Получение наибольшего общего делителя.
        /// </summary>
        /// <returns>Действительное число.</returns>
        public double Gcd();

        /// <summary>
        /// Деление функции на n.
        /// </summary>
        /// <param name="n">Комплексное число.</param>
        /// <returns>Разделённая делимая функция.</returns>
        public IDividable DivideBy(Number n);

        /// <summary>
        /// Умножение числа на n.
        /// </summary>
        /// <param name="n">Комплексное число.</param>
        /// <returns>Умноженное число.</returns>
        public Number MultiplyBy(Number n);
    }
}
