using Android.Content;
using Android.Views;
using Android.Widget;
using KappaLauncher.Apps;
using KappaLauncher.Misc;

namespace KappaLauncher.Views.Dialogs {
	class AppEditDialog : DialogBase {

		public AppEditDialog(Context context, AppDrawingData data) : base(context) {

		}

		public override View OnCreate(Context context) {
			LinearLayout main = new LinearLayout(context);
			main.Orientation = Orientation.Vertical;
			main.SetPadding(5.Dip(), 5.Dip(), 5.Dip(), 5.Dip());
			main.SetBackgroundColor(Android.Graphics.Color.Rgb(255, 255, 255));
			return main;
		}
	}
}