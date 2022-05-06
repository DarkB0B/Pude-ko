using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pudelko
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>, IComparable<Pudelko>
    {
        public double A { get { return Math.Truncate(a * 1000) / 1000; } }
        public double B { get { return Math.Truncate(b * 1000) / 1000; } }
        public double C { get { return Math.Truncate(c * 1000) / 1000; } }
        private readonly double a;
        private readonly double b;
        private readonly double c;
        public double Objetosc { get { return Math.Round(a * b * c, 9); } }
        public double Pole { get { return Math.Round(a * b * 2 + b * c * 2 + c * a * 2, 6); } }
        public UnitOfMeasure Unit { get; private set; }
        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if (a is null)
            {
                if (unit == UnitOfMeasure.meter) { a = 0.1; }
                else if (unit == UnitOfMeasure.centimeter) { a = 10; }
                else if (unit == UnitOfMeasure.milimeter) { a = 100; }
            }
            if (b is null)
            {
                if (unit == UnitOfMeasure.meter) { b = 0.1; }
                else if (unit == UnitOfMeasure.centimeter) { b = 10; }
                else if (unit == UnitOfMeasure.milimeter) { b = 100; }
            }
            if (c is null)
            {
                if (unit == UnitOfMeasure.meter) { c = 0.1; }
                else if (unit == UnitOfMeasure.centimeter) { c = 10; }
                else if (unit == UnitOfMeasure.milimeter) { c = 100; }
            }
            switch (unit)
            {
                case UnitOfMeasure.meter: break;
                case UnitOfMeasure.centimeter: a = a * 0.01; b = b * 0.01; c = c * 0.01; break;
                case UnitOfMeasure.milimeter: a = a * 0.001; b = b * 0.001; c = c * 0.001; break;
            }
            if (a <= 0.001 || b <= 0.001 || c <= 0.001)
            {
                throw new ArgumentOutOfRangeException("błędne wymiary pudełka");
            }
            if ( a > 10 || b > 10 || c > 10)
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
            switch (this.Unit)
            {
                case UnitOfMeasure.meter: return $"{Math.Round(A, 3):0.000} {"m"} × {Math.Round(B, 3):0.000} {"m"} × {Math.Round(C, 3):0.000} {"m"}";
                case UnitOfMeasure.centimeter: return $"{Math.Round(A * 100, 1):0.0} {"cm"} × {Math.Round(B * 100, 1):0.0} {"cm"} × {Math.Round(C * 100, 1):0.0} {"cm"}";
                case UnitOfMeasure.milimeter: return $"{Math.Round(A * 1000, 0)} {"mm"} × {Math.Round(B * 1000, 0)} {"mm"} × {Math.Round(C * 1000, 0)} {"mm"}";
                default: return $"{Math.Round(A, 3):0.000} {"m"} × {Math.Round(B, 3):0.000} {"m"} × {Math.Round(C, 3):0.000} {"m"}"; ;
            }
            
        }
        public string ToString(string format)
        {
            if(format == null) { return ToString(); }
            switch (format.ToLower())
            {
                case "m": return $"{Math.Round(A, 3):0.000} {"m"} × {Math.Round(B, 3):0.000} {"m"} × {Math.Round(C, 3):0.000} {"m"}";
                case "cm": return $"{Math.Round(A * 100, 1):0.0} {"cm"} × {Math.Round(B * 100, 1):0.0} {"cm"} × {Math.Round(C * 100, 1):0.0} {"cm"}";
                case "mm": return $"{Math.Round(A * 1000, 0)} {"mm"} × {Math.Round(B * 1000, 0)} {"mm"} × {Math.Round(C * 1000, 0)} {"mm"}";
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

        public IEnumerator<double> GetEnumerator()
        {
            for(int i = 0;i != 3; i++)
            {
                yield return this[i];    
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public static Pudelko Parse(string input)
        {
            UnitOfMeasure unitOfMeasure;
            string[] tabela = input.Split(" ");
            if(tabela[1] != tabela[4] && tabela[4] != tabela[7])//check if all units of measure are equal
            {
                throw new ArgumentException();
            }
            if(tabela[1] == "m")
            { unitOfMeasure = UnitOfMeasure.meter; }
            else if(tabela[1] == "cm")
            {
                unitOfMeasure = UnitOfMeasure.centimeter;
            }
            else if(tabela[1] == "mm")
            {
                unitOfMeasure = UnitOfMeasure.milimeter;
            }
            else
            {
                throw new ArgumentException("zła jednostka");
            }
            return new Pudelko(Convert.ToDouble(tabela[0]), Convert.ToDouble(tabela[3]), Convert.ToDouble(tabela[6]), unitOfMeasure);
        }

        public static bool operator ==(Pudelko pudelko1, Pudelko pudelko2)
        {
            return pudelko1.Equals(pudelko2);
        }
        public static bool operator !=(Pudelko pudelko1, Pudelko pudelko2)
        {
            return !pudelko1.Equals(pudelko2);
        }
        public static Pudelko operator +(Pudelko pudelko1, Pudelko pudelko2) 
        {
            double width = pudelko1.a + pudelko2.a;
            double length = Math.Max(pudelko1.b, pudelko2.b);
            double height = Math.Max(pudelko1.c, pudelko2.c);
            return new Pudelko(width, length, height); 
        }
        public static explicit operator double[](Pudelko pudelko)
        {
            return new double[] { pudelko.a, pudelko.b, pudelko.c, };
        }
        public static implicit operator Pudelko(ValueTuple<int, int, int> value)
        {          
            return new Pudelko(value.Item1, value.Item2, value.Item3, UnitOfMeasure.milimeter);
        }
        public double this[int index]
        {
            get
            {
               if(index == 2) { return c; }
               else if(index == 1) { return b; }
               else { return a; }                        
            }
        }

        public override bool Equals(object obj)
        {
            if(obj is not Pudelko)
            {
                return false;
            }
            else
            {
                return Equals(obj as Pudelko);
            }
            
        }

       
        public int CompareTo(Pudelko other)
        {
            if(this.Objetosc > other.Objetosc) { return 1; }
            else if(this.Objetosc < other.Objetosc){ return -1; }
            else if(this.Pole > other.Pole) { return 1; }
            else if(this.Pole < other.Pole) { return -1; }
            else if(this.a + this.b + this.c > other.a + other.b + other.c) { return 1; }
            else if (this.a + this.b + this.c < other.a + other.b + other.c) { return 1; }
            else { return 0; }
        }
    }

}
