using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task_1.Classes;

namespace Task_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
//            var arrTriangle = ReadData("data.txt");
//            SaveResultTask2(arrTriangle, "res2.txt");
            Console.ReadKey();
        }

        public static SortedList<int, Triangle> ReadData(string filename)
        {
            // will be sorted by first value, in our case it`s perimeter
            var res = new SortedList<int, Triangle>();
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
                res.Add(tempTriangle.Parse(line).Perimeter(), tempTriangle.Parse(line));
            }

            return res;
        }

        public static void SaveResultTask2(SortedList<int, Triangle> list, string filename)
        {
            var info = list.Aggregate(String.Empty, (current, elem) => current + string.Concat($"Perimeter: {elem.Key}", ", ", elem.Value, "\n"));
            File.AppendAllText(filename, $"{info}\n");
        }
    }
}
