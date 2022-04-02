using System;
namespace Pavlychev.Calculus.Functions
{
    /// <summary>
    /// Класс многочлен — сумма одночленов.
    /// </summary>
    public class Polynomial : IFunction
    {
        /// <summary>
        /// Одночлены
        /// </summary>
        public IFunction[] Monomials { get; set; }

        public IFunction Derivative
        {
            get
            {
                var len = Monomials.Length;
                var monomials = new IFunction[len];

                for (int i = 0; i < len; i++)
                {
                    monomials[i] = Monomials[i].Derivative;
                }

                return new Polynomial(monomials);
            }
        }

        public IFunction AntiDerivative
        {
            get
            {
                var len = Monomials.Length;
                var monomials = new IFunction[len];

                for (int i = 0; i < len; i++)
                {
                    monomials[i] = Monomials[i].AntiDerivative;
                }

                return new Polynomial(monomials);
            }
        }

        public bool Single => false;

        public bool Negative => false;

        public Number Coefficient { get => (Number)1; set { } }

        public IFunction AbsCoefficient
        {
            get
            {
                var len = Monomials.Length;
                var monomials = new IFunction[len];

                for (int i = 0; i < len; i++)
                {
                    monomials[i] = Monomials[i].AbsCoefficient;
                }

                return new Polynomial(monomials);
            }
        }

        public IFunction InvertCoefficient
        {
            get
            {
                var len = Monomials.Length;
                var monomials = new IFunction[len];

                for (int i = 0; i < len; i++)
                {
                    monomials[i] = Monomials[i].InvertCoefficient;
                }

                return new Polynomial(monomials);
            }
        }

        /// <summary>
        /// Создание многочлена.
        /// </summary>
        /// <param name="monomials">Одночлены.</param>
        public Polynomial(params IFunction[] monomials)
        {
            Monomials = monomials;
        }

        public override string ToString()
        {
            string ret = "";

            for (int i = 0; i < Monomials.Length; i++)
            {
                var mon = Monomials[i];
                ret += $"{(i > 0 ? (mon.Negative ? " - " : " + ") : "")}{mon.AbsCoefficient}";
            }

            return ret;
        }

        public Number GetValue(Number variable)
        {
            Number res = (Number)0;

            foreach (var mon in Monomials)
            {
                res = res + mon.GetValue(variable);
            }

            return res;
        }

        public Number Integrate(int bottomLimit, int upperLimit)
        {
            Number res = (Number)0;

            foreach (var mon in Monomials)
            {
                res = res + mon.Integrate(bottomLimit, upperLimit);
            }

            return res;
        }
    }
}
