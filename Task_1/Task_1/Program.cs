using System;
using System.Collections.Generic;
using System.IO;
using Task_1.Classes;

namespace Task_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Ex: white, 10, red, 2, green, 5 then another triangle write in the next line
//            var arrTriangle = ReadData("data.txt");
//            foreach (var triangle in arrTriangle)
//            {
//                Console.WriteLine(triangle);
//                triangle.Save("info.txt");
//            }
           // Console.WriteLine(tr.Parse("white, 10, red, 2, green, 5"));
            Console.ReadKey();
        }

        public static IEnumerable<Triangle> ReadData(string filename)
        {
            var res = new List<Triangle>();
            string[] data;
            try
            {
                data = File.ReadAllLines(filename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }
            foreach (var line in data)
            {
                var tempTriangle = new Triangle();
                res.Add(tempTriangle.Parse(line));
            }

            return res;
        }
    }
}
