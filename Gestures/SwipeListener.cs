using Android.Views;

namespace KappaLauncher.Gestures {
	class SwipeListener : BaseGestureListener {
		public override void OnClick(View view, MotionEvent e, long time) {}

		public override void OnMove(View view, MotionEvent e) {}

		public override void OnTouchEnd(View view, MotionEvent e) {}

		public override void OnTouchStart(View view, MotionEvent e) {}


		public delegate void OnSwipeListener(Direction direction);
		public event OnSwipeListener OnSwipe;



		public enum Direction {
			Left, Right
		}
	}
}