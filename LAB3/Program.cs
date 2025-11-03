using System;

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

   