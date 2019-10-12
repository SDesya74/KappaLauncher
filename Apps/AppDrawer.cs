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
		private static Paint Paint = new Paint(PaintFlags.AntiAlias);
		private static Path Path = new Path();
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
					RectF roundRect = new RectF(x, y, x + data.BackBounds.Width(), y + data.BackBounds.Height());
					canvas.DrawRoundRect(roundRect, 3.Dip(), 3.Dip(), Paint);

					break;

				case AppDrawingData.Shape.Oval:
					RectF oval = new RectF(x, y, x + data.BackBounds.Width(), y + data.BackBounds.Height());
					canvas.DrawRoundRect(oval, 999.Dip(), 999.Dip(), Paint);
					break;

				case AppDrawingData.Shape.Hex:
					Path.Reset();
					int width = data.BackBounds.Width();
					int height = data.BackBounds.Height();
					Path.MoveTo(x + width / 2, y);

					Path.LineTo(x + width - height / 2, y);
					Path.LineTo(x + width, y + height / 2);
					Path.LineTo(x + width - height / 2, y + height);

					Path.LineTo(x + height / 2, y + height);
					Path.LineTo(x, y + height / 2);
					Path.LineTo(x + height / 2, y);

					Path.LineTo(x + width / 2, y);

					canvas.DrawPath(Path, Paint);
					Path.Reset();

					break;
			}
		}

		public static void DrawAppLabel(Canvas canvas, int x, int y, AppDrawingData data) {

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