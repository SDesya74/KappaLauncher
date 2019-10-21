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

namespace KappaLauncher.Views.Dialogs {
	abstract class DialogBase {
		private PopupWindow Window;
		private Context Context;
		public DialogBase(Context context) {
			Window = new PopupWindow();
			Context = context;
		}

		public abstract View OnCreate(Context context);


		public bool IsShowing {
			get {
				return (bool) Window?.IsShowing;
			}
		}

		public void Show() {
			View view = OnCreate(Context);
			Window = new PopupWindow(view, ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			Window?.ShowAtLocation(Launcher.Parent, GravityFlags.Center, 0, 0);
		}

		public void Dismiss() {
			Window?.Dismiss();
			Window?.Dispose();
		}
	}
}