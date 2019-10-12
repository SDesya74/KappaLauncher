using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.Content.PM;
using KappaLauncher.Misc;
using Android.OS;

namespace KappaLauncher.Apps {
    public static class AppManager {
        public static List<App> Apps { get; private set; }
        private static PackageManager Manager;
        public static void Init(Context context) {
            Manager = context.PackageManager;
            Apps = new List<App>();
        }



        public delegate void LoadingListener(double progress);
        public static void Load(LoadingListener listener) {
			Handler handler = new Handler();
            Apps = (List<App>) DataSaver.Read("apps");
			if (Apps == null) {
				Java.Lang.Thread Th;
				Th = new Java.Lang.Thread(() => {
					Apps = new List<App>();

					Intent loader = new Intent(Intent.ActionMain, null);
					loader.AddCategory(Intent.CategoryLauncher);
					List<ResolveInfo> resolve = Manager.QueryIntentActivities(loader, 0).ToList();

					double progress = 0D;
					resolve.ForEach(e => {
						LoadAppFromResolve(e);
						progress++;
						listener(progress / (double) resolve.Count);


						try {
							Java.Lang.Thread.Sleep(10L); 
						} catch { }



					});
				});
				Th.Start();
			} else listener(1);
        }
        public static void LoadAppFromResolve(ResolveInfo e) {
            string package = e.ActivityInfo.PackageName;
            string activity = e.ActivityInfo.Name;
            string name = e.LoadLabel(Manager);

            long installTime = Manager.GetPackageInfo(package, 0).FirstInstallTime;

            App app = new App(package, activity, name, installTime);
            Apps.Add(app);
        }
		public static App GetFromPackage(string package) {
			return Apps.FirstOrDefault(e => e.Package == package);
		}
        public static void Save() {
            DataSaver.Save("apps", Apps);
        }





        public class App {
            public string Package { get; private set; } // key
            public string Activity { get; private set; }
            public string Name { get; private set; }
            public long InstallTime { get; private set; }

            public App(string package, string activity, string name, long instllTime) {
                Package = package;
                Activity = activity;
                Name = name;
                InstallTime = instllTime;
            }


            public int Popularity { get; set; }
            public bool Hidden { get; set; }
        }
    }
}