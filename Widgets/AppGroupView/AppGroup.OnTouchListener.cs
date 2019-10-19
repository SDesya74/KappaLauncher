using Android.Graphics;
using Android.Views;
using KappaLauncher.Apps;

namespace KappaLauncher.Widgets {
	partial class AppGroupView {


		private bool IsClick = true;
		private long TouchStartTime = -1;

		public bool OnTouch(View v, MotionEvent e) {
			
			switch(e.Action) {
				case MotionEventActions.Down:
					IsClick = true;
					TouchStartTime = Java.Lang.JavaSystem.CurrentTimeMillis();
					break;

				case MotionEventActions.Move:
					IsClick = false;
					break;

				case MotionEventActions.Up:
					long time = Java.Lang.JavaSystem.CurrentTimeMillis() - TouchStartTime;
					if(IsClick) OnClick(e, time);
					break;
			}

			return true;
		}



		public void OnClick(MotionEvent e, long time) {
			if(time > 300) return;
			using(PointF touch = new PointF(e.GetX(), e.GetY())) {
				Row row = GetRowByCoords(touch);
				AppDrawingData app = row?.GetAppByCoords(touch);
				if(app != null) OnAppClick(app);
			}
		}
	}
}