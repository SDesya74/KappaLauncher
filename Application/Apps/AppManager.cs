using System.Collections.Generic;

using Android.Content;
using Android.Content.PM;

namespace KappaLauncher.Application.Apps {
    public static class AppManager {
        public static List<App> Apps { get; private set; }
        private static PackageManager Manager;
        public static void Init(Context context) {
            Manager = context.PackageManager;
        }








        public class App {
            public string Package { get; private set; }
            public string Activity { get; private set; }
            public string Name { get; private set; }
            public long InstallTime { get; private set; }


            public int Popularity { get; set; }
            public bool Hidden { get; set; }
        }
    }
}