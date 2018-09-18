using Task_1.Structures;

namespace Task_1.Classes
{
    public class Triangle
    {
        private ColoredSide[] Sides;

        public Triangle()
        {
            this.Sides = new ColoredSide[3];
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
