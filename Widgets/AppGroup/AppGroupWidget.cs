using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using KappaLauncher.Apps;
using KappaLauncher.Views;
using System.Collections.Generic;

namespace KappaLauncher.Widgets.AppGroup {
	partial class AppGroupWidget : Widget, View.IOnTouchListener {
		public AppGroupData Data { get; private set; }
		

		public AppGroupWidget(Context context, AppGroupData data) : base(context) {
			Data = data;
			Rows = new List<Row>();

			SetOnTouchListener(this);
		}


		public void OnAppClick(AppDrawingData data) {
			AppManager.App app = AppManager.GetAppFromKey(data.Key);
			AppManager.StartApp(app);

			data.InvalidatePopularity();
			data.Invalidate();

			RequestLayout();
		}





		private void MeasureItems() {
			Rows.Clear();
			 
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
	}
}