using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WpfApp.Models;

namespace WpfApp.Utils
{
    public static class Serialization
    {
        public static void Serialize(string filename, IEnumerable<TaxiDriver> arr)
        {
            var xml = new XmlSerializer(typeof(List<TaxiDriver>));
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                xml.Serialize(fs, arr);
            }
        }

        public static void Serialize(string filename, IEnumerable<Order> arr)
        {
            var xml = new XmlSerializer(typeof(List<Order>));
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                xml.Serialize(fs, arr);
            }
        }

        public static IEnumerable<TaxiDriver> DeserializeDrivers(string filename)
        {
            var res = new List<TaxiDriver>();
            var xml = new XmlSerializer(typeof(List<TaxiDriver>));
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open))
                {
                    res = (List<TaxiDriver>)xml.Deserialize(fs);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }
            return res;
        }

        public static IEnumerable<Order> DeserializeOrders(string filename)
        {
            var res = new List<Order>();
            var xml = new XmlSerializer(typeof(List<Order>));
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open))
                {
                    res = (List<Order>)xml.Deserialize(fs);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }
            return res;
        }
    }
}