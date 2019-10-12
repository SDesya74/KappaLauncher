using System;

using Android.Graphics;
using Android.Util;
using KappaLauncher.Misc;

namespace KappaLauncher.Apps {
	public class AppDrawingData {
		public string Package { get; private set; }
		public string Label { get; set; }

		public Color TextColor { get; set; }
		public Color BackColor { get; set; }
		public Style BackStyle { get; set; }
		public Shape BackShape { get; set; }
		public float Alpha { get; set; }
		public int Popularity { get; set; }

		public Rect BackBounds { get; private set; }
		public Rect TextBounds { get; private set; }
		public Point Padding { get; set; }
		public float TextSize {
			get { return TypedValue.ApplyDimension(ComplexUnitType.Sp, Popularity + 10, Screen.DisplayMetrics); }
		}


		public AppDrawingData(AppManager.App app) {
			Package = app.Package;
			Label = app.Name;
			Popularity = app.Popularity;

			TextColor = Color.LightGray;

			Random r = new Random();
			BackColor = Color.Rgb(r.Next(256), r.Next(256), r.Next(256));
			Alpha = 1;

			BackShape = Shape.Rect;
			BackStyle = Style.Fill;

			Padding = new Point(3.Dip(), 3.Dip());

			CalcBounds();
		}

		private void CalcBounds() {
			TextBounds = AppDrawer.GetTextBounds(this);
			BackBounds = new Rect(TextBounds);
			
			BackBounds.Left -= Padding.X;
			BackBounds.Right += Padding.X;

			BackBounds.Top -= Padding.Y;
			BackBounds.Bottom += Padding.Y;
		}

		public enum Style {
			Stroke, Fill
		}
		public enum Shape {
			None, Rect, RoundRect, Oval, Hex
		}
	}
}