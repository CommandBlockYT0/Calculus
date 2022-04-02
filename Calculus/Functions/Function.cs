using System;
namespace Pavlychev.Calculus.Functions
{
    public interface IFunction
    {
        public bool Negative { get;  }

        public Number Coefficient { get; }

        public IFunction AbsCoefficient { get; }

        public IFunction InvertCoefficient { get; }

        /// <summary>
        /// Производная
        /// </summary>
        public IFunction Derivative { get; }

        /// <summary>
        /// Первообразная
        /// </summary>
        public IFunction AntiDerivative { get; }

        public bool Single { get; }

        public Number GetValue(Number variable);

        public Number Integrate(int bottomLimit, int upperLimit);
    }
}
