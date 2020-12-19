using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        readonly double re;
        readonly double im;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real => this.re;

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary => this.im;

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus => Math.Sqrt(this.re * this.re + this.im * this.im);

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase => Math.Atan2(this.im, this.re);

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            var outString = new StringBuilder();
            outString.Append(this.re.ToString(CultureInfo.CurrentCulture));
            outString.Append(this.im > 0 ? " + i"
                : this.im < 0 ? " - i"
                : "");
            outString.Append(Math.Abs(this.im));
            return outString.ToString();
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other)
        {
            const double tolerance = 0.000000001;
            return other != null
                && Math.Abs(this.re - other.Real) < tolerance
                && Math.Abs(this.im - other.Imaginary) < tolerance;
        }

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as IComplex);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            double s = this.re + this.im;
            return (this.Modulus / (s != 0 ? s : 1.0)).GetHashCode();
        }
    }
}
