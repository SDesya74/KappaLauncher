using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KappaLauncher.Application.Apps {
    public static class AppManager {
        public static List<App> Apps { get; private set; }
        private static PackageManager Manager;
        public static void Init(Context context) {
            Manager = context.PackageManager;

        }








        public class App {
            public String Package { get; private set; }
            public String Activity { get; private set; }
            public String Name { get; private set; }
            public long InstallTime { get; private set; }


            public int Popularity { get; set; }
            public bool Hidden { get; set; }
        }
    }
}