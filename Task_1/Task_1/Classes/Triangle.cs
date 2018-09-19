using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task_1.Structures;

namespace Task_1.Classes
{
    /// <summary>
    /// class represents Triangle
    /// </summary>
    /// <remarks>
    /// Main entity of our program
    /// </remarks>
    public class Triangle
    {
        /// <summary>
        /// Representation of the triangle by three Sides
        /// </summary>
        private ColoredSide[] sides = new ColoredSide[3];

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// Default constructor for Triangle.
        /// </summary>
        public Triangle() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// Constructor with parameter.
        /// </summary>
        /// <param name="color1">first side's color</param>
        /// <param name="length1">first side's length</param>
        /// <param name="color2">second side's color</param>
        /// <param name="length2">second side's length</param>
        /// <param name="color3">third side's color</param>
        /// <param name="length3">third side's length</param>
        public Triangle(string color1, int length1, string color2, int length2, string color3, int length3)
        {
            sides[0].Color = color1;
            sides[0].Length = length1;
            sides[1].Color = color2;
            sides[1].Length = length2;
            sides[2].Color = color3;
            sides[2].Length = length3;
        }

        /// <summary>
        /// Function to parse the triangle
        /// </summary>
        /// <returns>Triangle</returns>
        /// <param name="data">unparsed data</param>
        public Triangle Parse(string data)
        {
            var values = data.Split(',');
            var index = 0;
            for (int i = 0; i < values.Length; i += 2)
            {
                sides[index].Color = values[i];
                sides[index].Length = int.Parse(values[i + 1]);
                index++;
            }

            return this;
        }

        /// <summary>
        /// Function to save the triangle
        /// </summary>
        /// <param name="filename">file name of saved triangles</param>
        public void Save(string filename)
        {
            var info = sides.Aggregate(String.Empty, (current, side) => current + string.Concat(side.Color, ", ", side.Length.ToString(), ", "));
            info = info.Substring(0, info.Length - 2);
            File.AppendAllText(filename, $"{info}\n");
        }

        /// <summary>
        /// Method to calculate perimeter of the triangle
        /// </summary>
        /// <returns>int</returns>
        public int Perimeter()
        {
            var res = sides.Sum(side => side.Length);
            return res;
        }

        /// <summary>
        /// Representation of the triangle by string
        /// </summary>
        /// <returns>String representation of the element of triangle entity</returns>
        public override string ToString()
        {
            string str = string.Empty;
            foreach (var s in this.sides)
            {
                str += $"\nColor: {s.Color}, Length: {s.Length}\n";
            }
            return str;
        }

        /// <summary>
        /// Method to distinct colors
        /// </summary>
        /// <returns>list of sides</returns>
        public List<string> DistinctColors()
        {
            var cur = sides.Select(i => i.Color).Distinct().ToList();
            return cur;
        }

        /// <summary>
        /// Method that repaints third edge, if two other have equal colors
        /// </summary>
        /// <returns>Triangle</returns>
        public Triangle MakeUniqueColor()
        {
            var cur = DistinctColors();
            if (cur.Count == 3 || cur.Count == 1)
            {
                return this;
            }

            var majorColor = sides.Select(i => i.Color).Where(i => i == cur[0]).ToList().Count == 2 ? cur[0] : cur[1];

            var res = this;

            for (var i = 0; i < 3; ++i)
            {
                res.sides[i].Color = majorColor;
            }

            return res;
        }
    }
}