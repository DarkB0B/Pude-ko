using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pudelko
{
    public sealed class Pudelko
    {
        public double A { get; private set;}
        public double B { get; private set; }
        public double C { get; private set; }
        public Pudelko(double a, double b, double c, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if(a <= 0 || b <= 0 || c <= 0 || a > 10 || b > 10 || c > 10)
            {
                throw new ArgumentException("błędne wymiary pudełka");
            }
        }
    }
    
}
