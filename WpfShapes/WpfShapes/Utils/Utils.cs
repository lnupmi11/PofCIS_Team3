using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

using Point = DrawShape.Classes.Point;

namespace WpfShapes.Utils
{
	public static class Util
	{

		public static bool OnSegment(System.Windows.Point p, System.Windows.Point q, System.Windows.Point r)
		{
			return q.X <= Math.Max(p.X, r.X)
			       && q.X >= Math.Min(p.X, r.X)
			       && q.Y <= Math.Max(p.Y, r.Y)
			       && q.Y >= Math.Min(p.Y, r.Y);
		}
 
		public static int Orientation(System.Windows.Point p, System.Windows.Point q, System.Windows.Point r)
		{
			var a = r.X - q.X;
			var b = r.Y - q.Y;
			var c = q.X - p.X;
			var d = q.Y - p.Y;

			var vpv = a * d - c * b; // vector product of vectors 
			if (vpv < 1e-9 && vpv > -1e-9)
			{
				return 0; // collinear 
            }

			return (vpv > 1e-9) ? -1 : 1; // clockwise or counterclockwise
		}
		
		
		
	}
}
