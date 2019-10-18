using Android.Graphics;
using Android.Views;

namespace KappaLauncher.Gestures {
	public abstract class BaseGestureListener{

		public bool IsGesture;
		public long TouchStartTime;
		public PointF TouchStart;
		public PointF TouchCurrent;
		

		public bool OnTouch(View view, MotionEvent e) {
			TouchCurrent = new PointF(e.GetX(), e.GetY());
			switch(e.Action) {
				case MotionEventActions.Down:
					TouchStart = new PointF(e.GetX(), e.GetY());
					TouchStartTime = Java.Lang.JavaSystem.CurrentTimeMillis();
					IsGesture = false;

					OnTouchStart(view, e);
					return true;


				case MotionEventActions.Up:
					long time = Java.Lang.JavaSystem.CurrentTimeMillis() - TouchStartTime;
					TouchStartTime = -1;

					OnTouchEnd(view, e);
					if(!IsGesture) OnClick(view, e, time);
					return true;


				case MotionEventActions.Move:
					IsGesture = true;

					OnMove(view, e);
					return true;
			}
			return false;
		}



		public abstract void OnTouchStart(View view, MotionEvent e);

		public abstract void OnTouchEnd(View view, MotionEvent e);

		public abstract void OnMove(View view, MotionEvent e);

		public abstract void OnClick(View view, MotionEvent e, long time);
	}
}