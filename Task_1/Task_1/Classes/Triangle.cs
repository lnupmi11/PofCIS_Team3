using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task_1.Structures;

namespace Task_1.Classes
{
    public class Triangle
    {
        private ColoredSide[] Sides = new ColoredSide[3];
        
        public Triangle() {}        

        public Triangle(string color1, int length1, string color2, int length2, string color3, int length3)
        {
            Sides[0].Color = color1;
            Sides[0].Length = length1;
            Sides[1].Color = color2;
            Sides[1].Length = length2;
            Sides[2].Color = color3;
            Sides[2].Length = length3;
        }
        
        public Triangle Parse(string data)
        {
            var values = data.Split(',');
            var index = 0;
            for (int i = 0; i < values.Length; i += 2)
            {
                Sides[index].Color = values[i];
                Sides[index].Length = int.Parse(values[i + 1]);
                index++;
            }

            return this;
        }

        public void Save(string filename)
        {
            var info = Sides.Aggregate(String.Empty, (current, side) => current + string.Concat(side.Color, ", ", side.Length.ToString(), ", "));
            info = info.Substring(0, info.Length - 2);
            File.AppendAllText(filename, $"{info}\n");
        }

        public int Perimeter()
        {
            var res = Sides.Sum(side => side.Length);
            return res;
        }

        public override string ToString()
        {
            string str = string.Empty;
            foreach (var s in this.Sides)
            {
                str += $"\nColor: {s.Color}, Length: {s.Length}\n";
            }
            return str;
        }
        
        public List<string> DistinctColors()
        {
            var cur = Sides.Select(i => i.Color).Distinct().ToList();
            return cur;
        }

        public Triangle MakeUniqueColor() // repaints third edge, if two other have equal colors
        {
            var cur = DistinctColors();
            if (cur.Count == 3 || cur.Count == 1)
            {
                return this;
            }
            
            var majorColor = Sides.Select(i => i.Color).Where(i => i == cur[0]).ToList().Count == 2 ? cur[0] : cur[1];
            
            var res = this;

            for (var i = 0; i < 3; ++i)
            {
                res.Sides[i].Color = majorColor;
            }
            
            return res;

        }
        
    }
}
