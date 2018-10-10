using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace WpfShapes.Classes
{
	public class BrokenLine
	{
		public class LineColor
		{
			public int R { get; set; }

			public int G { get; set; }

			public int B { get; set; }

			public LineColor()
			{
			}

			public LineColor(int r, int g, int b)
			{
				R = r;
				G = g;
				B = b;
			}
		}
		
		public string Name { get; set; }

		public LineColor ColorLine { get; set; }

		public List<Point> Points { get; set; }

		
		
		public BrokenLine()
		{
		}

		public BrokenLine(string name, List<Point> points, Brush brush)
		{
			if (points == null)
			{
				throw new NullReferenceException("no points exception");
			}
			
			
			Name = name;
			Points = points;
			var color = ((SolidColorBrush) brush).Color;
			ColorLine = new LineColor(color.R, color.G, color.B);
		}

		public Point[] toArray()
		{
			List<Point> ret = new List<Point> ();
			
			foreach (var it in Points)
			{
				ret.Add(it);
			}
			return ret.ToArray();
		}

		public void drawLine()
		{
			bool found = true;
			while (found && Points.Count > 1)
			{
				for (var i = 0; i < Points.Count - 1; ++i)
				{
					if (Points[i] == Points[i + 1])
					{
						Points.RemoveAt(i);
						found = true;
						break;
					}
					found = false;
				}
			}
			// ready to draw line
		}
		
	}
}
