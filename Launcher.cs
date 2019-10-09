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

namespace KappaLauncher {
    public static class Launcher {
        public static Context Context { get; private set; }
        public static FrameLayout Parent { get; private set; }
        public static ScrollView Scroll { get; private set; }
        public static LinearLayout Main { get; private set; }


        public static void Init(Context context) {
            Context = context;

            Parent = new FrameLayout(Context);
            Parent.SetBackgroundColor(Color.Rgb(0, 100, 0));

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
			screen.ProgressBar.StrokeWidth = 3.Dip();
			Parent.AddView(screen);	

            AppManager.Load(progress => {
				progress *= screen.ProgressBar.LayerCount;
				int layer = 0;
				while(progress > 0) {
					screen.ProgressBar.SetProgress(layer++, Math.Min((float) progress--, 1));
				}
				
            });
        }

        public static void Save() {

        }
    }
}