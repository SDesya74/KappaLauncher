using Android.Graphics;
using Android.Views;
using System;

namespace KappaLauncher.Gestures {
	public abstract class BaseGestureListener{

		public bool IsGesture;
		public long TouchStartTime;

		private float Distance(PointF first, PointF second) {
			float dx = second.X - first.X;
			float dy = second.Y - first.Y;

			return MathF.Sqrt(dx * dx + dy * dy);
		}


		public bool OnTouchEvent(View view, MotionEvent e) {
			OnTouch(view, e);

			switch(e.Action) {	
				case MotionEventActions.Down:
					TouchStartTime = Java.Lang.JavaSystem.CurrentTimeMillis();
					IsGesture = false;

					OnTouchStart(view, e);
					return true;


				case MotionEventActions.Up:
					long time = Java.Lang.JavaSystem.CurrentTimeMillis() - TouchStartTime;
					TouchStartTime = -1;

					OnTouchEnd(view, e, time);
					if(!IsGesture) OnClick(view, e, time);
					return true;


				case MotionEventActions.Move:
					IsGesture = true;

					OnMove(view, e);
					return true;
			}
			return false;
		}

		public abstract void OnTouch(View view, MotionEvent e);

		public abstract void OnTouchStart(View view, MotionEvent e);

		public abstract void OnTouchEnd(View view, MotionEvent e, long time);

		public abstract void OnMove(View view, MotionEvent e);

		public abstract void OnClick(View view, MotionEvent e, long time);
	}
}