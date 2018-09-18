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
        
        // TODO: add exception
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

        public override string ToString()
        {
            string str = string.Empty;
            foreach (var s in this.Sides)
            {
                str += $"\nColor: {s.Color}, Length: {s.Length}\n";
            }
            return str;
        }
    }
}
