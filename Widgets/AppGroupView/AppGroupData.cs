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
using KappaLauncher.Apps;
using KappaLauncher.Misc;

namespace KappaLauncher.Widgets {

	[Serializable]
	class AppGroupData : Launcher.WidgetData{
		public List<AppDrawingData> Items { get; private set; }
		public int RowMargin { get; set; }
		public int ColumnMargin { get; set; }

		public AppGroupData(List<AppManager.App> list) {
			Items = new List<AppDrawingData>();
			list.ForEach(e => Items.Add(new AppDrawingData(e)));

			RowMargin = 3.Dip();
			ColumnMargin = 3.Dip();
		}
		
	}
}