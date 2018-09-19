using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Task_1.Classes;
using Task_1.Interfaces;

namespace Task_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
//            var arrTriangle = ReadData("data.txt");
//            SaveResultTask2(arrTriangle, "res2.txt");
            SingleColoredTriangles(new List<Triangle>());

            var cur = new List<Triangle>();
            
            cur.Add(new Triangle("a", 1, "a", 2, "a", 3));
            cur.Add(new Triangle("a", 1, "d", 2, "b", 3));
            cur.Add(new Triangle("b", 1, "b", 2, "b", 3));
            cur.Add(new Triangle("c", 1, "c", 2, "c", 3));
            cur.Add(new Triangle("c", 1, "c", 2, "c", 3));


            var res = RePaintSides(cur);

            foreach (var iter in res)
            {
                Console.WriteLine($"{iter.ToString()}");
            }

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


        public static SortedList<string, int> SingleColoredTriangles(List<Triangle> trianglesList)
        {
            
            var res = new SortedList<string, int>();
            var cur = trianglesList.Where(i => i.DistinctColors().Count == 1).GroupBy(it => it.DistinctColors()[0]);            
            foreach (var iter in cur)
            {
                res.Add(iter.Key, iter.ToList().Count);
                Console.WriteLine($"{iter.Key} {iter.ToList().Count}");
            }
            return res;
        }

        public static List<Triangle> RePaintSides(List<Triangle> trianglesList)
        {
            return trianglesList.Select(it => it.MakeUniqueColor()).ToList();
        }
        
    }
}
