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

namespace KappaLauncher.Application.Launcher {
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
            if (parentGroup != null) parentGroup.RemoveView(Parent);
            activity.SetContentView(Parent);
        }
    }
}