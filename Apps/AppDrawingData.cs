using System;

using Android.Graphics;
using Android.Util;
using KappaLauncher.Misc;

namespace KappaLauncher.Apps {
	public class AppDrawingData {
		public string Key { get; private set; }
		public string Label { get; set; }

		public Color TextColor { get; set; }
		public Color BackColor { get; set; }
		public Style BackStyle { get; set; }
		public Shape BackShape { get; set; }
		public int Alpha { get; set; }
		public int Popularity { get; set; }

		public Rect BackBounds { get; private set; }
		public Rect TextBounds { get; private set; }
		public Point Padding { get; set; }
		public float TextSize {
			get { return TypedValue.ApplyDimension(ComplexUnitType.Sp, Popularity + 10, Screen.DisplayMetrics); }
		}
		public TypefaceStyle TextStyle { get; set; }


		public AppDrawingData(AppManager.App app) {
			Key = app.Key;
			Label = app.Name;
			Popularity = app.Popularity;

			TextColor = Color.LightGray;
			TextStyle = TypefaceStyle.Normal;

			BackColor = Color.Transparent;
			Alpha = 255;

			BackShape = Shape.None;
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
		public void Invalidate() => CalcBounds();
		public void InvalidatePopularity() => Popularity = AppManager.GetAppFromKey(Key)?.Popularity ?? 0;

		public enum Style {
			Stroke, Fill
		}
		public enum Shape {
			None, Rect, RoundRect, Oval, Hex
		}
	}
}