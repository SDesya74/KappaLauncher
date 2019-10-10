using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
					row = new Row();
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

			int height = 500;

			Toast.MakeText(Context, "Width: " + MeasuredWidth, ToastLength.Short).Show();

			SetMeasuredDimension(MeasuredWidth, height);
		}








		public class Row {
			public List<AppDrawingData> Elements { get; set; }
			private int Width { get; set; }
			public int ItemsCount {
				get { return Elements.Count; }
			}

			public View Parent;


			public Row(AppGroupView view) {
				Parent = view;
				Width = 0;
			}

			public bool CanAdd(AppDrawingData data) {
				return Width + data.BackBounds.Width() < Parent.MeasuredWidth;
			}

			public void Add(AppDrawingData data) {
				Elements.Add(data);
			}
		}
	}
}