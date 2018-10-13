using System;
using System.Xml.Serialization;

namespace WpfShapes.Classes
{
	[Serializable]
	public class Point
	{
		[XmlAttribute]
		public double X { get; set; }
		[XmlAttribute]
		public double Y { get; set; }

		public Point()
		{
			
		}
		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}
