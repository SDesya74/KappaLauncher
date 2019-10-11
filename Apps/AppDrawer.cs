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
		public static void DrawApp(Canvas canvas, int x, int y, AppDrawingData data) {
			DrawAppBackground(canvas, x, y, data);
		}

		public static void DrawAppBackground(Canvas canvas, int x, int y, AppDrawingData data) {
			Paint.SetStyle(data.BackStyle == AppDrawingData.Style.Fill ? Paint.Style.Fill : Paint.Style.Stroke);
			Paint.Color = data.BackColor;
			switch (data.BackShape) {
				case AppDrawingData.Shape.Rect:
					canvas.DrawRect(x, y, x + data.BackBounds.Width(), y + data.BackBounds.Height(), Paint);
					break;

				case AppDrawingData.Shape.RoundRect:
					RectF rect = new RectF(x, y, x + data.BackBounds.Width(), y + data.BackBounds.Height());
					canvas.DrawRoundRect(rect, 3.Dip(), 3.Dip(), Paint);

					break;

				case AppDrawingData.Shape.Oval:
					break;

				case AppDrawingData.Shape.Hex:
					break;
			}
		}









		public static Rect GetTextBounds(AppDrawingData data) {
			Rect result = new Rect();
			Paint.SetTypeface(FontManager.Current);
			Paint.TextSize = data.TextSize;
			Paint.GetTextBounds(data.Label, 0, data.Label.Length, result);

			Rect hr = new Rect();
			Paint.GetTextBounds("p", 0, 1, hr);
			int height = hr.Height();

			result.Top = -height / 2;
			result.Bottom = height / 2;

			return result;
		}
	}
}