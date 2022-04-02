using System;
namespace Pavlychev.Calculus.Functions
{
    /// <summary>
    /// Функция.
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// Отрицательный ли знак у функции?
        /// </summary>
        public bool Negative { get; }

        /// <summary>
        /// Коэффицент функции.
        /// </summary>
        public Number Coefficient { get; }

        /// <summary>
        /// Модуль функции, избавление от минуса в начале.
        /// </summary>
        public IFunction AbsCoefficient { get; }

        /// <summary>
        /// Функция с отрицательным коэффицентом.
        /// </summary>
        public IFunction InvertCoefficient { get; }

        /// <summary>
        /// Производная.
        /// </summary>
        public IFunction Derivative { get; }

        /// <summary>
        /// Первообразная.
        /// </summary>
        public IFunction AntiDerivative { get; }

        /// <summary>
        /// Можно ли записать без скобок?
        /// </summary>
        public bool Single { get; }

        /// <summary>
        /// Получить значение.
        /// </summary>
        /// <param name="variable">Для переменной variable.</param>
        /// <returns>Полученное значение.</returns>
        public Number GetValue(Number variable);

        /// <summary>
        /// Интегрировать.
        /// </summary>
        /// <param name="bottomLimit">Нижний предел.</param>
        /// <param name="upperLimit">Верхний предел.</param>
        /// <returns>Интегрированное значение.</returns>
        public Number Integrate(int bottomLimit, int upperLimit);
    }
}
