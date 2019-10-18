using Android.Graphics;
using KappaLauncher.Misc;

namespace KappaLauncher.Apps {
	public static class AppDrawer {
		public static Paint Paint = new Paint(PaintFlags.AntiAlias);
		private static Path Path = new Path();
		public static void DrawApp(Canvas canvas, int x, int y, AppDrawingData data) {
			DrawAppBackground(canvas, x, y, data);
			DrawAppLabel(canvas, x, y, data);

			Paint.Color = Color.Red;
			canvas.DrawPoint(x, y, Paint);
		}

		public static void DrawAppBackground(Canvas canvas, int x, int y, AppDrawingData data) {
			Paint.SetStyle(data.BackStyle == AppDrawingData.Style.Fill ? Paint.Style.Fill : Paint.Style.Stroke);
			Paint.Color = data.BackColor;
			Paint.Alpha = data.Alpha;

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
			Paint.SetTypeface(Typeface.Create(FontManager.Current, data.TextStyle));
			Paint.TextSize = data.TextSize;
			Paint.Color = data.TextColor;
			Paint.Alpha = data.Alpha;

			int tx = x + (data.BackBounds.Width() - data.TextBounds.Width()) / 2;
			int ty = y + (data.BackBounds.Height() + data.TextBounds.Height()) / 2;

			
			canvas.DrawText(data.Label, tx, ty, Paint);
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