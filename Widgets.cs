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

namespace KappaLauncher {
	public static partial class Launcher {
		public static List<WidgetData> Widgets;


		[Serializable]
		public abstract class WidgetData { };

	}
}