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
using KappaLauncher.Application.Apps;

namespace KappaLauncher {
    public static class Launcher {
        public static Context Context { get; private set; }
        public static FrameLayout Parent { get; private set; }
        public static ScrollView Scroll { get; private set; }
        public static LinearLayout Main { get; private set; }


        public static void Init(Context context) {
            Context = context;

            Parent = new FrameLayout(Context);
            Parent.SetBackgroundColor(Color.Green);

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
            AppManager.Load(progress => {
                if (progress == 1) Toast.MakeText(Context, "Apps Loaded...", ToastLength.Short).Show();
            });
        }

        public static void Save() {

        }
    }
}