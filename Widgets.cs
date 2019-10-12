using System;
using System.Collections.Generic;

namespace KappaLauncher {
	public static partial class Launcher {
		public static List<WidgetData> Widgets;

		[Serializable]
		public abstract class WidgetData { };

	}
}