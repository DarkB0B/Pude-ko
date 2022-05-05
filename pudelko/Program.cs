using System;

namespace pudelko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Pudelko pudelkobase = new Pudelko();
            Pudelko pudelko1 = new Pudelko(1,1,1);
            Console.WriteLine(pudelkobase.ToString());
            Console.WriteLine(pudelko1.ToString());
            Pudelko pudelko2 = new Pudelko(1, null, 1, UnitOfMeasure.milimeter);
            Console.WriteLine(pudelko2.ToString());
        }
    }
}
