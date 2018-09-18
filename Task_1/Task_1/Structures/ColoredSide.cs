using Task_1.Interfaces;

namespace Task_1.Structures
{
    public struct ColoredSide : IColor
    {
        public string Color { get; set; }
        public int Length { get; set; }
    }
}
