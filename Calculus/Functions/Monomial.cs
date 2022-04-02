using System;
namespace Pavlychev.Calculus.Functions
{
    /// <summary>
    /// Класс одночлен вида axⁿ.
    /// </summary>
    public class Monomial : IFunction, IDividable
    {
        /// <summary>
        /// Коэффицент a.
        /// </summary>
        Number Coefficient { get; set; }

        /// <summary>
        /// Переменная x.
        /// </summary>
        char? Variable { get; set; } = null;

        /// <summary>
        /// Показатель степени n.
        /// </summary>
        Number Exponent { get; set; }

        public IFunction Derivative => new Monomial(Coefficient * Exponent, Variable ?? 'x', Exponent - 1);
        public IFunction AntiDerivative => new Monomial(new(Coefficient, Exponent + 1), Variable ?? 'x', Exponent + 1);

        public bool Single => Coefficient.Single;

        public bool Negative => Coefficient.Negative;

        public IFunction AbsCoefficient => new Monomial(new(MathC.Abs(Coefficient), reDen: Coefficient.ReDen, imDen: Coefficient.ImDen, var: Coefficient.Variable, varDen: Coefficient.VariableDen), Variable, Exponent);

        public IFunction InvertCoefficient => new Monomial(-Coefficient, Variable, Exponent);

        Number IFunction.Coefficient => Coefficient;

        /// <summary>
        /// Создание одночлена axⁿ.
        /// </summary>
        /// <param name="a">Коэффицент.</param>
        /// <param name="x">Переменная.</param>
        /// <param name="n">Показатель степени.</param>
        public Monomial(Number a, char? x, Number n)
        {
            Coefficient = a;
            Variable = x;
            Exponent = n;
        }

        /// <summary>
        /// Одночлен aⁿ.
        /// </summary>
        /// <param name="a">Коэффицент.</param>
        /// <param name="n">Показатель степени.</param>
        public Monomial(Number a, Number n)
        {
            Coefficient = a;
            Exponent = n;
        }

        public override string ToString()
        {
            var c = Coefficient;
            if (c == 0 || Variable == null) return "0";

            return $"{(c == 1 ? "" : (c == -1 ? "-" : $"{(c.Single ? "" : "(")}{c}{(c.Single ? "" : ")")}"))}{Variable}{(Exponent == 1 ? "" : Exponent.ToSuperScriptString())}";
        }

        public double Gcd() => Coefficient.Gcd();

        public IDividable DivideBy(Number n) => new Monomial(new Number(Coefficient, n), Variable, Exponent);

        public Number MultiplyBy(Number n)
        {
            throw new NotImplementedException();
        }

        public Number GetValue(Number variable) => Coefficient * MathC.Pow(variable, (int)Exponent.Re);

        public Number Integrate(int bottomLimit, int upperLimit)
        {
            var v2 = AntiDerivative.GetValue((Number)upperLimit);
            var v1 = AntiDerivative.GetValue((Number)bottomLimit);
            var val = v2 - v1;

            return val;
        }
    }
}
