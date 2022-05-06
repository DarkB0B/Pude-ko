using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pudelko
{
    public static class PudelkoKompresuj
    {
        public static Pudelko Kompresuj(this Pudelko pudelko)
        {
            double a = Math.Pow(pudelko.Objetosc, 1 / 3);
            return new Pudelko(a, a, a, pudelko.Unit);
        }
    }
}
