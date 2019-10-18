using Android.Graphics;
using Android.Views;

namespace KappaLauncher.Gestures {
	public class BaseGestureListener : Java.Lang.Object, View.IOnTouchListener {

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

					OnTouchStart?.Invoke(view, e);
					return true;


				case MotionEventActions.Up:
					long time = Java.Lang.JavaSystem.CurrentTimeMillis() - TouchStartTime;
					TouchStartTime = -1;

					OnTouchEnd?.Invoke(view, e);
					if(!IsGesture) OnClick?.Invoke(view, e, time);
					return true;


				case MotionEventActions.Move:
					IsGesture = true;

					OnTouchMove?.Invoke(view, e);
					return true;
			}
			return false;
		}



		public delegate void TouchStartListener(View view, MotionEvent e);
		public event TouchStartListener OnTouchStart;

		public delegate void TouchEndListener(View view, MotionEvent e);
		public event TouchEndListener OnTouchEnd;

		public delegate void TouchMoveListener(View view, MotionEvent e);
		public event TouchMoveListener OnTouchMove;

		public delegate void OnClickListener(View view, MotionEvent e, long time);
		public event OnClickListener OnClick;
	}
}