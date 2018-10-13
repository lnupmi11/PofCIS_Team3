using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

using WpfShapes.Classes;

namespace WpfShapes.Utils
{
    public static class Serialization
    {
        public static void Serialize(string filename, IEnumerable<BrokenLine> arr)
        {
            var xml = new XmlSerializer(typeof(List<BrokenLine>));
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                xml.Serialize(fs, arr);
            }
        }

        public static IEnumerable<BrokenLine> Deserialize(string filename)
        {
            var res = new List<BrokenLine>();
            var xml = new XmlSerializer(typeof(List<BrokenLine>));
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open))
                {
                    res = (List<BrokenLine>) xml.Deserialize(fs);
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