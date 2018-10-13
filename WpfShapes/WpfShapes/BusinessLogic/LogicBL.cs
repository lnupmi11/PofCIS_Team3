using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;

using WpfShapes.Classes;
using WpfShapes.Utils;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace WpfShapes.BusinessLogic
{
	public static class LogicBL
	{
		public static void SetColor(ref Brush current, ref BrokenLine picker)
		{
			if (!GetColorFromColorDilog(out var color)) return;
			current = color;
			picker.Fill = color;
		}

		public static IEnumerable<BrokenLine> ReadHexagons()
		{
			var ofd = new OpenFileDialog { Filter = @"Xml files (*.xml)|*.xml", DefaultExt = "xml", AddExtension = true };
			return ofd.ShowDialog() == true ? Serialization.Deserialize(ofd.FileName) : null;
		}

		public static void SaveBrokenLines(ref Canvas canvas)
		{
			var sfd = new SaveFileDialog { Filter = @"Xml files (*.xml)|*.xml", DefaultExt = "xml", FileName = "BrokenLines", AddExtension = true };
			if (sfd.ShowDialog() != true) return;

			var brokenLines = new List<BrokenLine>();
			foreach (var obj in canvas.Children)
			{
				if (obj is BrokenLine brokenLine)
				{
					brokenLines.Add(obj);
				}
			}

			Serialization.Serialize(sfd.FileName, brokenLines);
		}
	}
}