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
using KappaLauncher.Apps;

namespace KappaLauncher.Widgets {
	partial class AppGroupView {
		public List<Row> Rows { get; private set; }

		public Row GetRowByCoords(PointF touch) {
			float y = touch.Y;
			foreach(Row row in Rows) {
				if(y < row.Height) return row;
				y -= row.Height;
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

			public Row GetRowByCoords(PointF touch) {
				float y = touch.Y;
				foreach(Row row in Rows) {
					if(y < row.Height) return row;
					y -= row.Height;
				}
				return null;
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