using System;
using System.Collections.Generic;

using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using KappaLauncher.Apps;

namespace KappaLauncher.Widgets {
	class AppGroupView : View {
		public AppGroupData Data { get; private set; }
		public List<Row> Rows { get; private set; }


		public AppGroupView(Context context, AppGroupData data) : base(context) {
			Data = data;

			Rows = new List<Row>();
		}




		private void MeasureItems() {
			Row row = null;
			for (int i = 0; i < Data.Items.Count; i++) {
				AppDrawingData data = Data.Items[i];

				if (row == null) {
					row = new Row(this);
					Rows.Add(row);
				}

				if (row.CanAdd(data) || row.ItemsCount < 1) {
					row.Add(data);
				} else row = null;

			}
		}




		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
			base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

			MeasureItems();

			int height = Data.RowMargin;
			Rows.ForEach(e => height += e.Height + Data.RowMargin);

			SetMeasuredDimension(MeasuredWidth, height);
		}


		protected override void OnDraw(Canvas canvas) {
			int y = Data.RowMargin;
			Rows.ForEach(e => {
				e.Draw(canvas, y);
				y += e.Height + Data.RowMargin;
			});

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
				Width = 0;
			}

			public bool CanAdd(AppDrawingData data) {
				return Width + data.BackBounds.Width() < Parent.MeasuredWidth;
			}

			public void Add(AppDrawingData data) {
				Elements.Add(data);
				Width += data.BackBounds.Width();
				Height = Math.Max(Height, data.BackBounds.Height());
			}



			public void Draw(Canvas canvas, int y) {
				int bx = (Parent.Width - Width) >> 1;

				Elements.ForEach(e => {
					AppDrawer.DrawApp(canvas, bx, y + (e.BackBounds.Height() + Height) >> 1, e);
					bx += e.BackBounds.Width() + Parent.Data.ColumnMargin;
				});
				
			}
		}
	}
}