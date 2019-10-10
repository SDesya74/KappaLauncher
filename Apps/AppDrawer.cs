using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KappaLauncher.Misc;

namespace KappaLauncher.Apps {
	public static class AppDrawer {
		private static Paint Paint = new Paint();
		public static void DrawApp(Canvas canvas, AppDrawingData data, int x, int y) {
			DrawAppBackground(canvas, data, x, y);
		}

		public static void DrawAppBackground(Canvas canvas, AppDrawingData data, int x, int y) {
			
			Paint.SetStyle(data.BackStyle == AppDrawingData.Style.Fill ? Paint.Style.Fill : Paint.Style.Stroke);
			switch (data.BackShape) {
				case AppDrawingData.Shape.Rect:
					canvas.DrawRect(x, y, x + data.BackBounds.Width(), y + data.BackBounds.Height(), Paint);
					break;

				case AppDrawingData.Shape.RoundRect:
					break;

				case AppDrawingData.Shape.Oval:
					break;

				case AppDrawingData.Shape.Hex:
					break;
			}
		}









		public static Rect CalcAppBounds(AppDrawingData data) {
			Rect result = new Rect();
			Paint.SetTypeface(FontManager.Current);
			Paint.TextSize = data.TextSize;
			Paint.GetTextBounds(data.Label, 0, data.Label.Length, result);
			return result;
		}
	}
}