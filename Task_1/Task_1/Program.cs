using System;
using Task_1.Classes;
using Task_1.Structures;

namespace Task_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ColoredSide cs = default(ColoredSide);
            Triangle t = new Triangle();
            Console.WriteLine(cs.Length);
            Console.WriteLine(t);
            Console.ReadKey();
        }
    }
}
