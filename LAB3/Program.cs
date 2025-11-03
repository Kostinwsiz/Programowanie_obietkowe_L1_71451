using System;
using System.Security.Cryptography.X509Certificates;

namespace LAB3
{
    public interface IModular
    {
        double Module();
    }

    public sealed class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
    {
        private double re;
        private double im;

        public double Re
        {
            get => re;
            set => re = value;
        }

        public double Im
        {
            get => im;
            set => im = value;
        }

        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        public override string ToString()
        { 

            string sign = im >= 0 ? " + " : " - ";
            double absIm = Math.Abs(im);
            return $"{re}{sign}{absIm}i";
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) =>
            new ComplexNumber(a.re + b.re, a.im + b.im);

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b) =>
            new ComplexNumber(a.re - b.re, a.im - b.im);

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double real = a.re * b.re - a.im * b.im;
            double imag = a.re * b.im + a.im * b.re;
            return new ComplexNumber(real, imag);
        }

        public static ComplexNumber operator -(ComplexNumber a) =>
            new ComplexNumber(a.re, -a.im);

        public object Clone() => new ComplexNumber(re, im);

        public bool Equals(ComplexNumber? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return re == other.re && im == other.im;
        }

        public override bool Equals(object? obj) => obj is ComplexNumber other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(re, im);

        public static bool operator ==(ComplexNumber? left, ComplexNumber? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.Equals(right);
        }

        public static bool operator !=(ComplexNumber? left, ComplexNumber? right) => !(left == right);

        public double Module() => Math.Sqrt(re * re + im * im);
    }

    public class Program
    {
        public static void Main()
        {
            var z1 = new ComplexNumber(6, 7);
            var z2 = new ComplexNumber(2, -3);
            Console.WriteLine($"z1 = {z1}");
            Console.WriteLine($"z2 = {z2}");

            var sum = z1 + z2;
            var diff = z1 - z2;
            var prod = z1 * z2;
            var conj = -z1;

            Console.WriteLine($"z1 + z2 = {sum}");
            Console.WriteLine($"z1 - z2 = {diff}");
            Console.WriteLine($"z1 * z2 = {prod}");
            Console.WriteLine($"sprzężenie z1 = {conj}");

            var z1Clone = (ComplexNumber)z1.Clone();
            Console.WriteLine($"z1 clone = {z1Clone}");
            Console.WriteLine($"z1 == z1Clone: {z1 == z1Clone}");

            Console.WriteLine($"|z1| = {z1.Module():F4}");
        }
    }
}