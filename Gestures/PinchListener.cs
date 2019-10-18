using Android.Views;

namespace KappaLauncher.Gestures {
	class PinchListener : BaseGestureListener {
		public override void OnClick(View view, MotionEvent e, long time) { }

		public override void OnMove(View view, MotionEvent e) { }

		public override void OnTouchEnd(View view, MotionEvent e) { }

		public override void OnTouchStart(View view, MotionEvent e) { }


		public delegate void OnPinchListener(Direction direction);
		public event OnPinchListener OnPinch;



		public enum Direction {
			In, Out
		}
	}
}