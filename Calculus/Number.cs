using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Pavlychev.Calculus.Functions;

namespace Pavlychev.Calculus
{
    /// <summary>
    /// Число.
    /// </summary>
    public struct Number : IFunction, IDividable
    {
        public static Number Zero => new(0, 0);
        public static Number One => new(1, 0);
        public static Number I => new(0, 1);

        public double Re { get; set; }
        public double Im { get; set; }
        public char? Variable { get; set; }

        public double ReDen { get; set; }
        public double ImDen { get; set; }
        public char? VariableDen { get; set; }

        public Number Denominator { get => new(ReDen, ImDen, varDen: VariableDen); set { ReDen = value.Re; ImDen = value.Im; VariableDen = value.Variable; } }

        public bool Fraction => ReDen != 1 || ImDen != 1;
        public bool Complex => Im != 0;
        public bool Single => ((Re != 0 && Im == 0 && Variable == null) || (Re == 0 && Im != 0 && Variable == null) || (Re == 0 && Im == 0 && Variable != null)) && !Fraction;

        public IFunction Derivative => (Number)0;
        public IFunction AntiDerivative => new Monomial(new Number(Re, Im), Variable ?? 'x', (Number)(Variable == null ? 1 : 2));

        public bool Negative => Re < 0;

        public Number Coefficient => this;

        public IFunction AbsCoefficient => this;

        public IFunction InvertCoefficient => -this;

        /// <summary>
        /// Создание дроби.
        /// </summary>
        /// <param name="numerator">Числитель.</param>
        /// <param name="denominator">Знаменатель.</param>
        public Number(Number numerator, Number denominator) : this(numerator.Re, numerator.Im, denominator.Re, denominator.Im, numerator.Variable, denominator.Variable) { }

        /// <summary>
        /// Расширенное создание комплексного числа.
        /// </summary>
        /// <param name="re">Действительная часть числителя.</param>
        /// <param name="im">Мнимая часть числителя.</param>
        /// <param name="reDen">Действительная часть знаменателя.</param>
        /// <param name="imDen">Мнимая часть знаменателя.</param>
        /// <param name="var">Переменная числителя.</param>
        /// <param name="varDen">Переменная знаменателя.</param>
        public Number(double re, double im = 0, double reDen = 1, double imDen = 1, char? var = null, char? varDen = null)
        {
            var gcdRe = Gcd(Math.Abs(re) * 1000, Math.Abs(reDen) * 1000);
            var gcdIm = Gcd(Math.Abs(im) * 1000, Math.Abs(imDen) * 1000);
            var gcd = Gcd(gcdRe, gcdIm) / 1000;

            Re = re / gcd;
            Im = im / gcd;

            ReDen = reDen / gcd;
            ImDen = imDen / gcd;

            Variable = var;
            VariableDen = varDen;
        }

        public override string ToString()
        {
            var s = Single;
            var ds = Denominator.Single;

            var result = Complex ? $"({Re} + {Im}i)" : $"{Re}";

            if (Fraction)
                result += $"/{(ds ? "" : "(")}{Denominator}{(ds ? "" : ")")}";

            if (Variable != null) result += Variable;

            return result;
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj is int || obj is float || obj is decimal || obj is long || obj is double)
            {
                var n = (double)obj;
                return !Complex && Re == n;
            }
            if (obj is Number)
            {
                var n = (Number)obj;
                return Re == n.Re && Im == n.Im && ReDen == n.ReDen && ImDen == n.ImDen;
            }

            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Метод для получения числа "сверху".
        /// </summary>
        /// <returns>Строка числа сверху.</returns>
        public string ToSuperScriptString()
        {
            string normalString = ToString();
            string ret = "";

            Dictionary<char, char> superScript = new Dictionary<char, char>
            {
                { '0', '⁰' },
                { '1', '¹' },
                { '2', '²' },
                { '3', '³' },
                { '4', '⁴' },
                { '5', '⁵' },
                { '6', '⁶' },
                { '7', '⁷' },
                { '8', '⁸' },
                { '9', '⁹' },
                { 'i', 'ᶦ' },
                { '-', '⁻' },
                { '(', '⁽' },
                { ')', '⁾' },
            };

            for (int i = 0; i < normalString.Length; i++)
            {
                if (superScript.TryGetValue(normalString[i], out char val))
                    ret += val;
                else ret += normalString[i];
            }

            return ret;
        }

        public IDividable DivideBy(Number n) => new Number(Re / n.Re);

        public Number MultiplyBy(Number n) => this * n;

        public double Gcd() => Gcd(Re, Im);

        public static Number Divide(Number a, Number b) => new Number((a.Re * b.Re + a.Im * b.Im) / (b.Re * b.Re + b.Im * b.Im), (a.Im * b.Re - a.Re * b.Im) / (b.Re * b.Re + b.Im * b.Im));

        public Number GetValue(Number variable) => Variable == null ? new Number(new(Re, Im), Denominator) : new Number(new(Re, Im), Denominator) * variable;
        public double? ToDouble(double variable) => Complex && Denominator.Complex ? null : (Variable == null ? Re / ReDen : (Re / ReDen) * variable);

        public Number Integrate(int bottomLimit, int upperLimit) => AntiDerivative.GetValue((Number)bottomLimit) - AntiDerivative.GetValue((Number)bottomLimit);

        public static double Gcd(double a, double b)
        {
            if (a < b) return Gcd(b, a);

            if (Math.Abs(b) < 0.001) return a;
            else return Gcd(b, a - Math.Floor(a / b) * b);
        }

        public static Number operator +(Number a, Number b) => new(a.Re * b.ReDen + a.ReDen * b.Re, a.Im * b.ImDen + a.ImDen * b.Im, a.ReDen * b.ReDen, a.ImDen * b.ImDen, a.Variable, a.VariableDen);
        public static Number operator +(Number a, double b) => a + (Number)b;
        public static Number operator +(double a, Number b) => (Number)a + b;

        public static Number operator -(Number n) => new(-n.Re, n.Im);

        public static Number operator -(Number a, Number b) => new(a.Re * b.ReDen - a.ReDen * b.Re, a.Im * b.ImDen - a.ImDen * b.Im, a.ReDen * b.ReDen, a.ImDen * b.ImDen, a.Variable, a.VariableDen);
        public static Number operator -(Number a, double b) => a - (Number)b;
        public static Number operator -(double a, Number b) => (Number)a - b;

        public static Number operator *(Number a, Number b) => new(a.Re * b.Re, a.Im * b.Im, a.ReDen * b.ReDen, a.ImDen * b.ImDen, a.Variable, a.VariableDen);
        public static Number operator *(Number a, double b) => a * (Number)b;
        public static Number operator *(double a, Number b) => (Number)a * b;

        public static Number operator /(Number a, Number b) => new(new(a.Re * b.Re + a.Im * b.Im, a.Im * b.Re - a.Re * b.Im, a.ReDen * b.ReDen + a.ImDen * b.ImDen, a.ImDen * b.ReDen - a.ReDen * b.ImDen, a.Variable, a.VariableDen), new(b.Re * b.Re + b.Im * b.Im, 0, b.ReDen * b.ReDen + b.ImDen * b.ImDen, 0, a.Variable, a.VariableDen));
        public static Number operator /(Number a, double b) => a / (Number)b;
        public static Number operator /(double a, Number b) => (Number)a / b;

        public static bool operator ==(Number a, Number b) => a.Equals(b);
        public static bool operator ==(Number a, double b) => a == (Number)b;
        public static bool operator ==(double a, Number b) => (Number)a == b;

        public static bool operator !=(Number a, Number b) => !a.Equals(b);
        public static bool operator !=(Number a, double b) => a != (Number)b;
        public static bool operator !=(double a, Number b) => (Number)a != b;

        public static explicit operator Number(double d) => new(d, 0);
    }
}
