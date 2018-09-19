using Task_1.Interfaces;

namespace Task_1.Structures
{
    /// <summary>
    /// struct represents colored side
    /// </summary>
    public struct ColoredSide : IColor
    {
        /// <summary>
        /// read-write Color and Length properties
        /// </summary>
        public string Color { get; set; }
        public int Length { get; set; }
    }
}
