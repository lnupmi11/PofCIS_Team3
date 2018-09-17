using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1.Interfaces;

namespace Task_1.Structures
{
    public struct ColoredSide : IColor
    {
        private int length;
        private string color;
        public string Color
        {
            get
            {
                return color;
            }
        }
        public int Length
        {
            get
            {
                return length;
            }
        }
    }
}
