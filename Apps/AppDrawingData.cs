using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using KappaLauncher.Misc;

namespace KappaLauncher.Apps {
	public class AppDrawingData {
		public string Package { get; private set; }

		public string Label { get; set; }

		public int TextColor { get; set; }
		public int BackColor { get; set; }
		public Style BackStyle { get; set; }
		public Shape BackShape { get; set; }
		public float Alpha { get; set; }
		public int Popularity { get; set; }

		public Rect BackBounds { get; private set; }
		public Rect TextBounds { get; private set; }

		public float TextSize {
			get { return TypedValue.ApplyDimension(ComplexUnitType.Sp, Popularity + 10, Screen.DisplayMetrics); }
		}


		public AppDrawingData(AppManager.App app) {
			Package = app.Package;
			Label = app.Name;
			Popularity = app.Popularity;

			TextColor = Color.LightGray;
			BackColor = Color.Transparent;
			Alpha = 1;

			BackShape = Shape.None;
			BackStyle = Style.Fill;
		}


		public enum Style {
			Stroke, Fill
		}

		public enum Shape {
			None, Rect, RoundRect, Oval, Hex
		}
	}
}