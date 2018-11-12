using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WpfApp.Models;

namespace WpfApp.Utils
{

    /// <summary>
    /// Serialization class.
    /// </summary>
    public static class Serialization
    {
        /// <summary>
        /// Method to serialize taxi drivers.
        /// </summary>
        /// <param name="filename">Name of serialization file.</param>
        /// <param name="arr">List of taxi drivers</param>
        public static void Serialize(string filename, IEnumerable<TaxiDriver> arr)
        {
            var xml = new XmlSerializer(typeof(List<TaxiDriver>));
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                xml.Serialize(fs, arr);
            }
        }

        /// <summary>
        /// Method to serialize orders.
        /// </summary>
        /// <param name="filename">Name of serialization file.</param>
        /// <param name="arr">List of taxi orders</param>
        public static void Serialize(string filename, IEnumerable<Order> arr)
        {
            var xml = new XmlSerializer(typeof(List<Order>));
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                xml.Serialize(fs, arr);
            }
        }

        /// <summary>
        /// Method to deserialize taxi drivers.
        /// </summary>
        /// <param name="filename">Name of deserialization file.</param>
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

        /// <summary>
        /// Method to deserialize orders.
        /// </summary>
        /// <param name="filename">Name of deserialization file.</param>
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