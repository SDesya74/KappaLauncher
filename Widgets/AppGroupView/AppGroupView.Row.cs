using Android.Graphics;

using KappaLauncher.Apps;

using System;
using System.Collections.Generic;

namespace KappaLauncher.Widgets {
	partial class AppGroupView {
		public List<Row> Rows { get; private set; }

		public Row GetRowByCoords(PointF touch) {
			float y = touch.Y;
			foreach(Row row in Rows) {
				if(y < row.Height) return row;
				y -= row.Height + Data.RowMargin;
			}
			return null;
		}



		public class Row {
			public List<AppDrawingData> Elements { get; set; }
			public int Width { get; private set; }
			public int Height { get; private set; }
			public int ItemsCount {
				get { return Elements.Count; }
			}

			public AppGroupView Parent;



			public AppDrawingData GetAppByCoords(PointF touch) {
				float x = touch.X;
				foreach(AppDrawingData app in Elements) {
					if(x < app.BackBounds.Width()) return app;
					x -= app.BackBounds.Width() + Parent.Data.RowMargin;
				}
				return null;
			}



			public Row(AppGroupView view) {
				Elements = new List<AppDrawingData>();
				Parent = view;
				Width = Height = 0;
			}

			public bool CanAdd(AppDrawingData data) {
				return Width + data.BackBounds.Width() < Parent.MeasuredWidth;
			}

			public void Add(AppDrawingData data) {
				Elements.Add(data);
				Width += data.BackBounds.Width() + Parent.Data.ColumnMargin;
				Height = Math.Max(Height, data.BackBounds.Height());
			}

			public void Draw(Canvas canvas, int y) {
				int bx = (Parent.Width - (Width - Parent.Data.ColumnMargin)) / 2;

				Elements.ForEach(e => {
					AppDrawer.DrawApp(canvas, bx, y + (Height - e.BackBounds.Height()) / 2, e);
					bx += e.BackBounds.Width() + Parent.Data.ColumnMargin;
				});

			}
		}
	}
}