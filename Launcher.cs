using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using KappaLauncher.Apps;
using KappaLauncher.Misc;
using KappaLauncher.Views;
using KappaLauncher.Widgets;

namespace KappaLauncher {
    public static partial class Launcher {
        public static Context Context { get; private set; }
        public static FrameLayout Parent { get; private set; }
        public static ScrollView Scroll { get; private set; }
        public static LinearLayout Main { get; private set; }


        public static void Init(Context context) {
            Context = context;

            Parent = new FrameLayout(Context);
            
            Scroll = new ScrollView(Context);
            Parent.AddView(Scroll);

            Main = new LinearLayout(Context);
            Main.Orientation = Orientation.Vertical;
            Scroll.AddView(Main);
        }
        public static void Show(Activity activity) {
            ViewGroup parentGroup = (ViewGroup) Parent.Parent;
            parentGroup?.RemoveView(Parent);
            activity.SetContentView(Parent);
        }

        public static void Load() {
			LoadingScreen screen = new LoadingScreen(Context);
			screen.ProgressBar.LayerCount = 3;
			screen.ProgressBar.StrokeWidth = 8.Dip();
			Parent.AddView(screen);	

            AppManager.Load(progress => screen.ProgressBar.SetProgress(0, (float) progress));

			AppGroupData data = new AppGroupData(AppManager.Apps);
			Widgets.Add(data);

			Parent.RemoveView(screen);
        }

        public static void Save() {

        }


		public static void Build() {
			Widgets.ForEach(widget => {
				if(widget is AppGroupData) {
					AppGroupView view = new AppGroupView(Context, (AppGroupData) widget);
					Main.AddView(view);
				}

			});
		}
    }
}