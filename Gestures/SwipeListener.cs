using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KappaLauncher.Gestures {
	class SwipeListener : BaseGestureListener {
		public override void OnClick(View view, MotionEvent e, long time) {}

		public override void OnMove(View view, MotionEvent e) {}

		public override void OnTouchEnd(View view, MotionEvent e) {}

		public override void OnTouchStart(View view, MotionEvent e) {}


		public delegate void OnSwipeListener(Direction direction);
		public event OnSwipeListener OnSwipe;



		public enum Direction {
			LEFT, RIGHT
		}
	}
}