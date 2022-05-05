using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pudelko
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>
    {
        public double A { get { return Math.Truncate(a * 1000) / 1000; } }
        public double B { get { return Math.Truncate(a * 1000) / 1000; } }
        public double C { get { return Math.Truncate(a * 1000) / 1000; } }
        private readonly double a;
        private readonly double b;
        private readonly double c;
        public double Objetosc { get { return Math.Round(a * b * c, 9); } }
        public double Pole { get { return Math.Round(a * b * 2 + b * c * 2 + c * a * 2, 6); } }
        public UnitOfMeasure Unit { get; private set; }
        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if (a == null)
            {
                if (unit == UnitOfMeasure.meter) { a = 10; }
                else if (unit == UnitOfMeasure.centimeter) { a = 1000; }
                else if (unit == UnitOfMeasure.milimeter) { a = 10000; }
            }
            if (b == null)
            {
                if (unit == UnitOfMeasure.meter) { b = 10; }
                else if (unit == UnitOfMeasure.centimeter) { b = 1000; }
                else if (unit == UnitOfMeasure.milimeter) { b = 10000; }
            }
            if (c == null)
            {
                if (unit == UnitOfMeasure.meter) { c = 10; }
                else if (unit == UnitOfMeasure.centimeter) { c = 1000; }
                else if (unit == UnitOfMeasure.milimeter) { c = 10000; }
            }
            switch (unit)
            {
                case UnitOfMeasure.meter: break;
                case UnitOfMeasure.centimeter: a = a * 100; b = b * 100; c = c * 100; break;
                case UnitOfMeasure.milimeter: a = a * 1000; b = b * 1000; c = c * 1000; break;
            }
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentOutOfRangeException("błędne wymiary pudełka");
            }
            if (unit == UnitOfMeasure.meter || a > 10 || b > 10 || c > 10)
            {
                throw new ArgumentOutOfRangeException("błędne wymiary pudełka");
            }
            this.a = (double)a;
            this.b = (double)b;
            this.c = (double)c;
            this.Unit = unit;
        }
        public override string ToString()
        {
            return $"{A} {Unit} x {B} {Unit} x {C} {Unit}";
        }
        public string ToString(string format)
        {
            switch (format.ToLower())
            {
                case "m": return $"{A} {UnitOfMeasure.meter} x {B} {UnitOfMeasure.meter} x {C} {UnitOfMeasure.meter}";
                case "cm": return $"{Math.Round(A * 100, 1):0.0} {UnitOfMeasure.meter} x {Math.Round(B * 100, 1):0.0} {UnitOfMeasure.meter} x {Math.Round(C * 100, 1):0.0} {UnitOfMeasure.meter}";
                case "mm": return $"{Math.Round(A * 10000, 1):0.000} {UnitOfMeasure.meter} x {Math.Round(B * 10000, 1):0.000} {UnitOfMeasure.meter} x {Math.Round(C * 10000, 1):0.000} {UnitOfMeasure.meter}";
                default: throw new FormatException();
            }

        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString(format);
        }

        public bool Equals(Pudelko other)
        {
            if (other is null)
            {
                throw new ArgumentNullException();
            }
            if (Unit != other.Unit) 
            { 
                return false;
            }                
            double[] dimensions = { a, b, c };
            double[] dimensionsOther = { other.a, other.b, other.c };
            bool check = false;
            for (int i = 0; i != 3; i++)
            {
                for (int j = 0; j != 3; j++)
                {
                    if (dimensions[i] == dimensionsOther[j])
                        check = true;
                }
                if (check == false) 
                {
                    return false;
                }
                    
            }
            return true;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(a, b, c, Unit);
        }
        public static bool operator ==(Pudelko pudelko1, Pudelko pudelko2)
        {
            return pudelko1.Equals(pudelko2);
        }
        public static bool operator !=(Pudelko pudelko1, Pudelko pudelko2)
        {
            return !pudelko1.Equals(pudelko2);
        }


    }

}
